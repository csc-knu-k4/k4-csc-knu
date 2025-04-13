import { useEffect, useState } from 'react';
import { Button, Flex, VStack, Text } from '@chakra-ui/react';
import { BsBookFill, BsFileEarmarkCheckFill } from 'react-icons/bs';
import { LuPlus } from 'react-icons/lu';
import { useNavigate, useParams } from 'react-router-dom';
import { getClassesEducationPlanVm } from '@/shared/api/classesApi';

const ClassTask = () => {
  const navigate = useNavigate();
  const { classId } = useParams<{ classId: string }>();
  const [loading, setLoading] = useState(true);
  const [topics, setTopics] = useState<any[]>([]);
  const [tests, setTests] = useState<any[]>([]);

  useEffect(() => {
    if (!classId) return;

    const loadData = async () => {
      try {
        const planVm = await getClassesEducationPlanVm(Number(classId));
        setTopics(planVm.topics || []);
        setTests(planVm.assignmentSets || []);
      } catch (err) {
        console.error('❌ Error loading plan:', err);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [classId]);

  const handleCreateTask = () => {
    navigate(`/teacher/${classId}/class-task/create`);
  };

  return (
    <Flex overflowY="auto" flexDir="column" p={2} height="calc(100vh - 220px)">
      <Button maxW="20rem" borderRadius="1rem" colorPalette="orange" onClick={handleCreateTask}>
        <LuPlus /> Призначити завдання
      </Button>

      <VStack gap={4} mt={6}>
        {loading ? (
          <Text>Завантаження...</Text>
        ) : tests.length === 0 && topics.length === 0 ? (
          <Text color="gray.500" fontSize="lg" textAlign="center">
            Наразі немає призначених завдань або тем для цього класу.
          </Text>
        ) : (
          <>
            {/* Вивід тем */}
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

            {/* Вивід тестів */}
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
                    Тест {test.id}: {test.title || 'Без опису'}
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

export default ClassTask;
