import { Checkbox } from '@/components/ui/checkbox';
import { Field } from '@/components/ui/field';
import { toaster } from '@/components/ui/toaster';
import { uploadFile } from '@/shared/api/mediaApi';
import { addAssignments, Assignment } from '@/shared/api/testsApi';
import { Button, Flex, Input, Text } from '@chakra-ui/react';
import { useEffect, useRef, useState } from 'react';

interface MultiTypeTestProps {
  materialId: number;
}

const MultiAnswerTypeTest: React.FC<MultiTypeTestProps> = ({ materialId }) => {
  const [problemDescription, setProblemDescription] = useState<string>('');
  const [questions, setQuestions] = useState<string[]>(['', '', '']);
  const [answers, setAnswers] = useState<string[]>(['', '', '', '', '']);
  const [correctMatches, setCorrectMatches] = useState<number[][]>(Array(3).fill([]));
  const [validatedTopicId, setValidatedTopicId] = useState<number | null>(null);
  const [imageFile, setImageFile] = useState<File | null>(null);
  const [imageId, setImageId] = useState<string | null>(null);
  const [answerImages, setAnswerImages] = useState<(File | null)[]>(Array(5).fill(null));
  const [answerImageIds, setAnswerImageIds] = useState<(string | null)[]>(Array(5).fill(null));
  const [questionImages, setQuestionImages] = useState<(File | null)[]>(Array(3).fill(null));
  const [questionImageIds, setQuestionImageIds] = useState<(string | null)[]>(Array(3).fill(null));

  const fileInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (materialId !== null) {
      setValidatedTopicId(materialId);
    }
  }, [materialId]);

  const handleQuestionImageChange = (index: number, file: File) => {
    const newImages = [...questionImages];
    newImages[index] = file;
    setQuestionImages(newImages);
  };

  const handleRemoveQuestionImage = (index: number) => {
    const newImages = [...questionImages];
    const newIds = [...questionImageIds];
    newImages[index] = null;
    newIds[index] = null;
    setQuestionImages(newImages);
    setQuestionImageIds(newIds);
  };

  const handleAnswerImageChange = (index: number, file: File) => {
    const newImages = [...answerImages];
    newImages[index] = file;
    setAnswerImages(newImages);

    // автозаміщення тексту на літеру
    const newAnswers = [...answers];
    newAnswers[index] = String.fromCharCode(1040 + index);
    setAnswers(newAnswers);
  };

  const handleRemoveAnswerImage = (index: number) => {
    const newImages = [...answerImages];
    const newIds = [...answerImageIds];
    newImages[index] = null;
    newIds[index] = null;
    setAnswerImages(newImages);
    setAnswerImageIds(newIds);
  };

  const handleQuestionChange = (index: number, value: string) => {
    const newQuestions = [...questions];
    newQuestions[index] = value;
    setQuestions(newQuestions);
  };

  const handleAnswerChange = (index: number, value: string) => {
    const newAnswers = [...answers];
    newAnswers[index] = value;
    setAnswers(newAnswers);
  };

  const toggleCorrectMatch = (rowIndex: number, colIndex: number) => {
    const newMatches = [...correctMatches];
    if (newMatches[rowIndex].includes(colIndex)) {
      newMatches[rowIndex] = newMatches[rowIndex].filter((match) => match !== colIndex);
    } else {
      newMatches[rowIndex] = [...newMatches[rowIndex], colIndex];
    }
    setCorrectMatches(newMatches);
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      setImageFile(file);
    }
  };

  const handleRemoveFile = () => {
    setImageFile(null);
    setImageId(null);
  };

  const handleSubmit = async () => {
    const isAnswerInvalid = answers.some((answer, index) => {
      const hasImage = answerImages[index] !== null;
      return !hasImage && answer.trim() === '';
    });

    if (
      !problemDescription ||
      questions.some((q) => !q) ||
      isAnswerInvalid ||
      validatedTopicId === null
    ) {
      toaster.create({
        title: `Будь ласка, заповніть усі поля або додайте зображення до варіантів.`,
        type: 'warning',
      });
      return;
    }

    // Завантаження зображення до завдання
    let uploadedImageId = imageId;
    if (imageFile) {
      try {
        uploadedImageId = await uploadFile(imageFile);
        setImageId(uploadedImageId);
      } catch {
        toaster.create({
          title: `Помилка при завантаженні зображення до завдання`,
          type: 'warning',
        });
        return;
      }
    }

    // Завантаження зображень до відповідей
    const uploadedAnswerImageIds: (string | null)[] = [];

    for (let i = 0; i < answerImages.length; i++) {
      if (answerImages[i]) {
        try {
          const uploaded = await uploadFile(answerImages[i]!);
          uploadedAnswerImageIds[i] = uploaded;
        } catch {
          toaster.create({
            title: `Помилка при завантаженні зображення до відповіді ${String.fromCharCode(1040 + i)}`,
            type: 'warning',
          });
          return;
        }
      } else {
        uploadedAnswerImageIds[i] = null;
      }
    }
    setAnswerImageIds(uploadedAnswerImageIds);

    const uploadedQuestionImageIds: (string | null)[] = [];

    for (let i = 0; i < questionImages.length; i++) {
      if (questionImages[i]) {
        try {
          const uploaded = await uploadFile(questionImages[i]!);
          uploadedQuestionImageIds[i] = uploaded;
        } catch {
          toaster.create({
            title: `Помилка при завантаженні зображення до запитання ${i + 1}`,
            type: 'warning',
          });
          return;
        }
      } else {
        uploadedQuestionImageIds[i] = null;
      }
    }
    setQuestionImageIds(uploadedQuestionImageIds);

    // Побудова тесту
    const usedCorrectIndexes = new Set(correctMatches.flat());
    const lastCorrectIndex = correctMatches[correctMatches.length - 1][0];
    const remainingIncorrectIndexes = answers
      .map((_, i) => i)
      .filter((i) => !usedCorrectIndexes.has(i));

    const test: Assignment = {
      id: 0,
      objectId: validatedTopicId,
      problemDescription,
      problemDescriptionImage: uploadedImageId || '',
      explanation: '',
      assignmentModelType: 2,
      parentAssignmentId: 0,
      answers: [], // головне завдання не має відповідей
      childAssignments: questions.map((question, index) => {
        const assignedAnswers =
          index === questions.length - 1
            ? [
                ...remainingIncorrectIndexes.map((answerIndex) => ({
                  id: 0,
                  value: uploadedAnswerImageIds[answerIndex]
                    ? String.fromCharCode(1040 + answerIndex)
                    : answers[answerIndex],
                  valueImage: uploadedAnswerImageIds[answerIndex] || '',
                  isCorrect: false,
                  points: 0,
                  assignmentId: 0,
                })),
                {
                  id: 0,
                  value: uploadedAnswerImageIds[lastCorrectIndex]
                    ? String.fromCharCode(1040 + lastCorrectIndex)
                    : answers[lastCorrectIndex],
                  valueImage: uploadedAnswerImageIds[lastCorrectIndex] || '',
                  isCorrect: true,
                  points: 1,
                  assignmentId: 0,
                },
              ]
            : correctMatches[index].map((answerIndex) => ({
                id: 0,
                value: uploadedAnswerImageIds[answerIndex]
                  ? String.fromCharCode(1040 + answerIndex)
                  : answers[answerIndex],
                valueImage: uploadedAnswerImageIds[answerIndex] || '',
                isCorrect: true,
                points: 1,
                assignmentId: 0,
              }));

        return {
          id: 0,
          objectId: validatedTopicId,
          problemDescription: uploadedQuestionImageIds[index] ? String(index + 1) : question,
          problemDescriptionImage: uploadedQuestionImageIds[index] || '',
          explanation: '',
          assignmentModelType: 3,
          parentAssignmentId: 0,
          answers: assignedAnswers,
          childAssignments: [],
        };
      }),
    };

    try {
      await addAssignments(test);
      toaster.create({
        title: `Тест успішно додано!`,
        type: 'success',
      });

      // (опційно) очистити форму:
      setProblemDescription('');
      setQuestions(['', '', '']);
      setAnswers(['', '', '', '', '']);
      setCorrectMatches(Array(3).fill([]));
      setImageFile(null);
      setImageId(null);
      setAnswerImages(Array(5).fill(null));
      setAnswerImageIds(Array(5).fill(null));
      setQuestionImages(Array(3).fill(null));
      setQuestionImageIds(Array(3).fill(null));
    } catch (error) {
      toaster.create({
        title: `Помилка при додаванні тесту: ${error}`,
        type: 'warning',
      });
    }
  };

  return (
    <Flex flexDir="column" justifyContent="center" alignItems="flex-start">
      <Field label="Завдання" required mb={3} color="orange">
        <Input
          w={{ base: '20rem', md: '30.5rem' }}
          placeholder="Введіть завдання"
          value={problemDescription}
          onChange={(e) => setProblemDescription(e.target.value)}
        />
      </Field>
      {/* Поле вибору файлу */}
      <input
        type="file"
        accept="image/*"
        ref={fileInputRef}
        onChange={handleFileChange}
        style={{ display: 'none' }}
      />

      {/* Відображення доданого файлу */}
      {imageFile ? (
        <Flex alignItems="center" mb={3}>
          <Text fontSize="sm" color="green" mr={3}>
            {imageFile.name}
          </Text>
          <Button size="xs" colorPalette="red" onClick={handleRemoveFile}>
            Видалити
          </Button>
        </Flex>
      ) : (
        <Button
          size="xs"
          colorPalette="orange"
          mb={2}
          onClick={() => fileInputRef.current?.click()}
        >
          Додати фото
        </Button>
      )}

      <Flex flexDir={{ base: 'column', xl: 'row' }} gap={8}>
        <Flex flexDir="column">
          <Text color="orange" fontSize="sm" mb={2}>
            Запитання
          </Text>
          {questions.map((question, index) => (
            <Flex key={index} flexDir="column" mb={3}>
              <Flex flexDir="row" gap={2} justifyContent="center" alignItems="center">
                <Text fontWeight="bold" color="orange" w="0.75rem">
                  {index + 1}
                </Text>
                <Input
                  w={{ base: '20rem', md: '30.5rem' }}
                  placeholder={`Запитання ${index + 1}`}
                  value={question}
                  onChange={(e) => handleQuestionChange(index, e.target.value)}
                />
              </Flex>

              {/* Додавання картинки до запитання */}
              <Flex align="center" gap={2} mt={1}>
                <input
                  type="file"
                  accept="image/*"
                  id={`question-image-${index}`}
                  style={{ display: 'none' }}
                  onChange={(e) => {
                    const file = e.target.files?.[0];
                    if (file) handleQuestionImageChange(index, file);
                  }}
                />
                <Button
                  size="xs"
                  colorPalette="orange"
                  ml={6}
                  onClick={() => document.getElementById(`question-image-${index}`)?.click()}
                >
                  Додати фото
                </Button>
                {questionImages[index] && (
                  <>
                    <Text fontSize="sm" color="green">
                      {questionImages[index]?.name}
                    </Text>
                    <Button
                      size="xs"
                      colorPalette="red"
                      onClick={() => handleRemoveQuestionImage(index)}
                    >
                      Видалити
                    </Button>
                  </>
                )}
              </Flex>
            </Flex>
          ))}
        </Flex>
        <Flex flexDir="column">
          <Text color="orange" fontSize="sm" mb={2}>
            Варіанти відповіді
          </Text>
          {answers.map((answer, index) => (
            <Flex key={index} flexDir="column" mb={4}>
              <Flex flexDir="row" gap={2} mb={1} justifyContent="center" alignItems="center">
                <Text fontWeight="bold" color="orange" w="0.75rem">
                  {String.fromCharCode(1040 + index)}
                </Text>
                <Input
                  w={{ base: '20rem', md: '30.5rem' }}
                  placeholder={`Відповідь ${String.fromCharCode(1040 + index)}`}
                  value={answers[index]}
                  onChange={(e) => handleAnswerChange(index, e.target.value)}
                />
              </Flex>

              <Flex align="center" gap={2}>
                <input
                  type="file"
                  accept="image/*"
                  id={`answer-image-${index}`}
                  style={{ display: 'none' }}
                  onChange={(e) => {
                    const file = e.target.files?.[0];
                    if (file) handleAnswerImageChange(index, file);
                  }}
                />
                <Button
                  ml={6}
                  colorPalette="orange"
                  size="xs"
                  onClick={() => document.getElementById(`answer-image-${index}`)?.click()}
                >
                  Додати фото
                </Button>
                {answerImages[index] && (
                  <>
                    <Text fontSize="sm" color="green">
                      {answerImages[index]?.name}
                    </Text>
                    <Button
                      size="xs"
                      colorPalette="red"
                      onClick={() => handleRemoveAnswerImage(index)}
                    >
                      Видалити
                    </Button>
                  </>
                )}
              </Flex>
            </Flex>
          ))}
        </Flex>
      </Flex>
      <Flex flexDir="column" alignItems="center">
        <Flex flexDir="row" gap={2} mb={2}>
          <Text w="1.5rem"></Text>
          {answers.map((_, index) => (
            <Text key={index} fontWeight="bold" color="orange" textAlign="center" w="1.5rem">
              {String.fromCharCode(1040 + index)}
            </Text>
          ))}
        </Flex>
        {questions.map((_, rowIndex) => (
          <Flex key={rowIndex} flexDir="row" alignItems="center" gap={2} mb={2}>
            <Text fontWeight="bold" color="orange" w="1.5rem" textAlign="center">
              {rowIndex + 1}
            </Text>
            {answers.map((_, colIndex) => (
              <Checkbox
                key={colIndex}
                size="lg"
                colorPalette="orange"
                checked={correctMatches[rowIndex].includes(colIndex)}
                onChange={() => toggleCorrectMatch(rowIndex, colIndex)}
              />
            ))}
          </Flex>
        ))}
      </Flex>
      <Button colorPalette="orange" size="md" mt={4} ml={7} width="9.75rem" onClick={handleSubmit}>
        Додати
      </Button>
    </Flex>
  );
};

export default MultiAnswerTypeTest;
