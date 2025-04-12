import { Field } from '@/components/ui/field';
import {
  SelectContent,
  SelectItem,
  SelectRoot,
  SelectTrigger,
  SelectValueText,
} from '@/components/ui/select';
import { getSubjects, Subject } from '@/shared/api/subjectsApi';
import { createListCollection, Flex } from '@chakra-ui/react';
import { useState } from 'react';
import { useQuery, useQueryClient } from 'react-query';
const ClassTaskCreate = () => {
  const [subjectId, setSubjectId] = useState<number | null>(null);
  const queryClient = useQueryClient();

  const { data: subjectsData, isLoading: subjectsLoading } = useQuery<Subject[]>(
    ['subjects'],
    getSubjects,
  );

  const subjects = createListCollection({
    items: subjectsData
      ? subjectsData.map((subject: { id: number; title: string }) => ({
          label: subject.title,
          value: subject.id.toString(),
        }))
      : [],
  });

  return (
    <Flex flexDir="row" gap={5} justifyContent="center" alignItems="center">
      <Field required color="orange" maxW="25rem">
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
      <Field required color="orange" maxW="25rem">
        <SelectRoot collection={assignmentsList}>
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
  );
};

const assignmentsList = createListCollection({
  items: [
    { label: 'Загальний тест', value: '0' },
    { label: 'Проходження тесту', value: '1' },
    { label: 'Опрацювання матеріалу', value: '2' },
  ],
});

export default ClassTaskCreate;
