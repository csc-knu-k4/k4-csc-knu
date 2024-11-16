import { useQuery } from 'react-query';
import { getSubjects } from '@/shared/api/subjectsApi';
import { Text, Flex } from '@chakra-ui/react';
import { AddSubjectButton } from '@/features/subjects';
import { SubjectsTable } from '@/entities/subjects';

const Subjects = () => {
  const { data: subjects, isLoading } = useQuery(['subjects'], getSubjects);

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }
  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Предмети
        </Text>
        <AddSubjectButton />
      </Flex>
      <SubjectsTable items={subjects} />
    </>
  );
};

export default Subjects;
