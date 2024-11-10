import { Text, Flex } from '@chakra-ui/react';
import { AddMaterialButton } from '@/features/materials';
import { MaterialTable } from '@/entities/materials';

const items = [
  { id: 1, topic: 'Властивості дій з дійсними числа...', date: '20.10.2024, 16:32' },
  { id: 2, topic: 'Правила порівняння дійсними чис...', date: '20.10.2024, 16:32' },
  { id: 3, topic: 'Правила знаходження найбільшог...', date: '20.10.2024, 16:32' },
  { id: 4, topic: 'Означення кореня п-го степеня і ...', date: '20.10.2024, 16:32' },
  { id: 5, topic: 'Модуль дійсного числа та ймовір...', date: '20.10.2024, 16:32' },
];

const Materials = () => {
  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Матеріали
        </Text>
        <AddMaterialButton />
      </Flex>
      <MaterialTable items={items} />
    </>
  );
};

export default Materials;
