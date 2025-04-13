import { useEffect, useState } from 'react';
import { Flex, VStack, Text, Box } from '@chakra-ui/react';
import { BsBookFill, BsFileEarmarkCheckFill } from 'react-icons/bs';
import { getUserClassEducationPlansById } from '@/shared/api/userApi';
import { Link } from 'react-router-dom';

const StudentEducationPlan = () => {
  const [loading, setLoading] = useState(true);
  const [plans, setPlans] = useState<any[]>([]);

  useEffect(() => {
    const userId = Number(localStorage.getItem('userId'));
    if (!userId) return;

    const loadData = async () => {
      try {
        const data = await getUserClassEducationPlansById(userId);
        setPlans(data || []);
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
      <VStack gap={6} mt={6} w="full">
        {loading ? (
          <Text>Завантаження...</Text>
        ) : plans.length === 0 ? (
          <Text color="gray.500" fontSize="lg" textAlign="center">
            Наразі немає призначених завдань або тем.
          </Text>
        ) : (
          plans.map((plan) => (
            <Box key={`plan-${plan.educationClassId}`} w="full">
              <Text fontSize="lg" fontWeight="semibold" mb={2} color="gray.700">
                Клас: {plan.educationClassName}
              </Text>

              {/* Теми */}
              {plan.topics.map((topic: any) => (
                <Box mb={4} w="full" key={`topic-${topic.id}`}>
                  <Link to={`/course/subject-topic/${topic.topicId}`}>
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

              {/* Тести */}
              {plan.assignmentSets.map((test: any) => (
                <Box mb={4} w="full" key={`test-${test.assignmentSetId}`}>
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
            </Box>
          ))
        )}
      </VStack>
    </Flex>
  );
};

export default StudentEducationPlan;
