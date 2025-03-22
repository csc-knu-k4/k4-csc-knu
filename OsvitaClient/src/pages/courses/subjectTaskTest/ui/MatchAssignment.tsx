import { Box, Text, Flex, Grid, GridItem } from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { Checkbox } from '@/components/ui/checkbox';
import React from 'react';

const alphabet = ['А', 'Б', 'В', 'Г', 'Д'];

const shuffle = (array: any[]) => [...array].sort(() => Math.random() - 0.5);

const MatchAssignment = ({
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
  const [variants, setVariants] = useState<any[]>([]);
  const [selected, setSelected] = useState<number[]>([]); // зберігає індекс відповіді для кожного рядка

  useEffect(() => {
    const allAnswers = data.childAssignments.flatMap((c: any) => c.answers);
    const unique = Array.from(new Map(allAnswers.map((a: any) => [a.value, a])).values());
    const shuffled = shuffle(unique);
    setVariants(shuffled);

    if (isFinished) {
      const selectedIndices = data.childAssignments.map((child: any) => {
        const userAnswer = userAnswers.find((a) => a.assignmentId === child.id);
        return shuffled.findIndex((v) => v.value === userAnswer?.answerValue);
      });
      setSelected(selectedIndices);
    } else {
      setSelected(Array(data.childAssignments.length).fill(-1));
    }
  }, [data, isFinished]);

  const handleSelect = (row: number, col: number) => {
    if (isFinished) return;

    const updated = [...selected];
    updated[row] = col;
    setSelected(updated);

    const value = variants[col]?.value || '';
    const child = data.childAssignments[row];
    onAnswer(child.id, value);
  };

  const getIsCorrect = (row: number, col: number) => {
    const child = data.childAssignments[row];
    const correct = child.answers.find((a: any) => a.isCorrect)?.value;
    return variants[col]?.value === correct;
  };

  return (
    <Box p={5} maxW="full" borderWidth={1} borderRadius="xl" boxShadow="sm">
      <Text fontSize="lg" mb={3}>
        {index + 1}. Встановіть відповідність:
      </Text>

      <Flex gap={10}>
        <Flex flexDir="column">
          {data.childAssignments.map((child: any, i: number) => (
            <Text key={`q-${child.id}`} mb={2}>
              {i + 1}. {child.problemDescription}
            </Text>
          ))}
        </Flex>

        <Flex flexDir="column">
          {variants.map((v: any, i: number) => (
            <Text key={`v-${v.id}`} mb={2}>
              {alphabet[i]} {v.value}
            </Text>
          ))}
        </Flex>
      </Flex>

      <Text fontSize="md" my={3}>
        Позначте відповіді:
      </Text>

      <Grid templateColumns={`repeat(${variants.length + 1}, 1fr)`} maxW="170px" gap={2}>
        <GridItem />
        {alphabet.slice(0, variants.length).map((opt, i) => (
          <GridItem key={`head-${i}`} fontWeight="bold" color="orange.500" textAlign="center">
            {opt}
          </GridItem>
        ))}

        {data.childAssignments.map((child: any, row: number) => (
          <React.Fragment key={`row-${row}`}>
            <GridItem fontWeight="bold" color="orange" textAlign="center">
              {row + 1}
            </GridItem>
            {variants.map((_, col: number) => {
              const isChecked = selected[row] === col;
              const isCorrect = getIsCorrect(row, col);
              return (
                <GridItem key={`cell-${row}-${col}`} textAlign="center">
                  <Checkbox
                    checked={isChecked}
                    onChange={() => handleSelect(row, col)}
                    disabled={isFinished}
                    borderColor={isFinished && isCorrect ? 'green.500' : 'gray.200'}
                    colorPalette={isFinished && isCorrect ? 'green' : 'gray.200'}
                  />
                </GridItem>
              );
            })}
          </React.Fragment>
        ))}
      </Grid>

      {isFinished && (
        <Text fontWeight="semibold" mt={3}>
          Кількість балів:{' '}
          {data.childAssignments.reduce((acc: number, child: any) => {
            const correct = child.answers.find((a: any) => a.isCorrect)?.value;
            const userAnswer = userAnswers.find((a) => a.assignmentId === child.id)?.answerValue;
            return acc + (userAnswer === correct ? 1 : 0);
          }, 0)}
        </Text>
      )}
    </Box>
  );
};

export default MatchAssignment;
