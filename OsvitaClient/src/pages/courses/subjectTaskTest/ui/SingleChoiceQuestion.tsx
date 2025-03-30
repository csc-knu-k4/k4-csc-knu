import { Box, Text, HStack, Highlight, Flex, Image } from '@chakra-ui/react';
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
  const allAnswersAreImages = shuffledAnswers.every((ans) => ans.valueImage);

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
      {data.problemDescriptionImage && (
        <Image
          src={`${window.location.origin}${data.problemDescriptionImage}`}
          alt="зображення до завдання"
          borderRadius="lg"
          maxW="100%"
          maxH="300px"
          objectFit="contain"
          mt={4}
          mb={5}
        />
      )}
      {allAnswersAreImages ? (
        <Flex direction="column" align="flex-start" gap={4} mb={4}>
          {shuffledAnswers.map((ans, i) => (
            <Flex key={`highlight-${ans.id}`} align="center" gap={4}>
              <Text fontWeight="bold" color="orange" minW="1.5rem">
                {alphabet[i]}
              </Text>
              <Image
                src={`${window.location.origin}${ans.valueImage}`}
                alt={`Варіант ${alphabet[i]}`}
                borderRadius="md"
                maxW="200px"
                maxH="140px"
                objectFit="contain"
              />
            </Flex>
          ))}
        </Flex>
      ) : (
        <HStack align="start" wrap="wrap">
          {shuffledAnswers.map((ans, i) => (
            <Box key={`highlight-${ans.id}`} textAlign="center" mr={4} mb={2}>
              {ans.valueImage ? (
                <>
                  <Image
                    src={`${window.location.origin}${ans.valueImage}`}
                    alt={`Варіант ${alphabet[i]}`}
                    borderRadius="md"
                    maxW="120px"
                    maxH="100px"
                    objectFit="contain"
                  />
                  <Text fontWeight="semibold" color="orange" mt={1}>
                    {alphabet[i]}
                  </Text>
                </>
              ) : (
                <Highlight query={alphabet[i]} styles={{ fontWeight: 'semibold', color: 'orange' }}>
                  {`${alphabet[i]} ${ans.value}`}
                </Highlight>
              )}
            </Box>
          ))}
        </HStack>
      )}

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
                borderColor={isFinished && isCorrect ? 'green.500' : 'orange'}
                colorPalette={isFinished && isCorrect ? 'green' : 'orange'}
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
