import { Text, Flex } from '@chakra-ui/react';
import { useQuery } from 'react-query';
import { useLocation } from 'react-router-dom';
import { getAssignments } from '@/shared/api/testsApi';
import { AssignmentTable } from '@/entities/assignments';
import { AddTestButton } from '@/features/assingmentsFeatures';

const Tests = () => {
  const location = useLocation();
  const topicId = location.state?.topicId;

  const {
    data: assignments,
    isLoading,
    isError,
  } = useQuery(['topics', topicId], () => getAssignments(), { keepPreviousData: true });

  const header = (
    <Flex justifyContent="space-between" alignItems="center" mb={2}>
      <Text fontSize="2xl" fontWeight="medium">
        {topicId ? `Тести для теми #${topicId}` : 'Тести'}
      </Text>
      <AddTestButton />
    </Flex>
  );

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  if (isError) {
    return (
      <>
        {header}
        <Text color="red.500">Помилка завантаження даних.</Text>
      </>
    );
  }

  return (
    <>
      {header}
      {assignments && assignments.length > 0 ? (
        <AssignmentTable items={assignments} />
      ) : (
        <Text>Дані відсутні.</Text>
      )}
    </>
  );
};

export default Tests;
