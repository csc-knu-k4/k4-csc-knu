import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Text, Box, Spinner, Button, Dialog } from '@chakra-ui/react';
import { getDiagnosticalRecomendation } from '@/shared/api/userStatisticApi';
import TestRenderer from '../../subjectTaskTest/ui/TestRenderer';

const DiagnosticTest = () => {
  const { testId } = useParams();
  const [recommendation, setRecommendation] = useState<string | null>(null);
  const [isTestFinished, setIsTestFinished] = useState(false);
  const [progressDetailId, setProgressDetailId] = useState<number | null>(null);
  const [isLoadingRecommendation, setIsLoadingRecommendation] = useState(false);
  const [isOpen, setIsOpen] = useState(false);

  const userId = Number(localStorage.getItem('userId'));

  useEffect(() => {
    if (isTestFinished && progressDetailId) {
      setIsLoadingRecommendation(true);

      getDiagnosticalRecomendation(userId, progressDetailId)
        .then((data) => {
          setRecommendation(data?.recomendationText || 'Немає рекомендації');
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
