import { useState } from 'react';
import { Field } from '@/components/ui/field';
import { createListCollection, Flex, Text } from '@chakra-ui/react';
import {
  SelectContent,
  SelectItem,
  SelectRoot,
  SelectTrigger,
  SelectValueText,
} from '@/components/ui/select';
import { useQuery } from 'react-query';
import SingleAnswerTypeTest from './SingleAnswerTypeTest';
import OpenAnswerTypeTest from './OpenAnswerTypeTest';
import MultiAnswerTypeTest from './MultiAnswerTypeTest';
import { getMaterials, Material } from '@/shared/api/materialsApi';

const AddTests = () => {
  const [materialId, setMaterialId] = useState<number>(0);
  const [testType, setTestType] = useState<string | null>(null);

  const { data: materialsData, isLoading: materialsLoading } = useQuery<Material[]>(
    ['materials'],
    getMaterials,
  );
  const materials = createListCollection({
    items: materialsData
      ? materialsData.map((material) => ({ label: material.title, value: material.id.toString() }))
      : [],
  });

  if (materialsLoading) {
    return <Text>Завантаження даних...</Text>;
  }

  return (
    <>
      <Flex flexDir="column">
        <Text mb="2rem" fontSize="2xl" fontWeight="medium">
          Додати тест
        </Text>
        <Field label="Тема" required mb={3} color="orange">
          <SelectRoot
            width="30.5rem"
            collection={materials}
            onValueChange={(selected) => setMaterialId(Number(selected?.value))}
          >
            <SelectTrigger>
              <SelectValueText placeholder="Вкажіть тему" />
            </SelectTrigger>
            <SelectContent>
              {materials.items.map((material) => (
                <SelectItem item={material} key={material.value}>
                  {material.label}
                </SelectItem>
              ))}
            </SelectContent>
          </SelectRoot>
        </Field>
        <Field label="Тип тесту" required mb={3} color="orange">
          <SelectRoot
            collection={assignmentsList}
            width="30.5rem"
            onValueChange={(selected) => setTestType(selected?.value?.toString() || null)}
          >
            <SelectTrigger>
              <SelectValueText placeholder="Оберіть тип тесту" />
            </SelectTrigger>
            <SelectContent>
              {assignmentsList.items.map((assignments) => (
                <SelectItem item={assignments} key={assignments.value}>
                  {assignments.label}
                </SelectItem>
              ))}
            </SelectContent>
          </SelectRoot>
        </Field>
      </Flex>

      {testType === '0' && <SingleAnswerTypeTest materialId={materialId} />}

      {testType === '1' && <OpenAnswerTypeTest materialId={materialId} />}

      {testType === '2' && <MultiAnswerTypeTest materialId={materialId} />}
    </>
  );
};

const assignmentsList = createListCollection({
  items: [
    { label: 'Одна правильна відповідь', value: '0' },
    { label: 'Відкрита відповідь', value: '1' },
    { label: 'Встановлення відповідності', value: '2' },
  ],
});

export default AddTests;
