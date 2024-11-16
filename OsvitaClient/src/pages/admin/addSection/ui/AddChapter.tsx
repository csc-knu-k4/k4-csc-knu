import { useState } from 'react';
import { useMutation, useQuery, useQueryClient } from 'react-query';
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
import { addChapter } from '@/shared/api/chaptersApi';
import { getSubjects, Subject } from '@/shared/api/subjectsApi';

const AddChapter = () => {
  const [title, setTitle] = useState('');
  const [subjectId, setSubjectId] = useState<number | null>(null);
  const queryClient = useQueryClient();

  const { data: subjectsData, isLoading } = useQuery<Subject[]>(['subjects'], getSubjects);

  const subjects = createListCollection({
    items: subjectsData
      ? subjectsData.map((subject: { id: number; title: string }) => ({
          label: subject.title,
          value: subject.id.toString(),
        }))
      : [],
  });

  const mutation = useMutation({
    mutationFn: addChapter,
    onSuccess: () => {
      queryClient.invalidateQueries(['chapters']);
      setTitle('');
      setSubjectId(null);
    },
  });

  const handleAdd = () => {
    if (!title.trim() || subjectId === null) {
      alert('Будь ласка, заповніть всі поля!');
      return;
    }

    mutation.mutate({
      title: title.trim(),
      subjectId,
      orderPosition: 0,
      topicsIds: [],
    });
  };

  if (isLoading) {
    return <Text>Завантаження предметів...</Text>;
  }

  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати розділ
      </Text>
      <Field label="Назва" required mb={3} color="orange">
        <Input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Вкажіть назву"
          color="orange.placeholder"
        />
      </Field>
      <Field label="Предмет" required mb={3} color="orange">
        <SelectRoot
          collection={subjects}
          onValueChange={(selected) => setSubjectId(Number(selected?.value))}
        >
          <SelectTrigger>
            <SelectValueText placeholder="Вкажіть предмет" />
          </SelectTrigger>
          <SelectContent>
            {subjects.items.map((subject) => (
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

export default AddChapter;
