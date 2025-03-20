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

  const fileInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (materialId !== null) {
      setValidatedTopicId(materialId);
    }
  }, [materialId]);

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
    if (
      !problemDescription ||
      questions.some((q) => !q) ||
      answers.some((a) => !a) ||
      validatedTopicId === null
    ) {
      toaster.create({
        title: `Будь ласка, заповніть усі поля.`,
        type: 'warning',
      });
      return;
    }

    let uploadedImageId = imageId;
    if (imageFile) {
      try {
        uploadedImageId = await uploadFile(imageFile);
        setImageId(uploadedImageId);
      } catch {
        toaster.create({
          title: `Помилка при завантаженні зображення`,
          type: 'warning',
        });
        return;
      }
    }

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
      answers: [],
      childAssignments: questions.map((question, index) => {
        const assignedAnswers =
          index === questions.length - 1
            ? [
                ...remainingIncorrectIndexes.map((answerIndex) => ({
                  id: 0,
                  value: answers[answerIndex],
                  isCorrect: false,
                  points: 0,
                  assignmentId: 0,
                })),
                {
                  id: 0,
                  value: answers[lastCorrectIndex],
                  isCorrect: true,
                  points: 1,
                  assignmentId: 0,
                },
              ]
            : correctMatches[index].map((answerIndex) => ({
                id: 0,
                value: answers[answerIndex],
                isCorrect: true,
                points: 1,
                assignmentId: 0,
              }));

        return {
          id: 0,
          objectId: validatedTopicId,
          problemDescription: question,
          problemDescriptionImage: uploadedImageId || '',
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
    } catch (error) {
      toaster.create({
        title: `Помилка при додаванні тесту ${error}`,
        type: 'warning',
      });
    }
  };

  return (
    <Flex flexDir="column" justifyContent="center" alignItems="flex-start">
      <Field label="Завдання" required mb={3} color="orange">
        <Input
          width="30.5rem"
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
          <Button size="sm" colorPalette="red" onClick={handleRemoveFile}>
            Видалити
          </Button>
        </Flex>
      ) : (
        <Button colorPalette="orange" mb={2} onClick={() => fileInputRef.current?.click()}>
          Додати фото
        </Button>
      )}

      <Flex flexDir="row" gap={8}>
        <Flex flexDir="column">
          <Text color="orange" fontSize="sm" mb={2}>
            Запитання
          </Text>
          {questions.map((question, index) => (
            <Flex
              key={index}
              flexDir="row"
              gap={2}
              mb={3}
              justifyContent="center"
              alignItems="center"
            >
              <Text fontWeight="bold" color="orange" w="0.75rem">
                {index + 1}
              </Text>
              <Input
                width="30.5rem"
                placeholder={`Запитання ${index + 1}`}
                value={question}
                onChange={(e) => handleQuestionChange(index, e.target.value)}
              />
            </Flex>
          ))}
        </Flex>
        <Flex flexDir="column">
          <Text color="orange" fontSize="sm" mb={2}>
            Варіанти відповіді
          </Text>
          {answers.map((answer, index) => (
            <Flex
              key={index}
              flexDir="row"
              gap={2}
              mb={3}
              justifyContent="center"
              alignItems="center"
            >
              <Text fontWeight="bold" color="orange" w="0.75rem">
                {String.fromCharCode(1040 + index)}
              </Text>
              <Input
                width="30.5rem"
                placeholder={`Відповідь ${String.fromCharCode(1040 + index)}`}
                value={answer}
                onChange={(e) => handleAnswerChange(index, e.target.value)}
              />
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
