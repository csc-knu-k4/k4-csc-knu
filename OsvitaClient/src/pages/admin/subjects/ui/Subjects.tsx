import { useQuery } from 'react-query';
import { getSubjects } from '@/shared/api/subjectsApi';
import { Text, Flex } from '@chakra-ui/react';
import { AddSubjectButton } from '@/features/subjects';
import { SubjectsTable } from '@/entities/subjects';

const Subjects = () => {
  const { data: subjects, isLoading, isError } = useQuery(['subjects'], getSubjects);

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  if (isError) {
    return <Text color="red.500">Помилка завантаження даних.</Text>;
  }

  const items = subjects || [];

  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Предмети
        </Text>
        <AddSubjectButton />
      </Flex>
      {items.length > 0 ? <SubjectsTable items={items} /> : <Text>Дані відсутні.</Text>}
    </>
  );
};

export default Subjects;
