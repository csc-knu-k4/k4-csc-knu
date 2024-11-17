import { Button } from '@/components/ui/button';
import { createListCollection, Flex, Input, Text } from '@chakra-ui/react';
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
import { Chapter, getChapters } from '@/shared/api/chaptersApi';
import { addTopic, getTopics } from '@/shared/api/topicsApi';

const AddTopic = () => {
  const [title, setTitle] = useState('');
  const [chapterId, setChapterId] = useState<number | null>(null);
  const queryClient = useQueryClient();

  const { data: chaptersData, isLoading: chaptersLoading } = useQuery<Chapter[]>(
    ['chapters'],
    getChapters,
  );
  const { data: topicsData, isLoading: topicsLoading } = useQuery(['topics'], getTopics);

  const chapters = createListCollection({
    items: chaptersData
      ? chaptersData.map((chapter: { id: number; title: string }) => ({
          label: chapter.title,
          value: chapter.id.toString(),
        }))
      : [],
  });

  const mutation = useMutation({
    mutationFn: addTopic,
    onSuccess: () => {
      queryClient.invalidateQueries(['topics']);
      setTitle('');
      setChapterId(null);
    },
  });

  const handleAdd = () => {
    if (!title.trim() || chapterId === null) {
      alert('Будь ласка, заповніть всі поля!');
      return;
    }

    const maxOrderPosition = topicsData
      ? Math.max(...topicsData.map((topic: { orderPosition: number }) => topic.orderPosition), 0)
      : 0;

    mutation.mutate({
      title: title.trim(),
      chapterId,
      orderPosition: maxOrderPosition + 1,
      materialsIds: [],
    });
  };

  if (chaptersLoading || topicsLoading) {
    return <Text>Завантаження предметів...</Text>;
  }

  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати тему
      </Text>
      <Field label="Назва" required mb={3} color="orange">
        <Input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Вкажіть назву"
        />
      </Field>
      <Field label="Розділ" required mb={3} color="orange">
        <SelectRoot
          collection={chapters}
          onValueChange={(selected) => setChapterId(Number(selected?.value))}
        >
          <SelectTrigger>
            <SelectValueText placeholder="Вкажіть розділ" />
          </SelectTrigger>
          <SelectContent>
            {chapters.items.map((subject) => (
              <SelectItem item={subject} key={subject.value}>
                {subject.label}
              </SelectItem>
            ))}
          </SelectContent>
        </SelectRoot>
      </Field>
      <Button bgColor="orange" onClick={handleAdd}>
        Додати
      </Button>
    </Flex>
  );
};

export default AddTopic;
