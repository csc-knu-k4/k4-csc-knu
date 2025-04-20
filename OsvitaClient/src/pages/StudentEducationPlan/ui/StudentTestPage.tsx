import { useParams } from 'react-router-dom';
import { Box, Text } from '@chakra-ui/react';
import TestRenderer from '@/pages/courses/subjectTaskTest/ui/TestRenderer';

const StudentTestPage = () => {
  const { assignmentSetId } = useParams<{ assignmentSetId: string }>();

  if (!assignmentSetId) {
    return <Text color="red.500">Не вказано ID тесту</Text>;
  }

  return (
    <Box p={4}>
      <TestRenderer testId={Number(assignmentSetId)} />
    </Box>
  );
};

export default StudentTestPage;
