import { useQuery } from 'react-query';
import { getSubjects } from '@/shared/api/subjectsApi';
import { Text, Flex, Box } from '@chakra-ui/react';
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
    <Box height="calc(100vh - 180px)" display="flex" flexDirection="column">
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
          Предмети
        </Text>
        <AddSubjectButton />
      </Flex>
      <Box flex="1" overflowY="auto" p={4}>
        {items.length > 0 ? <SubjectsTable items={items} /> : <Text>Дані відсутні.</Text>}
      </Box>
    </Box>
  );
};

export default Subjects;
