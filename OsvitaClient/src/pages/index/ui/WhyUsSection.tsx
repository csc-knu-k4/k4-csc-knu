import { Flex, Text, Highlight } from '@chakra-ui/react';
import { VscBook } from 'react-icons/vsc';

const boxData = [
  {
    title: 'Актуальні матеріали',
    description:
      'Тут зібрані актуальні матеріали, що включають теоретичні виклади, приклади тестових завдань та практичні поради.',
  },
  {
    title: 'Власний навчальний план',
    description: 'Пройдіть тест, який створить план підготовки до НМТ на основі ваших знань.',
  },
  {
    title: 'Доступність в будь-який час',
    description:
      'Доступ до матеріалів та тестів для підготовки відкритий 24/7, що дозволяє вчитися у своєму темпі та відповідно до власного графіка.',
  },
  {
    title: 'Відстеження прогресу',
    description:
      'Інтерактивна статистика дозволить вам бачити, які теми вже освоєні, а над якими варто ще попрацювати.',
  },
];

const WhyUsSection = () => {
  return (
    <>
      <Flex flexDir="column" justifyContent="center" alignItems="center" mt="130px">
        <Text fontSize="2xl" fontWeight="bold">
          Чому ми та які наші{' '}
          <Highlight query="переваги?" styles={{ color: '#FF6D18' }}>
            переваги?
          </Highlight>
        </Text>
        <Text maxW="42rem" mt={3} textAlign="center">
          Ми розуміємо, що підготовка до НМТ може бути складною. Наша платформа зробить цей процес
          захопливим та ефективним, щоб ти міг зосередитись на результаті!
        </Text>
      </Flex>
      <Flex flexDir="row" justifyContent="space-between" gap={6} mt={9}>
        {boxData.map((box, index) => (
          <Flex
            key={index}
            w="17.6rem"
            h="12.2rem"
            border="0.06rem solid #5C6CFF"
            borderRadius="1.25rem"
            boxShadow="0rem 0rem 0.5rem 0rem #5C6CFF"
            flexDir="column"
          >
            <Flex
              bgColor="#FF6D18"
              justifyContent="center"
              alignItems="center"
              boxShadow="inset 0.13rem 0.13rem 0.94rem 0rem rgba(0, 0, 0, 0.25);"
              px={4}
              borderRadius="1.25rem 0rem 1.25rem 0rem"
              w="5rem"
              h="3.5rem"
            >
              <VscBook size="2rem" color="white" />
            </Flex>
            <Flex flexDir="column" mx={5}>
              <Text fontWeight="bold" mt={4}>
                {box.title}
              </Text>
              <Text mt={1} fontSize="xs" lineHeight="1rem">
                {box.description}
              </Text>
            </Flex>
          </Flex>
        ))}
      </Flex>
    </>
  );
};

export default WhyUsSection;
