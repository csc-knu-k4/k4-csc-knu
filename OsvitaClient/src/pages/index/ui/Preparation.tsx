import PreparationPicture from '@/shared/assets/PreparationPicture';
import { Flex, Text, Highlight, VStack, Circle, Box } from '@chakra-ui/react';

const steps = [
  {
    number: 1,
    title: 'Реєстрація',
    description:
      'Після успішної реєстрації надається доступ до особистого кабінету, де можна зберігати результати тестів, стежити за прогресом та вибирати навчальні теми.',
  },
  {
    number: 2,
    title: 'Вибір теми',
    description:
      'Теми організовані за основними розділами офіційної програми підготовки до НМТ. Можна пройти тест, який надасть власний план проходження тем, або можна обрати конкретні розділи та завдання.',
  },
  {
    number: 3,
    title: 'Проходження тестів',
    description:
      'Кожен тест складається з питань різного типу, які охоплюють тематику НМТ. Після завершення тестування система надає правильні та неправильні відповіді.',
  },
  {
    number: 4,
    title: 'Відстежування результату',
    description:
      'Можна переглянути який матеріал вже пройдений та на скільки балів складений тест. Відображаються загальні бали та успіхи по окремих темах. Це дозволяє визначити свої слабкі та сильні сторони і сконцентруватися на покращенні потрібних навичок.',
  },
];

const Preparation = () => {
  return (
    <Flex justifyContent="center" alignItems="center" flexDir="row" bgColor="orange.100" mt="130px">
      <PreparationPicture />
      <Flex flexDir="column">
        <Text fontSize="2xl" fontWeight="bold" mt={10} ml={5}>
          Як проходить{' '}
          <Highlight query="підготовка ?" styles={{ color: 'orange' }}>
            підготовка ?
          </Highlight>
        </Text>
        <Flex direction="column" align="center" mt={3} p={5} maxW="600px" mx="auto">
          <VStack align="stretch" gap={5} position="relative">
            {steps.map((step, index) => (
              <Flex key={step.number} align="flex-start" position="relative">
                <Flex direction="column" align="center" mr={4} position="relative">
                  <Circle
                    size="3.15rem"
                    bg="orange"
                    color="white"
                    fontWeight="bold"
                    fontSize="lg"
                    zIndex="1"
                  >
                    {step.number}
                  </Circle>
                  {index < steps.length - 1 && (
                    <Box position="absolute" top="2rem" w="2px" h="calc(100% + 4rem)" bg="orange" />
                  )}
                </Flex>
                <Box pl={4}>
                  <Text fontWeight="bold" fontSize="xl" mb={2}>
                    {step.title}
                  </Text>
                  <Text fontSize="xs">{step.description}</Text>
                </Box>
              </Flex>
            ))}
          </VStack>
        </Flex>
      </Flex>
    </Flex>
  );
};

export default Preparation;
