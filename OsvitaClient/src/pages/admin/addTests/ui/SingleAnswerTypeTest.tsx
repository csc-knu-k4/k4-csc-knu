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

  const fileInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (materialId !== null) {
      setValidatedMaterialId(materialId);
    }
  }, [materialId]);

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
    if (!question || options.some((opt) => opt === '') || validatedMaterialId === null) {
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

    const test: Assignment = {
      id: 0,
      objectId: validatedMaterialId,
      problemDescription: question,
      problemDescriptionImage: uploadedImageId || '',
      explanation: '',
      assignmentModelType: 0,
      parentAssignmentId: 0,
      answers: options.map((value, index) => ({
        id: 0,
        value,
        isCorrect: index === correctAnswerIndex,
        points: index === correctAnswerIndex ? 1 : 0,
        assignmentId: 0,
      })),
      childAssignments: [],
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
    <>
      <Field label="Запитання" required mb={3} color="orange">
        <Input
          w={{ base: '20rem', md: '30.5rem' }}
          placeholder="Введіть запитання"
          value={question}
          onChange={(e) => setQuestion(e.target.value)}
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

      <Text color="orange" fontSize="sm" mb={2}>
        Варіанти відповіді
      </Text>

      {options.map((option, index) => (
        <Flex key={index} flexDir="row" alignItems="center" gap={3} mb={3}>
          <Text fontWeight="bold" color="orange" w="0.75rem">
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
            <Button size="sm" colorPalette="orange" onClick={() => handleRemoveOption(index)}>
              Видалити
            </Button>
          )}
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
