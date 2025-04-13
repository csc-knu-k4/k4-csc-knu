import { useEffect, useState } from 'react';
import { Flex, VStack, Text, Box } from '@chakra-ui/react';
import { BsBookFill, BsFileEarmarkCheckFill } from 'react-icons/bs';
import { getUserEducationPlanById } from '@/shared/api/userApi';
import { Link } from 'react-router-dom';

const StudentEducationPlan = () => {
  const [loading, setLoading] = useState(true);
  const [topics, setTopics] = useState<any[]>([]);
  const [tests, setTests] = useState<any[]>([]);

  useEffect(() => {
    const userId = Number(localStorage.getItem('userId'));
    if (!userId) return;

    const loadData = async () => {
      try {
        const data = await getUserEducationPlanById(userId);
        setTopics(data.topics || []);
        setTests(data.assignmentSets || []);
      } catch (err) {
        console.error('❌ Error loading user plan:', err);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  return (
    <Flex overflowY="auto" flexDir="column" p={2} height="calc(100vh - 220px)">
      <Text color="orange" fontSize="xl" fontWeight="bold" textAlign="center">
        Мій навчальний план
      </Text>
      <VStack gap={4} mt={6} w="full">
        {loading ? (
          <Text>Завантаження...</Text>
        ) : tests.length === 0 && topics.length === 0 ? (
          <Text color="gray.500" fontSize="lg" textAlign="center">
            Наразі немає призначених завдань або тем.
          </Text>
        ) : (
          <>
            {/* Вивід тем */}
            {topics.map((topic) => (
              <Box w="full" key={`topic-${topic.id}`}>
                <Link to={`/course/topic/${topic.topicId}`}>
                  <Flex
                    borderRadius="1rem"
                    w="100%"
                    boxShadow="sm"
                    p="1rem"
                    flexDir="row"
                    justifyContent="space-between"
                    alignItems="center"
                    _hover={{ bg: 'gray.50' }}
                  >
                    <Flex gap={4} alignItems="center">
                      <BsBookFill size="28px" color="rgb(234, 88, 12)" />
                      <Text fontSize="md">{topic.title}</Text>
                    </Flex>
                  </Flex>
                </Link>
              </Box>
            ))}

            {/* Вивід тестів */}
            {tests.map((test) => (
              <Box w="full" key={`test-${test.assignmentSetId}`}>
                <Link to={`/course/student/test/${test.assignmentSetId}`}>
                  <Flex
                    borderRadius="1rem"
                    w="100%"
                    boxShadow="sm"
                    p="1rem"
                    flexDir="row"
                    justifyContent="space-between"
                    alignItems="center"
                    _hover={{ bg: 'gray.50' }}
                  >
                    <Flex gap={4} alignItems="center">
                      <BsFileEarmarkCheckFill size="28px" color="rgb(234, 88, 12)" />
                      <Text fontSize="md">
                        {`Тест: ${test.title || test.assignmentSetId}`} {test.isCompleted && '✓'}
                      </Text>
                    </Flex>
                  </Flex>
                </Link>
              </Box>
            ))}
          </>
        )}
      </VStack>
    </Flex>
  );
};

export default StudentEducationPlan;
