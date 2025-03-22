import { Field } from '@/components/ui/field';
import { addAssignments, Assignment } from '@/shared/api/testsApi';
import { uploadFile } from '@/shared/api/mediaApi';
import { Button, Input, Flex, Text } from '@chakra-ui/react';
import { useEffect, useState, useRef } from 'react';
import { toaster } from '@/components/ui/toaster';

interface OpenAnswerTypeTestProps {
  materialId: number;
}

const OpenAnswerTypeTest: React.FC<OpenAnswerTypeTestProps> = ({ materialId }) => {
  const [question, setQuestion] = useState<string>('');
  const [answer, setAnswer] = useState<string>('');
  const [validatedMaterialId, setValidatedMaterialId] = useState<number | null>(null);
  const [imageFile, setImageFile] = useState<File | null>(null);
  const [imageId, setImageId] = useState<string | null>(null);

  const fileInputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (materialId !== null) {
      setValidatedMaterialId(materialId);
    }
  }, [materialId]);

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
    if (!question || !answer || validatedMaterialId === null) {
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
      assignmentModelType: 1,
      parentAssignmentId: 0,
      answers: [
        {
          id: 0,
          value: answer,
          valueImage: '',
          isCorrect: true,
          points: 2,
          assignmentId: 0,
        },
      ],
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
          <Button size="sm" colorPalette="red" onClick={handleRemoveFile}>
            Видалити
          </Button>
        </Flex>
      ) : (
        <Button
          size="sm"
          colorPalette="orange"
          mb={2}
          onClick={() => fileInputRef.current?.click()}
        >
          Додати фото
        </Button>
      )}

      <Field label="Відповідь" required mb={3} color="orange">
        <Input
          w={{ base: '20rem', md: '30.5rem' }}
          placeholder="Введіть відповідь"
          value={answer}
          onChange={(e) => setAnswer(e.target.value)}
        />
      </Field>

      <Button colorPalette="orange" size="sm" onClick={handleSubmit}>
        Зберегти
      </Button>
    </>
  );
};

export default OpenAnswerTypeTest;
