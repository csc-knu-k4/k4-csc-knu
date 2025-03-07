import { Box, HStack, Text, Highlight, Flex, Input, GridItem, Grid } from '@chakra-ui/react';
import React, { useState } from 'react';
import { Checkbox } from '@/components/ui/checkbox';

const options = ['A', 'Б', 'В', 'Г', 'Д'];

const equations = [
  '(2+3)+5 = 2+(3+5)',
  '(2⋅3)+5 = 2⋅(3+5)',
  '2+3 = 3+2',
  '(2⋅3)+5 = 2⋅(3+5)',
  '(2⋅3)+5 = 2⋅(3+5)',
];

const multiOptions = ['А 24', 'Б 12', 'В 28', 'Г 7', 'Д 16'];

const tasks = [
  'Обчисліть значення виразу: 5x−3, якщо x=2',
  'Розв’яжіть рівняння: 3y+7=16',
  'Знайдіть площу прямокутника зі сторонами 4 см і 6 см',
  'Визначте периметр квадрата зі стороною 7 см.',
];

const SubjectTaskTest = () => {
  const [answers, setAnswers] = useState<Record<string, boolean>>(
    Object.fromEntries(options.map((key) => [key, false])),
  );

  const [multiAnswers, setMultiAnswers] = useState(
    Array(tasks.length)
      .fill(null)
      .map(() => Array(multiOptions.length).fill(false)),
  );

  const handleMultiCheckboxChange = (taskIndex: number, optionIndex: number) => {
    setMultiAnswers((prev) => {
      const newAnswers = prev.map((row) => [...row]);
      newAnswers[taskIndex][optionIndex] = !newAnswers[taskIndex][optionIndex];
      return newAnswers;
    });
  };

  const handleCheckboxChange = (option: string) => {
    setAnswers((prev) => ({ ...prev, [option]: !prev[option] }));
  };

  return (
    <Flex flexDir="column" gap={5}>
      <Box p={5} maxW="100%" borderWidth={1} borderRadius="xl" boxShadow="sm">
        <Text fontSize="lg" mb={3}>
          Яке з наступних рівнянь є прикладом асоціативної властивості додавання?
        </Text>
        <HStack align="start">
          {options.map((key, index) => (
            <Highlight
              key={`${key}-${index}`}
              query={key}
              styles={{ fontWeight: 'semibold', color: 'orange' }}
            >
              {`${key} ${equations[index]}`}
            </Highlight>
          ))}
        </HStack>
        <Text fontSize="md" my={3}>
          Позначте відповідь:
        </Text>
        <HStack>
          {options.map((key, index) => (
            <Flex
              key={`${key}-${index}`}
              flexDir="column"
              justifyContent="center"
              alignItems="center"
            >
              <Text fontWeight="semibold" color="orange">
                {key}
              </Text>
              <Checkbox
                variant="outline"
                colorPalette="orange"
                checked={answers[key]}
                onChange={() => handleCheckboxChange(key)}
              />
            </Flex>
          ))}
        </HStack>
      </Box>
      <Box p={5} maxW="100%" borderWidth={1} borderRadius="xl" boxShadow="sm">
        <Text fontSize="lg" mb={3}>
          Яке з наступних рівнянь є прикладом асоціативної властивості додавання?
        </Text>
        <Text fontSize="md" my={3}>
          Впишіть відповідь:
        </Text>
        <Input colorPalette="orange" borderColor="orange" borderRadius="2xl" maxW="sm"></Input>
      </Box>

      <Box p={5} maxW="full" borderWidth={1} borderRadius="xl" boxShadow="sm">
        <Text fontSize="lg" mb={3}>
          Встановіть відповідність:
        </Text>
        <Flex flexDir="row" gap={10}>
          <Flex flexDir="column">
            {tasks.map((task, i) => (
              <Text key={i} mb={2}>
                {i + 1}. {task}
              </Text>
            ))}
          </Flex>
          <Flex flexDir="column">
            {multiOptions.map((task, i) => (
              <Text key={i} mb={2}>
                {task}
              </Text>
            ))}
          </Flex>
        </Flex>

        <Text fontSize="md" my={3}>
          Позначте відповіді:
        </Text>

        <Grid templateColumns={`repeat(${options.length + 1}, 1fr)`} maxW="170px">
          <GridItem></GridItem>
          {options.map((opt, i) => (
            <GridItem key={i} fontWeight="bold" color="orange.500" textAlign="center">
              {opt}
            </GridItem>
          ))}

          {tasks.map((_, taskIndex) => (
            <React.Fragment key={taskIndex}>
              <GridItem fontWeight="bold" color="orange" textAlign="center">
                {taskIndex + 1}
              </GridItem>
              {multiOptions.map((_, optionIndex) => (
                <GridItem key={`${taskIndex}-${optionIndex}`} textAlign="center">
                  <Checkbox
                    variant="outline"
                    colorPalette="orange"
                    checked={multiAnswers[taskIndex][optionIndex]}
                    onChange={() => handleMultiCheckboxChange(taskIndex, optionIndex)}
                  />
                </GridItem>
              ))}
            </React.Fragment>
          ))}
        </Grid>
      </Box>
    </Flex>
  );
};

export default SubjectTaskTest;
