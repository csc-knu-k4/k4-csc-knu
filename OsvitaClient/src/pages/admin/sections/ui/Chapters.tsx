import { Text, Flex } from '@chakra-ui/react';
import { AddChapterButton } from '@/features/sections';
import { SectionsTable } from '@/entities/sections';

const items = [
  { id: 1, topic: 'Числа і вирази', subjectName: 'Математика', date: '20.10.2024, 16:32' },
  {
    id: 2,
    topic: 'Дійсні числа (натуральні, раціональні та ірраціональні...',
    subjectName: 'Математика',
    date: '20.10.2024, 16:32',
  },
  {
    id: 3,
    topic: 'Відношення та пропорції. Відсотки. Основні задачі на...',
    subjectName: 'Математика',
    date: '20.10.2024, 16:32',
  },
  {
    id: 4,
    topic: 'Раціональні, ірраціональні, степеневі, показникові, ...',
    subjectName: 'Математика',
    date: '20.10.2024, 16:32',
  },
  {
    id: 5,
    topic: 'Лінійні, квадратні, раціональні, ірраціональні, показ...',
    subjectName: 'Математика',
    date: '20.10.2024, 16:32',
  },
];

const Sections = () => {
  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Розділи
        </Text>
        <AddChapterButton />
      </Flex>
      <SectionsTable items={items} />
    </>
  );
};

export default Sections;
