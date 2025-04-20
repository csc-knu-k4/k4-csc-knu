import { Box, Text, Input, Image } from '@chakra-ui/react';
import { useEffect, useState } from 'react';

const OpenAnswerQuestion = ({
  data,
  index,
  isFinished,
  onAnswer,
  userAnswers,
}: {
  data: any;
  index: number;
  isFinished: boolean;
  onAnswer: (assignmentId: number, value: string) => void;
  userAnswers: any[];
}) => {
  const [answer, setAnswer] = useState('');

  useEffect(() => {
    if (isFinished) {
      const existing = userAnswers.find((a) => a.assignmentId === data.id);
      if (existing) setAnswer(existing.answerValue);
    }
  }, [isFinished]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    setAnswer(val);
    onAnswer(data.id, val);
  };

  const isCorrect = answer.trim() === data.answers[0]?.value.trim();

  return (
    <Box p={5} maxW="100%" borderWidth={1} borderRadius="xl" boxShadow="sm">
      <Text fontSize="lg" mb={3}>
        {index + 1}. {data.problemDescription}
      </Text>
      {data.problemDescriptionImage && (
        <Image
          src={`${import.meta.env.VITE_IMG_URL}${data.problemDescriptionImage}`}
          alt="зображення до завдання"
          borderRadius="lg"
          maxW="100%"
          maxH="300px"
          objectFit="contain"
          mt={4}
        />
      )}
      <Text fontSize="md" my={3}>
        Впишіть відповідь:
      </Text>
      <Input
        colorPalette="orange"
        borderColor={isFinished && isCorrect ? 'green.500' : 'gray.200'}
        borderRadius="2xl"
        maxW="sm"
        value={answer}
        onChange={handleInputChange}
        disabled={isFinished}
      />

      {isFinished && (
        <Text fontWeight="semibold" mt={3}>
          Кількість балів: {isCorrect ? 1 : 0}
        </Text>
      )}
    </Box>
  );
};

export default OpenAnswerQuestion;
