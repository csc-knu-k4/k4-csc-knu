import { useEffect, useState } from 'react';
import { Field } from '@/components/ui/field';
import { Flex, Input, Text, Button } from '@chakra-ui/react';
import { addAssignments, Assignment } from '@/shared/api/testsApi';
import { Checkbox } from '@/components/ui/checkbox';

const MAX_OPTIONS = 5;
const MIN_OPTIONS = 2;
const OPTION_LABELS = ['А', 'Б', 'В', 'Г', 'Д'];

interface SingleAnswerTypeTestProps {
  topicId: number;
}

const SingleAnswerTypeTest: React.FC<SingleAnswerTypeTestProps> = ({ topicId }) => {
  const [question, setQuestion] = useState<string>('');
  const [options, setOptions] = useState<string[]>(['', '']);
  const [correctAnswerIndex, setCorrectAnswerIndex] = useState<number | null>(null);
  const [validatedTopicId, setValidatedTopicId] = useState<number | null>(null);

  useEffect(() => {
    if (topicId !== null) {
      setValidatedTopicId(topicId);
      console.log(topicId);
    }
  }, [topicId]);

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

  const handleSubmit = async () => {
    if (!question || options.some((opt) => opt === '') || validatedTopicId === null) {
      alert('Будь ласка, заповніть усі поля.');
      return;
    }

    const test: Assignment = {
      id: 0,
      objectId: validatedTopicId,
      problemDescription: question,
      explanation: '',
      assignmentModelType: 0,
      parentAssignmentId: 0,
      answers: [
        ...options.map((value, index) => ({
          id: 0,
          value,
          isCorrect: index === correctAnswerIndex,
          points: index === correctAnswerIndex ? 1 : 0,
          assignmentId: 0,
        })),
      ] as [
        {
          id: number;
          value: string;
          isCorrect: boolean;
          points: number;
          assignmentId: number;
        },
      ],
      childAssignments: [],
    };

    try {
      await addAssignments(test);
      alert('Тест успішно додано!');
    } catch (error) {
      console.error(error);
      alert('Помилка при додаванні тесту.');
    }
  };

  return (
    <>
      <Field label="Запитання" required mb={3} color="orange">
        <Input
          width="30.5rem"
          placeholder="Введіть запитання"
          value={question}
          onChange={(e) => setQuestion(e.target.value)}
        />
      </Field>
      <Text color="orange" fontSize="sm" mb={2}>
        Варіанти відповіді
      </Text>
      {options.map((option, index) => (
        <Flex key={index} flexDir="row" alignItems="center" gap={3} mb={3}>
          <Text fontWeight="bold" color="orange" w="0.75rem">
            {OPTION_LABELS[index]}
          </Text>
          <Input
            width="30.5rem"
            placeholder={`Варіант ${OPTION_LABELS[index]}`}
            value={option}
            onChange={(e) => handleOptionChange(index, e.target.value)}
          />
          <Checkbox
            checked={correctAnswerIndex === index}
            onCheckedChange={() => setCorrectAnswerIndex(index)}
            colorScheme="orange"
          />
          {options.length > MIN_OPTIONS && (
            <Button size="sm" colorScheme="red" onClick={() => handleRemoveOption(index)}>
              Видалити
            </Button>
          )}
        </Flex>
      ))}
      {options.length < MAX_OPTIONS && (
        <Button size="sm" colorScheme="orange" onClick={handleAddOption}>
          Додати варіант
        </Button>
      )}
      <Button colorScheme="orange" size="sm" ml={3} onClick={handleSubmit}>
        Зберегти
      </Button>
    </>
  );
};

export default SingleAnswerTypeTest;
