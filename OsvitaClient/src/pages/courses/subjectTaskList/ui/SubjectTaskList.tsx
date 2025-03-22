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

const SubjectTaskList = () => {
  const { subjectId } = useParams();
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    getSubjects()
      .then((data) => {
        setSubjects(data);
        localStorage.setItem('subjectsData', JSON.stringify(data));
      })
      .catch((error) => console.error('Помилка завантаження предметів:', error))
      .finally(() => setLoading(false));
  }, []);

  const selectedSubject = subjects.find((sub) => sub.id.toString() === subjectId);

  const handleStartTestFromTopic = async (topicId: number) => {
    try {
      // Створюємо тест
      const testId = await addAssignmentsSets({
        id: 0,
        objectModelType: 1,
        objectId: topicId,
        assignments: [],
      });

      // Отримуємо завдання тесту
      const testData = await getAssignmentsSets(testId);

      if (!testData.assignments || testData.assignments.length === 0) {
        alert('До цієї теми ще немає тесту 😔');
        return;
      }

      // Переходимо на тест
      navigate(`/course/subject-test/${testId}`);
    } catch (err) {
      console.error('Помилка при створенні тесту:', err);
      alert('Не вдалося створити тест. Спробуйте пізніше.');
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
                          <AccordionItemTrigger>{topic.title}</AccordionItemTrigger>
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
