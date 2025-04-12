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
import { useParams } from 'react-router-dom';
import {
  addClassesEducationPlanAssignments,
  addClassesEducationPlanTopics,
  deleteClassesEducationPlanTopicsById,
  getClassesEducationPlan,
} from '@/shared/api/classesApi';
import { addAssignmentsSets } from '@/shared/api/assingnmentsSets';

const ClassTaskCreate = () => {
  const [subjectId, setSubjectId] = useState<number | null>(null);
  const [taskType, setTaskType] = useState<number | null>(null); // "0" | "1" | "2"
  const [selectedTopics, setSelectedTopics] = useState<number[]>([]);
  const { classId } = useParams<{ classId: string }>();
  const [initialTopics, setInitialTopics] = useState<number[]>([]);
  const [subjects, setSubjects] = useState<Subject[]>([]);

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
      });
  }, []);

  useEffect(() => {
    setSelectedTopics([]);
    setInitialTopics([]);

    if (!classId || taskType !== 2) return;

    getClassesEducationPlan(Number(classId))
      .then((data) => {
        const topicIds = data.topicPlanDetails.map((t: any) => t.topicId);
        setInitialTopics(topicIds);
        setSelectedTopics(topicIds);
      })
      .catch((error) => {
        toaster.error({ title: `Помилка при завантаженні плану ${error}` });
      });
  }, [classId, taskType]);

  const selectedSubject = subjects.find((s) => s.id === subjectId);

  const subjectsList = createListCollection({
    items: subjects.map((subject) => ({
      label: subject.title,
      value: subject.id.toString(),
    })),
  });

  const handleToggleTopic = async (topicId: number) => {
    if (taskType !== 1 && taskType !== 2) return;

    const isSelected = selectedTopics.includes(topicId);
    const updated = isSelected
      ? selectedTopics.filter((id) => id !== topicId)
      : [...selectedTopics, topicId];

    setSelectedTopics(updated);

    if (taskType !== 2) return;

    try {
      if (isSelected) {
        await deleteClassesEducationPlanTopicsById(topicId, Number(classId));
      } else {
        await addClassesEducationPlanTopics(
          {
            id: 0,
            educationPlanId: 0,
            topicId,
            educationClassPlanId: Number(classId),
          },
          Number(classId),
        );
      }
    } catch (error) {
      toaster.error({ title: `Помилка при оновленні теми ${error}` });
    }
  };

  const handleAssign = async () => {
    if (!classId) {
      toaster.error({ title: 'Клас не обраний' });
      return;
    }

    try {
      if (taskType === 2) {
        // 🟠 Опрацювання матеріалу
        if (selectedTopics.length === 0) {
          toaster.error({ title: 'Оберіть теми для призначення' });
          return;
        }

        for (const topicId of selectedTopics) {
          await addClassesEducationPlanTopics(
            {
              id: 0,
              educationPlanId: 0,
              topicId,
              educationClassPlanId: Number(classId),
            },
            Number(classId),
          );
        }

        toaster.success({ title: 'Матеріали призначено!' });
        setSelectedTopics([]);
      }

      if (taskType === 1) {
        // 🟠 Проходження тесту
        if (selectedTopics.length === 0) {
          toaster.error({ title: 'Оберіть теми для тестування' });
          return;
        }

        for (const topicId of selectedTopics) {
          const assignmentSet = await addAssignmentsSets({
            id: 0,
            objectModelType: 1, // 🟠 тема
            objectId: topicId,
            assignments: [],
          });

          await addClassesEducationPlanAssignments(
            {
              id: 0,
              assignmentSetId: assignmentSet,
              educationClassPlanId: Number(classId),
            },
            Number(classId),
          );
        }

        toaster.success({ title: 'Тести призначено!' });
        setSelectedTopics([]);
      }

      if (taskType === 0) {
        // 🟠 Загальний тест
        if (!subjectId) {
          toaster.error({ title: 'Оберіть предмет для загального тесту' });
          return;
        }

        const assignmentSet = await addAssignmentsSets({
          id: 0,
          objectModelType: 3, // 🟠 subject
          objectId: subjectId,
          assignments: [],
        });

        await addClassesEducationPlanAssignments(
          {
            id: 0,
            assignmentSetId: assignmentSet,
            educationClassPlanId: Number(classId),
          },
          Number(classId),
        );

        toaster.success({ title: 'Загальний тест призначено!' });
      }
    } catch (err) {
      toaster.error({ title: `Помилка збереження: ${err}` });
    }
  };

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
          <SelectRoot
            collection={assignmentsList}
            onValueChange={(selected) => setTaskType(Number(selected?.value))}
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

      <Stack width="full">
        {selectedSubject && (
          <>
            {(taskType === 1 || taskType === 2) && (
              <AccordionRoot multiple collapsible>
                {selectedSubject.chapters.map((chapter) => (
                  <AccordionItem
                    key={chapter.id}
                    value={`chapter-${chapter.id}`}
                    mb={3}
                    px={4}
                    py={2}
                    borderRadius="1rem"
                    boxShadow="0rem 0.13rem 0.31rem 0rem rgba(0, 0, 0, 0.15);"
                  >
                    <AccordionItemTrigger>{chapter.title}</AccordionItemTrigger>
                    <AccordionItemContent>
                      <Stack mt={2}>
                        <AccordionRoot multiple collapsible>
                          {chapter.topics.map((topic) => (
                            <AccordionItem key={topic.id} value={`topic-${topic.id}`} mb={2} ml={4}>
                              <Flex align="center" justify="space-between" w="full" mb={2}>
                                <Text>{topic.title}</Text>
                                {(taskType === 1 || taskType === 2) && (
                                  <Checkbox
                                    checked={selectedTopics.includes(topic.id)}
                                    onCheckedChange={() => handleToggleTopic(topic.id)}
                                    colorPalette="orange"
                                  >
                                    {taskType === 2 && initialTopics.includes(topic.id)
                                      ? 'Додано'
                                      : 'Додати'}
                                  </Checkbox>
                                )}
                              </Flex>
                            </AccordionItem>
                          ))}
                        </AccordionRoot>
                      </Stack>
                    </AccordionItemContent>
                  </AccordionItem>
                ))}
              </AccordionRoot>
            )}
            <Button
              m="0 auto"
              onClick={handleAssign}
              colorPalette="orange"
              borderRadius="1rem"
              maxW="20rem"
            >
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
