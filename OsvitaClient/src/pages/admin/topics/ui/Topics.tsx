import { Text, Flex } from '@chakra-ui/react';
import { AddTopicButton } from '@/features/topics';
import { TopicsTable } from '@/entities/topics';

const items = [
  { id: 1, topic: 'Властивості коренів', sectionName: 'Числа і вирази', date: '20.10.2024, 16:32' },
  {
    id: 2,
    topic: 'Властивості дій з дійсними числа...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
  {
    id: 3,
    topic: 'Правила порівняння дійсними чис...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
  {
    id: 4,
    topic: 'Правила знаходження найбільшог...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
  {
    id: 5,
    topic: 'Модуль дійсного числа та ймовір...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
];

const Topics = () => {
  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Теми
        </Text>
        <AddTopicButton />
      </Flex>
      <TopicsTable items={items} />
    </>
  );
};

export default Topics;
