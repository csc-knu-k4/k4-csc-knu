import { Text, Flex } from '@chakra-ui/react';
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
    <Flex justifyContent="space-between" alignItems="center" mb={2}>
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
    <>
      {header}
      {materials && materials.length > 0 ? (
        <MaterialTable items={materials} />
      ) : (
        <Text>Дані відсутні.</Text>
      )}
    </>
  );
};

export default Materials;
