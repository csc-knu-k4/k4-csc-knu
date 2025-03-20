import { Text, Flex, Box } from '@chakra-ui/react';
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
    <Flex
      position="sticky"
      top="0"
      bg="white"
      zIndex="3"
      p={4}
      justifyContent="space-between"
      alignItems="center"
      borderBottom="1px solid #ddd"
    >
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
    <Box height="calc(100vh - 180px)" display="flex" flexDirection="column">
      {header}
      <Box flex="1" overflowY="auto" p={4}>
        {assignments && assignments.length > 0 ? (
          <AssignmentTable items={assignments} />
        ) : (
          <Text>Дані відсутні.</Text>
        )}
      </Box>
    </Box>
  );
};

export default Tests;
