import { Field } from '@/components/ui/field';
import { addAssignments, Assignment } from '@/shared/api/testsApi';
import { Button, Input } from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { toaster } from '@/components/ui/toaster';

interface SingleAnswerTypeTestProps {
  materialId: number;
}

const OpenAnswerTypeTest: React.FC<SingleAnswerTypeTestProps> = ({ materialId }) => {
  const [question, setQuestion] = useState<string>('');
  const [answer, setAnswer] = useState<string>('');
  const [validatedMaterialId, setValidatedMaterialId] = useState<number | null>(null);

  useEffect(() => {
    if (materialId !== null) {
      setValidatedMaterialId(materialId);
    }
  }, [materialId]);

  const handleSubmit = async () => {
    if (!question || !answer || validatedMaterialId === null) {
      toaster.create({
        title: `Будь ласка, заповніть усі поля.`,
        type: 'warning',
      });

      return;
    }

    const test: Assignment = {
      id: 0,
      objectId: validatedMaterialId,
      problemDescription: question,
      explanation: '',
      assignmentModelType: 1,
      parentAssignmentId: 0,
      answers: [
        {
          id: 0,
          value: answer,
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
          width="30.5rem"
          placeholder="Введіть запитання"
          onChange={(e) => setQuestion(e.target.value)}
        />
      </Field>
      <Field label="Відповідь" required mb={3} color="orange">
        <Input
          width="30.5rem"
          placeholder="Введіть відповідь"
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
