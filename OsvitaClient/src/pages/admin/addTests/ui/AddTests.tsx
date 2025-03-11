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
import { Topic } from '@/entities/topics';
import { getTopics } from '@/shared/api/topicsApi';
import SingleAnswerTypeTest from './SingleAnswerTypeTest';
import OpenAnswerTypeTest from './OpenAnswerTypeTest';

const MAX_OPTIONS = 5;
const MIN_OPTIONS = 2;

const AddTests = () => {
  const [topicId, setTopicId] = useState<number | null>(null);
  const [testType, setTestType] = useState<string | null>(null);
  const [options, setOptions] = useState<string[]>(['', '']);

  const { data: topicsData, isLoading: topicsLoading } = useQuery<Topic[]>(['topics'], getTopics);
  const topics = createListCollection({
    items: topicsData
      ? topicsData.map((topic) => ({ label: topic.title, value: topic.id.toString() }))
      : [],
  });

  const handleAddOption = () => {
    if (options.length < MAX_OPTIONS) {
      setOptions([...options, '']);
    }
  };

  const handleRemoveOption = (index: number) => {
    if (options.length > MIN_OPTIONS) {
      setOptions(options.filter((_, i) => i !== index));
    }
  };

  const handleOptionChange = (index: number, value: string) => {
    const newOptions = [...options];
    newOptions[index] = value;
    setOptions(newOptions);
  };

  if (topicsLoading) {
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
            collection={topics}
            onValueChange={(selected) => setTopicId(Number(selected?.value))}
          >
            <SelectTrigger>
              <SelectValueText placeholder="Вкажіть тему" />
            </SelectTrigger>
            <SelectContent>
              {topics.items.map((topic) => (
                <SelectItem item={topic} key={topic.value}>
                  {topic.label}
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

      {testType === '0' && (
        <SingleAnswerTypeTest
          options={options}
          handleOptionChange={handleOptionChange}
          handleRemoveOption={handleRemoveOption}
          handleAddOption={handleAddOption}
        />
      )}

      {testType === '1' && <OpenAnswerTypeTest />}

      {testType === '2' && (
        <>
          <Text>Встановлення відповідності</Text>
        </>
      )}
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
