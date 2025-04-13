import { useEffect, useState } from 'react';
import { Button, Flex, VStack, Text } from '@chakra-ui/react';
import { BsBookFill, BsFileEarmarkCheckFill } from 'react-icons/bs';
import { LuPlus } from 'react-icons/lu';
import { useNavigate, useParams } from 'react-router-dom';
import { getClassesEducationPlan, getClassesEducationPlanVm } from '@/shared/api/classesApi';
import { getAssignmentSetById } from '@/shared/api/testsApi';
import { getTopicById } from '@/shared/api/topicsApi';

const ClassTask = () => {
  const navigate = useNavigate();
  const { classId } = useParams<{ classId: string }>();
  const [loading, setLoading] = useState(true);
  const [topics, setTopics] = useState<any[]>([]);
  const [tests, setTests] = useState<any[]>([]);
  const [topicsVms, setTopicsVms] = useState<any[]>([]);
  const [testsVms, setTestsVms] = useState<any[]>([]);

  useEffect(() => {
    if (!classId) return;

    const loadData = async () => {
      try {
        const plan = await getClassesEducationPlan(Number(classId));
        const planvm = await getClassesEducationPlanVm(Number(classId));

        const validTestIds = plan.assignmentSetPlanDetails
          .map((a: any) => a.assignmentSetId)
          .filter((id: number) => id !== 0);

        const testRequests = validTestIds.map((id: number) => getAssignmentSetById(id));
        const testData = await Promise.all(testRequests);

        const topicRequests = plan.topicPlanDetails.map((t: any) => getTopicById(t.topicId));
        const topicsData = await Promise.all(topicRequests);

        setTopics(topicsData);
        setTests(testData);
        setTopicsVms(planvm.topics);
        setTestsVms(planvm.assignmentSets);
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
            {/* 🔹 Вивід тем */}
            {topicsVms.map((topic) => (
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
            {testsVms.map((test) => (
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
                    Тест {test.id}:{' '}
                    {test.title || 'Без опису'}
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
