import { Field } from '@/components/ui/field';
import {
  SelectContent,
  SelectItem,
  SelectRoot,
  SelectTrigger,
  SelectValueText,
} from '@/components/ui/select';
import {
  AccordionItem,
  AccordionItemContent,
  AccordionItemTrigger,
  AccordionRoot,
} from '@/components/ui/accordion';
import { getSubjects, Subject } from '@/shared/api/subjectsApi';
import { Button, createListCollection, Flex, Stack, Text } from '@chakra-ui/react';
import { Checkbox } from '@/components/ui/checkbox';
import { useEffect, useState } from 'react';
import { toaster } from '@/components/ui/toaster';
const ClassTaskCreate = () => {
  const [subjectId, setSubjectId] = useState<number | null>(null);

  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getSubjects()
      .then((data) => {
        setSubjects(data);
        localStorage.setItem('subjectsData', JSON.stringify(data));
      })
      .catch((error) => {
        toaster.create({
          title: `Помилка завантаження предметів: ${error}`,
          type: 'error',
        });
      })
      .finally(() => setLoading(false));
  }, []);

  const selectedSubject = subjects.find((s) => s.id === subjectId);

  const subjectsList = createListCollection({
    items: subjects
      ? subjects.map((subject: { id: number; title: string }) => ({
          label: subject.title,
          value: subject.id.toString(),
        }))
      : [],
  });

  return (
    <Flex flexDir="column" gap={6}>
      <Flex flexDir="row" gap={5} justifyContent="center" alignItems="center">
        <Field required color="orange" maxW="25rem">
          <SelectRoot
            collection={subjectsList}
            onValueChange={(selected) => setSubjectId(Number(selected?.value))}
          >
            <SelectTrigger>
              <SelectValueText placeholder="Вкажіть предмет" />
            </SelectTrigger>
            <SelectContent>
              {subjectsList.items.map((subject) => (
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

      <Stack width="full">
        {/* Головний акордеон */}
        {selectedSubject && (
          <>
            <AccordionRoot multiple collapsible>
              {selectedSubject.chapters.map((chapter) => (
                <AccordionItem
                  boxShadow="0rem 0.13rem 0.31rem 0rem rgba(0, 0, 0, 0.15);"
                  px={4}
                  py={2}
                  key={chapter.id}
                  value={`chapter-${chapter.id}`}
                  mb={3}
                  borderRadius="1rem"
                >
                  <AccordionItemTrigger>{chapter.title}</AccordionItemTrigger>
                  <AccordionItemContent>
                    <Stack mt={2}>
                      <AccordionRoot multiple collapsible>
                        {chapter.topics.map((topic) => (
                          <AccordionItem key={topic.id} value={`topic-${topic.id}`} mb={2} ml={4}>
                            <Flex align="center" justify="space-between" w="full" mb={2}>
                              <Text>{topic.title}</Text>
                              <Checkbox colorPalette="orange">Додати</Checkbox>
                            </Flex>
                          </AccordionItem>
                        ))}
                      </AccordionRoot>
                    </Stack>
                  </AccordionItemContent>
                </AccordionItem>
              ))}
            </AccordionRoot>
            <Button colorPalette="orange" borderRadius="1rem" maxW="20rem">
              Призначити завдання
            </Button>
          </>
        )}
      </Stack>
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
