import { Link, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Text, Box, Spinner, Button, Dialog, VStack } from '@chakra-ui/react';
import { getDiagnosticalRecomendation } from '@/shared/api/userStatisticApi';
import TestRenderer from '../../subjectTaskTest/ui/TestRenderer';
import { getTopicById } from '@/shared/api/topicsApi';

const DiagnosticTest = () => {
  const { testId } = useParams();
  const [recommendation, setRecommendation] = useState<string | null>(null);
  const [isTestFinished, setIsTestFinished] = useState(false);
  const [progressDetailId, setProgressDetailId] = useState<number | null>(null);
  const [isLoadingRecommendation, setIsLoadingRecommendation] = useState(false);
  const [isOpen, setIsOpen] = useState(false);
  const [recommendedTopics, setRecommendedTopics] = useState<{ id: number; title: string }[]>([]);

  const userId = Number(localStorage.getItem('userId'));

  useEffect(() => {
    if (isTestFinished && progressDetailId) {
      setIsLoadingRecommendation(true);

      getDiagnosticalRecomendation(userId, progressDetailId)
        .then(async (data) => {
          setRecommendation(data?.recomendationText || 'Немає рекомендації');
          if (data?.topicIds?.length) {
            const topics = await Promise.all(data.topicIds.map((id: number) => getTopicById(id)));
            setRecommendedTopics(topics.map((t) => ({ id: t.id, title: t.title })));
          }
          setIsOpen(true);
        })
        .catch(() => {
          setRecommendation('Не вдалося завантажити рекомендацію');
          setIsOpen(true);
        })
        .finally(() => {
          setIsLoadingRecommendation(false);
        });
    }
  }, [isTestFinished, progressDetailId]);

  return (
    <Box>
      <TestRenderer
        testId={Number(testId)}
        onFinish={(id) => {
          setIsTestFinished(true);
          setProgressDetailId(id);
        }}
        showCorrectAnswers={isTestFinished}
      />

      {isTestFinished && (
        <>
          <Button mt={4} colorPalette="orange" onClick={() => setIsOpen(true)}>
            Показати рекомендацію
          </Button>

          <Dialog.Root open={isOpen} onOpenChange={(details) => setIsOpen(details.open)}>
            <Dialog.Backdrop />
            <Dialog.Positioner>
              <Dialog.Content borderRadius="xl" bg="white" p={4}>
                <Dialog.Header>
                  <Dialog.Title fontSize="xl" fontWeight="bold" color="orange">
                    Рекомендація
                  </Dialog.Title>
                  <Dialog.CloseTrigger />
                </Dialog.Header>
                <Dialog.Body>
                  {isLoadingRecommendation ? (
                    <Spinner size="md" />
                  ) : (
                    <Text whiteSpace="pre-wrap">{recommendation}</Text>
                  )}

                  {recommendedTopics.length > 0 && (
                    <Box mt={4}>
                      <Text fontWeight="bold" mb={2}>
                        Рекомендовані теми:
                      </Text>
                      <VStack align="start" gap={2}>
                        {recommendedTopics.map((topic) => (
                          <Link key={topic.id} to={`/course/subject-topic/${topic.id}`}>
                            <Button colorPalette="orange">{topic.title}</Button>
                          </Link>
                        ))}
                      </VStack>
                    </Box>
                  )}
                </Dialog.Body>
                <Dialog.Footer>
                  <Button onClick={() => window.location.reload()} colorPalette="orange">
                    Пройти ще раз
                  </Button>
                </Dialog.Footer>
              </Dialog.Content>
            </Dialog.Positioner>
          </Dialog.Root>
        </>
      )}
    </Box>
  );
};

export default DiagnosticTest;
