import { Button } from '@/components/ui/button';
import { createListCollection, Flex, HStack, Input, Text, Textarea } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import {
  SelectContent,
  SelectItem,
  SelectRoot,
  SelectTrigger,
  SelectValueText,
} from '@/components/ui/select';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { useState } from 'react';
import { getTopics } from '@/shared/api/topicsApi';
import { Topic } from '@/entities/topics';
import { addMaterial, getMaterials } from '@/shared/api/materialsApi';
import { addContentBlock, getContentBlocks } from '@/shared/api/contentBlocksApi';
import { toaster } from '@/components/ui/toaster';

const AddMaterial = () => {
  const [title, setTitle] = useState('');
  const [topicId, setTopicId] = useState<number | null>(null);
  const [contentValue, setContentValue] = useState('');
  const queryClient = useQueryClient();

  const { data: topicsData, isLoading: topicsLoading } = useQuery<Topic[]>(['topics'], getTopics);
  const { data: materialsData, isLoading: materialsLoading } = useQuery(
    ['materials'],
    getMaterials,
  );
  const { data: contentBlocksData, isLoading: contentBlocksLoading } = useQuery(
    ['contentBlocks'],
    getContentBlocks,
  );

  const topics = createListCollection({
    items: topicsData
      ? topicsData.map((topics: { id: number; title: string }) => ({
          label: topics.title,
          value: topics.id.toString(),
        }))
      : [],
  });

  const addMaterialMutation = useMutation({
    mutationFn: addMaterial,
    onSuccess: (data) => {
      queryClient.invalidateQueries(['materials']);
      handleAddContentBlock(data.id);
    },
  });

  const addContentBlockMutation = useMutation({
    mutationFn: addContentBlock,
    onSuccess: () => {
      queryClient.invalidateQueries(['contentBlocks']);
      setTitle('');
      setTopicId(null);
      setContentValue('');
    },
  });

  const handleAddContentBlock = (materialId: number) => {
    if (!contentBlocksData) return;

    const maxOrderPosition = Math.max(
      ...contentBlocksData.map((block: { orderPosition: number }) => block.orderPosition),
      0,
    );

    addContentBlockMutation.mutate({
      id: 0,
      title,
      contentBlockModelType: 0,
      orderPosition: maxOrderPosition + 1,
      materialId,
      value: contentValue,
    });
  };

  const handleAddMaterial = () => {
    if (!title.trim() || topicId === null || !contentValue.trim()) {
      toaster.create({
        title: `Будь ласка, заповніть всі поля!`,
        type: 'error',
      });
      return;
    }

    const maxOrderPosition = materialsData
      ? Math.max(
          ...materialsData.map((material: { orderPosition: number }) => material.orderPosition),
          0,
        )
      : 0;

    addMaterialMutation.mutate({
      title: title.trim(),
      topicId,
      orderPosition: maxOrderPosition + 1,
      contentBlocksIds: [],
    });
  };

  if (materialsLoading || topicsLoading || contentBlocksLoading) {
    return <Text>Завантаження даних...</Text>;
  }

  return (
    <Flex flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати матеріал
      </Text>
      <Flex flexDir="row" gap={4}>
        <Field label="Назва" required mb={3} color="orange">
          <Input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Вкажіть назву"
            color="orange.placeholder"
            _placeholder={{ color: 'inherit' }}
          />
        </Field>
        <Field label="Тема" required mb={3} color="orange">
          <SelectRoot
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
      </Flex>
      <HStack width="full" mb={3}>
        <Field color="orange" label="Наповнення" required errorText="Поле обов'язкове">
          <Textarea
            _placeholder={{ color: 'inherit' }}
            minH="450px"
            color="orange.placeholder"
            placeholder="Начніть друкувати..."
            variant="outline"
            value={contentValue}
            onChange={(e) => setContentValue(e.target.value)}
          />
        </Field>
      </HStack>
      <Button bgColor="orange" onClick={handleAddMaterial}>
        Додати
      </Button>
    </Flex>
  );
};

export default AddMaterial;
