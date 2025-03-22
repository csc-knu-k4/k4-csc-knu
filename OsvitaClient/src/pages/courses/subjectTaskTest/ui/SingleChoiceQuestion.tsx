import { Box, Text, HStack, Highlight, Flex } from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { Checkbox } from '@/components/ui/checkbox';

const alphabet = ['А', 'Б', 'В', 'Г', 'Д', 'Е'];

const shuffle = (array: any[]) => [...array].sort(() => Math.random() - 0.5);

const SingleChoiceQuestion = ({
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
  const [shuffledAnswers, setShuffledAnswers] = useState<any[]>([]);
  const [selected, setSelected] = useState<string | null>(null);

  useEffect(() => {
    const shuffled = shuffle(data.answers);
    setShuffledAnswers(shuffled);

    if (isFinished) {
      const existing = userAnswers.find((a) => a.assignmentId === data.id);
      if (existing) setSelected(existing.answerValue);
    }
  }, [data, isFinished]);

  const handleSelect = (ans: any) => {
    if (isFinished) return;
    setSelected(ans.value);
    onAnswer(data.id, ans.value);
  };

  return (
    <Box p={5} maxW="100%" borderWidth={1} borderRadius="xl" boxShadow="sm">
      <Text fontSize="lg" mb={3}>
        {index + 1}. {data.problemDescription}
      </Text>

      <HStack align="start" wrap="wrap">
        {shuffledAnswers.map((ans, i) => (
          <Highlight
            key={`highlight-${ans.id}`}
            query={alphabet[i]}
            styles={{ fontWeight: 'semibold', color: 'orange' }}
          >
            {`${alphabet[i]} ${ans.value}`}
          </Highlight>
        ))}
      </HStack>

      <Text fontSize="md" my={3}>
        Позначте відповідь:
      </Text>

      <HStack>
        {shuffledAnswers.map((ans, i) => {
          const isCorrect = ans.isCorrect;
          const isChecked = isFinished ? isCorrect : selected === ans.value;

          return (
            <Flex
              key={`checkbox-${ans.id}`}
              flexDir="column"
              justifyContent="center"
              alignItems="center"
            >
              <Text fontWeight="semibold" color="orange">
                {alphabet[i]}
              </Text>
              <Checkbox
                checked={isChecked}
                disabled={isFinished}
                onChange={() => handleSelect(ans)}
                borderColor={isFinished && isCorrect ? 'green.500' : 'gray.300'}
                colorPalette={isFinished && isCorrect ? 'green' : 'gray.200'}
              />
            </Flex>
          );
        })}
      </HStack>

      {isFinished && (
        <Text fontWeight="semibold" mt={3}>
          Кількість балів:{' '}
          {selected && data.answers.find((a: any) => a.value === selected && a.isCorrect) ? 1 : 0}
        </Text>
      )}
    </Box>
  );
};

export default SingleChoiceQuestion;
