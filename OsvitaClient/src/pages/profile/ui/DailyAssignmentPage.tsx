import { useParams } from 'react-router-dom';
import { Spinner, Center, Text } from '@chakra-ui/react';
import TestRenderer from '@/pages/courses/subjectTaskTest/ui/TestRenderer';

const DailyAssignmentPage = () => {
  const { id } = useParams();

  const testId = Number(id);
  const isLoading = !id;
  const isInvalid = isNaN(testId);

  if (isLoading) {
    return (
      <Center h="100vh">
        <Spinner size="xl" color="orange.400" />
      </Center>
    );
  }

  if (isInvalid) {
    return (
      <Center h="100vh">
        <Text color="red.500">Невалідний або відсутній ID щоденного завдання</Text>
      </Center>
    );
  }

  return (
    <Center py={6}>
      <TestRenderer testId={testId} />
    </Center>
  );
};

export default DailyAssignmentPage;
