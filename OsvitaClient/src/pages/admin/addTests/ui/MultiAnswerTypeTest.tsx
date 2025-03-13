import { Checkbox } from '@/components/ui/checkbox';
import { Button, Flex, Input, Text } from '@chakra-ui/react';

const questions = ['Запитання 1', 'Запитання 2', 'Запитання 3', 'Запитання 4'];
const answers = ['Відповідь А', 'Відповідь Б', 'Відповідь В', 'Відповідь Г', 'Відповідь Д'];
const answerLabels = ['A', 'Б', 'В', 'Г', 'Д'];

interface MultiTypeTestProps {
  materialId: number;
}

const MultiAnswerTypeTest: React.FC<MultiTypeTestProps> = ({ materialId }) => {
  return (
    <Flex flexDir="column" justifyContent="center" alignItems="flex-start">
      <Flex flexDir="row" gap={8}>
        <Flex flexDir="column">
          <Text color="orange" fontSize="sm" mb={2}>
            Запитання
          </Text>
          {questions.map((placeholder, index) => (
            <Flex
              key={index}
              flexDir="row"
              justifyContent="center"
              alignItems="center"
              gap={2}
              mb={3}
            >
              <Text fontWeight="bold" color="orange" w="0.75rem">
                {index + 1}
              </Text>
              <Input width="30.5rem" placeholder={placeholder} />
            </Flex>
          ))}
        </Flex>

        <Flex flexDir="column">
          <Text color="orange" fontSize="sm" mb={2}>
            Варіанти відповіді
          </Text>
          {answers.map((placeholder, index) => (
            <Flex
              key={index}
              flexDir="row"
              justifyContent="center"
              alignItems="center"
              gap={2}
              mb={3}
            >
              <Text fontWeight="bold" color="orange" w="0.75rem">
                {answerLabels[index]}
              </Text>
              <Input width="30.5rem" placeholder={placeholder} />
            </Flex>
          ))}
        </Flex>
      </Flex>

      <Flex flexDir="column" alignItems="center">
        <Flex flexDir="row" gap={2} mb={2}>
          <Text w="1.5rem"></Text>
          {answerLabels.map((label, index) => (
            <Text key={index} fontWeight="bold" color="orange" textAlign="center" w="1.5rem">
              {label}
            </Text>
          ))}
        </Flex>
        {questions.map((_, rowIndex) => (
          <Flex key={rowIndex} flexDir="row" alignItems="center" gap={2} mb={2}>
            <Text fontWeight="bold" color="orange" w="1.5rem" textAlign="center">
              {rowIndex + 1}
            </Text>
            {answerLabels.map((_, colIndex) => (
              <Checkbox key={colIndex} size="lg" colorPalette="orange" variant="outline" />
            ))}
          </Flex>
        ))}
      </Flex>

      <Button colorPalette="orange" size="md" mt={4} ml={7} width="9.75rem">
        Додати
      </Button>
    </Flex>
  );
};

export default MultiAnswerTypeTest;
