import { Text, Flex } from '@chakra-ui/react';
import { AddSubjectButton } from '@/features/subjects';
import { SubjectsTable } from '@/entities/subjects';

const items = [
  { id: 1, subjectName: 'Математика', date: '20.10.2024, 16:32' },
  { id: 2, subjectName: 'Українська мова', date: '20.10.2024, 16:32' },
  { id: 3, subjectName: 'Англійська мова', date: '20.10.2024, 16:32' },
  { id: 4, subjectName: 'Історія', date: '20.10.2024, 16:32' },
  { id: 5, subjectName: 'Фізика', date: '20.10.2024, 16:32' },
];

const Subjects = () => {
  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Предмети
        </Text>
        <AddSubjectButton />
      </Flex>
      <SubjectsTable items={items} />
    </>
  );
};

export default Subjects;
