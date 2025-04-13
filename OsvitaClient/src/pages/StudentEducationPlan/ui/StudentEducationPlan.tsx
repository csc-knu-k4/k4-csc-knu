import { useEffect, useState } from 'react';
import { Flex, VStack, Text } from '@chakra-ui/react';
import { BsBookFill, BsFileEarmarkCheckFill } from 'react-icons/bs';
import { getUserEducationPlanById } from '@/shared/api/userApi';

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
      <VStack gap={4} mt={6}>
        {loading ? (
          <Text>Завантаження...</Text>
        ) : tests.length === 0 && topics.length === 0 ? (
          <Text color="gray.500" fontSize="lg" textAlign="center">
            Наразі немає призначених завдань або тем.
          </Text>
        ) : (
          <>
            {/* 🔹 Вивід тем */}
            {topics.map((topic) => (
              <Flex
                key={`topic-${topic.id}`}
                borderRadius="1rem"
                w="full"
                boxShadow="sm"
                p="1rem"
                flexDir="row"
                justifyContent="space-between"
                alignItems="center"
              >
                <Flex gap={4} alignItems="center">
                  <BsBookFill size="28px" color="rgb(234, 88, 12)" />
                  <Text fontSize="md">{topic.title}</Text>
                </Flex>
              </Flex>
            ))}

            {/* 🔹 Вивід тестів */}
            {tests.map((test) => (
              <Flex
                key={`test-${test.id}`}
                borderRadius="1rem"
                w="full"
                boxShadow="sm"
                p="1rem"
                flexDir="row"
                justifyContent="space-between"
                alignItems="center"
              >
                <Flex gap={4} alignItems="center">
                  <BsFileEarmarkCheckFill size="28px" color="rgb(234, 88, 12)" />
                  <Text fontSize="md">
                    {test.title || `Тест ${test.assignmentSetId}`} {test.isCompleted && '✓'}
                  </Text>
                </Flex>
              </Flex>
            ))}
          </>
        )}
      </VStack>
    </Flex>
  );
};

export default StudentEducationPlan;
