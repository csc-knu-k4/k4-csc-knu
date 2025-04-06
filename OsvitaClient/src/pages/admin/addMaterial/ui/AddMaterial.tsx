import { Button } from '@/components/ui/button';
import {
  Box,
  createListCollection,
  Flex,
  HStack,
  Input,
  Text,
  Textarea,
  VStack,
  Image,
} from '@chakra-ui/react';
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
import { addMaterial, ContentBlock, getMaterials } from '@/shared/api/materialsApi';
import { toaster } from '@/components/ui/toaster';
import { uploadFile } from '@/shared/api/mediaApi';

const AddMaterial = () => {
  const [title, setTitle] = useState('');
  const [topicId, setTopicId] = useState<number | null>(null);
  const queryClient = useQueryClient();
  const [blocks, setBlocks] = useState<ContentBlock[]>([]);

  const { data: topicsData, isLoading: topicsLoading } = useQuery<Topic[]>(['topics'], getTopics);
  const { data: materialsData, isLoading: materialsLoading } = useQuery(
    ['materials'],
    getMaterials,
  );

  const topics = createListCollection({
    items: topicsData
      ? topicsData.map((topics: { id: number; title: string }) => ({
          label: topics.title,
          value: topics.id.toString(),
        }))
      : [],
  });

  const addTextBlock = () => {
    setBlocks((prev) => [
      ...prev,
      {
        id: 0,
        title,
        contentBlockModelType: 0,
        orderPosition: prev.length + 1,
        materialId: 0,
        value: '',
      },
    ]);
  };

  const addImageBlock = async (file: File) => {
    const uploaded = await uploadFile(file);
    setBlocks((prev) => [
      ...prev,
      {
        id: 0,
        title: file.name,
        contentBlockModelType: 1,
        orderPosition: prev.length + 1,
        materialId: 0,
        value: uploaded,
      },
    ]);
  };

  const removeBlock = (index: number) => {
    setBlocks((prev) => {
      const updated = prev.filter((_, i) => i !== index);
      return updated.map((block, i) => ({ ...block, orderPosition: i + 1 }));
    });
  };

  const updateBlockValue = (index: number, value: string) => {
    setBlocks((prev) => {
      const copy = [...prev];
      copy[index].value = value;
      return copy;
    });
  };

  const addMaterialMutation = useMutation({
    mutationFn: addMaterial,
    onSuccess: () => {
      queryClient.invalidateQueries(['materials']);
      toaster.create({ title: 'Матеріал створено!', type: 'success' });
      setTitle('');
      setTopicId(null);
      setBlocks([]);
    },
  });

  const isFormValid = () => {
    if (!title.trim() || topicId === null || blocks.length === 0) return false;
    for (const block of blocks) {
      if (block.contentBlockModelType === 0 && !block.value.trim()) return false;
      if (block.contentBlockModelType === 1 && !block.value) return false;
    }
    return true;
  };

  const handleAddMaterial = () => {
    if (!isFormValid()) {
      toaster.create({
        title: `Будь ласка, заповніть всі поля!`,
        type: 'error',
      });
      return;
    }

    const maxOrderPosition = materialsData
      ? Math.max(...materialsData.map((m) => m.orderPosition), 0)
      : 0;

    const preparedBlocks = blocks.map((block, index) => ({
      ...block,
      id: 0,
      orderPosition: index + 1,
      materialId: 0,
    }));

    const newMaterial = {
      id: 0,
      title: title.trim(),
      topicId: topicId!,
      orderPosition: maxOrderPosition + 1,
      contentBlocksIds: [],
      contentBlocks: preparedBlocks,
    };

    addMaterialMutation.mutate(newMaterial);
  };

  if (materialsLoading || topicsLoading) {
    return <Text>Завантаження даних...</Text>;
  }

  return (
    <Box overflowY="auto" h="calc(100dvh - 180px)">
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

        {/* Контент-блоки */}
        <VStack gap={6} align="stretch" mt={6}>
          {blocks.map((block, index) => (
            <Field
              key={index}
              label={`Блок #${index + 1} (${block.contentBlockModelType === 0 ? 'текст' : 'зображення'})`}
              color="orange"
            >
              <Flex flexDir="column" gap={2} w="full">
                {block.contentBlockModelType === 0 ? (
                  <Textarea
                    value={block.value}
                    onChange={(e) => updateBlockValue(index, e.target.value)}
                    placeholder="Введіть текст"
                    minH="150px"
                  />
                ) : (
                  <Box border="1px solid orange" p={2}>
                    <Text fontWeight="medium" mb={2}>
                      {block.title}
                    </Text>
                    <Image
                      src={`${'http://localhost:5134/'}${block.value}`}
                      alt={block.title}
                      borderRadius="lg"
                      maxW="100%"
                      maxH="300px"
                      objectFit="contain"
                      boxShadow="md"
                    />
                  </Box>
                )}

                <Button
                  size="sm"
                  colorScheme="red"
                  variant="outline"
                  alignSelf="flex-end"
                  onClick={() => removeBlock(index)}
                >
                  Видалити блок
                </Button>
              </Flex>
            </Field>
          ))}
        </VStack>

        <HStack mt={6} gap={4}>
          <Button colorPalette="orange" onClick={addTextBlock}>
            + Текстовий блок
          </Button>
          <Button as="label" cursor="pointer" colorScheme="orange" variant="outline">
            + Додати зображення
            <Input
              type="file"
              accept="image/*"
              display="none"
              onChange={(e) => {
                if (e.target.files?.[0]) {
                  addImageBlock(e.target.files[0]);
                }
              }}
            />
          </Button>
        </HStack>

        <Button mt={6} bgColor="orange" onClick={handleAddMaterial}>
          Додати матеріал
        </Button>
      </Flex>
    </Box>
  );
};

export default AddMaterial;
