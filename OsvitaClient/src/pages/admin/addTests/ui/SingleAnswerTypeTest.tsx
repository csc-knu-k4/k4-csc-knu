import { useEffect, useState, useRef } from 'react';
import { Field } from '@/components/ui/field';
import { Flex, Input, Text, Button } from '@chakra-ui/react';
import { addAssignments, Assignment } from '@/shared/api/testsApi';
import { uploadFile } from '@/shared/api/mediaApi';
import { Checkbox } from '@/components/ui/checkbox';
import { toaster } from '@/components/ui/toaster';

const MAX_OPTIONS = 5;
const MIN_OPTIONS = 2;
const OPTION_LABELS = ['А', 'Б', 'В', 'Г', 'Д'];

interface SingleAnswerTypeTestProps {
  materialId: number;
}

const SingleAnswerTypeTest: React.FC<SingleAnswerTypeTestProps> = ({ materialId }) => {
  const [question, setQuestion] = useState<string>('');
  const [options, setOptions] = useState<string[]>(['', '']);
  const [correctAnswerIndex, setCorrectAnswerIndex] = useState<number | null>(null);
  const [validatedMaterialId, setValidatedMaterialId] = useState<number | null>(null);
  const [imageFile, setImageFile] = useState<File | null>(null);
  const [imageId, setImageId] = useState<string | null>(null);

  const [optionImages, setOptionImages] = useState<(File | null)[]>([null, null]);
  const [optionImageIds, setOptionImageIds] = useState<(string | null)[]>([null, null]);

  const fileInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (materialId !== null) {
      setValidatedMaterialId(materialId);
    }
  }, [materialId]);

  const handleOptionImageChange = (index: number, file: File) => {
    const newImages = [...optionImages];
    newImages[index] = file;
    setOptionImages(newImages);
  };

  const handleRemoveOptionImage = (index: number) => {
    const newImages = [...optionImages];
    const newIds = [...optionImageIds];
    newImages[index] = null;
    newIds[index] = null;
    setOptionImages(newImages);
    setOptionImageIds(newIds);
  };

  const handleOptionChange = (index: number, value: string) => {
    const newOptions = [...options];
    newOptions[index] = value;
    setOptions(newOptions);
  };

  const handleRemoveOption = (index: number) => {
    if (options.length > MIN_OPTIONS) {
      setOptions(options.filter((_, i) => i !== index));
    }
  };

  const handleAddOption = () => {
    if (options.length < MAX_OPTIONS) {
      setOptions([...options, '']);
    }
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
    const isOptionEmptyWithoutImage = options.some((opt, index) => {
      const hasImage = optionImages[index] !== null;
      return !hasImage && opt.trim() === '';
    });

    if (!question || isOptionEmptyWithoutImage || validatedMaterialId === null) {
      toaster.create({
        title: `Будь ласка, заповніть усі обов'язкові поля.`,
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
          title: `Помилка при завантаженні зображення до питання`,
          type: 'warning',
        });
        return;
      }
    }

    const uploadedOptionImageIds: (string | null)[] = [];

    for (let i = 0; i < optionImages.length; i++) {
      if (optionImages[i]) {
        try {
          const uploaded = await uploadFile(optionImages[i]!);
          uploadedOptionImageIds[i] = uploaded;
        } catch {
          toaster.create({
            title: `Помилка при завантаженні зображення до варіанту ${OPTION_LABELS[i]}`,
            type: 'warning',
          });
          return;
        }
      } else {
        uploadedOptionImageIds[i] = null;
      }
    }
    setOptionImageIds(uploadedOptionImageIds);

    const test: Assignment = {
      id: 0,
      objectId: validatedMaterialId,
      problemDescription: question,
      problemDescriptionImage: uploadedImageId || '',
      explanation: '',
      assignmentModelType: 0,
      parentAssignmentId: 0,
      answers: options.map((value, index) => {
        const hasImage = uploadedOptionImageIds[index];
        return {
          id: 0,
          value: hasImage ? OPTION_LABELS[index] : value,
          valueImage: hasImage || '',
          isCorrect: index === correctAnswerIndex,
          points: index === correctAnswerIndex ? 1 : 0,
          assignmentId: 0,
        };
      }),
      childAssignments: [],
    };

    try {
      await addAssignments(test);
      toaster.create({
        title: `Тест успішно додано!`,
        type: 'success',
      });

      setQuestion('');
      setOptions(['', '']);
      setCorrectAnswerIndex(null);
      setImageFile(null);
      setImageId(null);
      setOptionImages([null, null]);
      setOptionImageIds([null, null]);
    } catch (error) {
      toaster.create({
        title: `Помилка при додаванні тесту: ${error}`,
        type: 'warning',
      });
    }
  };

  return (
    <>
      <Field label="Запитання" required mb={3} color="orange">
        <Input
          w={{ base: '20rem', md: '30.5rem' }}
          placeholder="Введіть запитання"
          value={question}
          onChange={(e) => setQuestion(e.target.value)}
        />
      </Field>

      <input
        type="file"
        accept="image/*"
        ref={fileInputRef}
        onChange={handleFileChange}
        style={{ display: 'none' }}
      />

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

      <Text color="orange" fontSize="sm" mb={2}>
        Варіанти відповіді
      </Text>

      {options.map((option, index) => (
        <Flex key={index} flexDir="column" mb={4}>
          <Flex align="center" gap={3}>
            <Text fontWeight="bold" color="orange">
              {OPTION_LABELS[index]}
            </Text>
            <Input
              w={{ base: '20rem', md: '30.5rem' }}
              placeholder={`Варіант ${OPTION_LABELS[index]}`}
              value={option}
              onChange={(e) => handleOptionChange(index, e.target.value)}
            />
            <Checkbox
              checked={correctAnswerIndex === index}
              onCheckedChange={() => setCorrectAnswerIndex(index)}
              colorPalette="orange"
            />
            {options.length > MIN_OPTIONS && (
              <Button size="xs" colorPalette="orange" onClick={() => handleRemoveOption(index)}>
                Видалити
              </Button>
            )}
          </Flex>

          <Flex align="center" mt={1} gap={3}>
            <input
              type="file"
              accept="image/*"
              style={{ display: 'none' }}
              id={`option-image-${index}`}
              onChange={(e) => {
                const file = e.target.files?.[0];
                if (file) {
                  handleOptionImageChange(index, file);
                }
              }}
            />
            <Button
              ml={6}
              colorPalette="orange"
              size="xs"
              onClick={() => document.getElementById(`option-image-${index}`)?.click()}
            >
              Додати фото
            </Button>
            {optionImages[index] && (
              <>
                <Text fontSize="sm" color="green">
                  {optionImages[index]?.name}
                </Text>
                <Button size="xs" colorPalette="red" onClick={() => handleRemoveOptionImage(index)}>
                  Видалити
                </Button>
              </>
            )}
          </Flex>
        </Flex>
      ))}

      {options.length < MAX_OPTIONS && (
        <Button size="sm" colorPalette="orange" onClick={handleAddOption}>
          Додати варіант
        </Button>
      )}

      <Button colorPalette="orange" size="sm" ml={3} onClick={handleSubmit}>
        Зберегти
      </Button>
    </>
  );
};

export default SingleAnswerTypeTest;
