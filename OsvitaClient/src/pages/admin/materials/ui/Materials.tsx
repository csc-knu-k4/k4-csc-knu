import { Text, Flex } from '@chakra-ui/react';
import { AddMaterialButton } from '@/features/materials';
import { MaterialTable } from '@/entities/materials';
import { getMaterials } from '@/shared/api/materialsApi';
import { useQuery } from 'react-query';

const Materials = () => {
  const { data: materials, isLoading } = useQuery(['materials'], getMaterials);

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Матеріали
        </Text>
        <AddMaterialButton />
      </Flex>
      <MaterialTable items={materials || []} />
    </>
  );
};

export default Materials;
