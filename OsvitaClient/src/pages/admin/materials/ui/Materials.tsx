import { Text, Flex, Box } from '@chakra-ui/react';
import { AddMaterialButton } from '@/features/materials';
import { MaterialTable } from '@/entities/materials';
import { getMaterials, getMaterialsByTopic } from '@/shared/api/materialsApi';
import { useQuery } from 'react-query';
import { useLocation } from 'react-router-dom';

const Materials = () => {
  const location = useLocation();
  const topicId = location.state?.topicId;

  const {
    data: materials,
    isLoading,
    isError,
  } = useQuery(
    ['topics', topicId],
    () => (topicId ? getMaterialsByTopic(topicId) : getMaterials()),
    { keepPreviousData: true },
  );

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
        {topicId ? `Матеріали для теми #${topicId}` : 'Матеріали'}
      </Text>
      <AddMaterialButton />
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
        {materials && materials.length > 0 ? (
          <MaterialTable items={materials} />
        ) : (
          <Text>Дані відсутні.</Text>
        )}
      </Box>
    </Box>
  );
};

export default Materials;
