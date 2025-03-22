import { Flex, Stack, Text, Spinner, Button } from '@chakra-ui/react';
import {
  AccordionItem,
  AccordionItemContent,
  AccordionItemTrigger,
  AccordionRoot,
} from '@/components/ui/accordion';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getSubjects, Subject } from '@/shared/api/subjectsApi';
import { addAssignmentsSets, getAssignmentsSets } from '@/shared/api/assingnmentsSets';
import { toaster } from '@/components/ui/toaster';
import { Checkbox } from '@/components/ui/checkbox';
import {
  addStatisticTopic,
  getUserStatisticbyId,
  updateStatisticTopic,
} from '@/shared/api/userStatisticApi';

const SubjectTaskList = () => {
  const { subjectId } = useParams();
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const [topicProgress, setTopicProgress] = useState<
    Record<number, { isCompleted: boolean; id: number }>
  >({});
  const userId = Number(localStorage.getItem('userId'));

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

    getUserStatisticbyId(userId).then((stat) => {
      const progressMap: Record<number, { isCompleted: boolean; id: number }> = {};

      stat.topicProgressDetails.forEach((item: any) => {
        progressMap[item.topicId] = {
          isCompleted: item.isCompleted,
          id: item.id,
        };
      });

      setTopicProgress(progressMap);
    });
  }, []);

  const selectedSubject = subjects.find((sub) => sub.id.toString() === subjectId);

  const handleStartTestFromTopic = async (topicId: number) => {
    try {
      const testId = await addAssignmentsSets({
        id: 0,
        objectModelType: 1,
        objectId: topicId,
        assignments: [],
      });

      const testData = await getAssignmentsSets(testId);

      if (!testData.assignments || testData.assignments.length === 0) {
        toaster.create({
          title: `До цієї теми ще немає тесту 😔`,
          type: 'warning',
        });
        return;
      }

      navigate(`/course/subject-test/${testId}`);
    } catch (err) {
      toaster.create({
        title: `Не вдалося створити тест. Спробуйте пізніше: ${err}`,
        type: 'error',
      });
    }
  };

  const handleTopicCheckboxChange = async (topicId: number) => {
    const current = topicProgress[topicId];
    const newStatus = !current?.isCompleted;
    const now = new Date().toISOString();

    try {
      if (current) {
        await updateStatisticTopic(userId, {
          id: current.id,
          statisticId: userId,
          topicId,
          isCompleted: newStatus,
          completedDate: now,
        });
      } else {
        const res = await addStatisticTopic(userId, {
          id: 0,
          statisticId: userId,
          topicId,
          isCompleted: true,
          completedDate: now,
        });

        setTopicProgress((prev) => ({
          ...prev,
          [topicId]: { isCompleted: true, id: res.id },
        }));
        return;
      }

      setTopicProgress((prev) => ({
        ...prev,
        [topicId]: {
          ...(prev[topicId] || { id: 0 }),
          isCompleted: newStatus,
        },
      }));
    } catch (err) {
      toaster.create({
        title: `Не вдалося оновити прогрес теми ${err}`,
        type: 'error',
      });
    }
  };

  return (
    <Flex
      overflowY="auto"
      justifyContent="flex-start"
      alignItems="center"
      flexDir="column"
      w="full"
      p={5}
      height="calc(100vh - 180px)"
    >
      <Text fontSize="2xl" fontWeight="bold" mb={4} color="orange">
        {selectedSubject ? selectedSubject.title : 'Завантаження...'}
      </Text>

      {loading ? (
        <Spinner size="xl" />
      ) : (
        <Stack width="full">
          {/* Головний акордеон */}
          <AccordionRoot multiple collapsible>
            {selectedSubject?.chapters.map((chapter) => (
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
                    {/* Вкладений акордеон для тем */}
                    <AccordionRoot multiple collapsible>
                      {chapter.topics.map((topic) => (
                        <AccordionItem key={topic.id} value={`topic-${topic.id}`} mb={2}>
                          <AccordionItemTrigger>
                            <Flex align="center" justify="space-between" w="full">
                              <Text>{topic.title}</Text>
                              <Checkbox
                                checked={topicProgress[topic.id]?.isCompleted || false}
                                onChange={() => handleTopicCheckboxChange(topic.id)}
                                colorPalette="orange"
                              >
                                Пройдено
                              </Checkbox>
                            </Flex>
                          </AccordionItemTrigger>
                          <AccordionItemContent>
                            <Stack mt={2}>
                              {[...topic.materials]
                                .sort((a, b) => a.orderPosition - b.orderPosition)
                                .map((material) => (
                                  <Text
                                    key={material.id}
                                    fontSize="md"
                                    color="gray.700"
                                    cursor="pointer"
                                    _hover={{ textDecoration: 'underline' }}
                                    onClick={() =>
                                      navigate(`/course/subject-material/${material.id}`, {
                                        state: { topicId: topic.id },
                                      })
                                    }
                                  >
                                    📖 {material.title}
                                  </Text>
                                ))}

                              <Button
                                colorPalette="orange"
                                maxW="10rem"
                                mt={3}
                                size="sm"
                                borderRadius="0.5rem"
                                onClick={() => handleStartTestFromTopic(topic.id)}
                              >
                                Пройти тест
                              </Button>
                            </Stack>
                          </AccordionItemContent>
                        </AccordionItem>
                      ))}
                    </AccordionRoot>
                  </Stack>
                </AccordionItemContent>
              </AccordionItem>
            ))}
          </AccordionRoot>
        </Stack>
      )}
    </Flex>
  );
};

export default SubjectTaskList;
