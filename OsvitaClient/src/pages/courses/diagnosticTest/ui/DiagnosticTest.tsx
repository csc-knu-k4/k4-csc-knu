import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Text, Box, Spinner, Button } from '@chakra-ui/react';
import { getDiagnosticalRecomendation } from '@/shared/api/userStatisticApi';
import TestRenderer from '../../subjectTaskTest/ui/TestRenderer';

const DiagnosticTest = () => {
  const { testId } = useParams();
  const [recommendation, setRecommendation] = useState<string | null>(null);
  const [isTestFinished, setIsTestFinished] = useState(false);
  const [progressDetailId, setProgressDetailId] = useState<number | null>(null);
  const [isLoadingRecommendation, setIsLoadingRecommendation] = useState(false);

  const userId = Number(localStorage.getItem('userId'));

  useEffect(() => {
    if (isTestFinished && progressDetailId) {
      setIsLoadingRecommendation(true);

      getDiagnosticalRecomendation(userId, progressDetailId)
        .then((data) => {
          if (data?.recomendationText) {
            setRecommendation(data.recomendationText);
          } else {
            setRecommendation('Немає рекомендації');
          }
        })
        .catch(() => {
          setRecommendation('Не вдалося завантажити рекомендацію');
        })
        .finally(() => {
          setIsLoadingRecommendation(false);
        });
    }
  }, [isTestFinished, progressDetailId]);

  return (
    <Box>
      {!isTestFinished && (
        <TestRenderer
          testId={Number(testId)}
          onFinish={(id) => {
            setIsTestFinished(true);
            setProgressDetailId(id);
          }}
        />
      )}

      {isTestFinished && (
        <Box mt={5} p={4} borderWidth={1} borderRadius="xl" bg="gray.50">
          <Text fontSize="lg" fontWeight="bold" mb={2}>
            Рекомендація:
          </Text>
          {isLoadingRecommendation ? (
            <Box>
              <Spinner size="md" />
            </Box>
          ) : (
            <Text whiteSpace="pre-wrap">{recommendation}</Text>
          )}
          <Button mt={4} colorPalette="orange" onClick={() => window.location.reload()}>
            Пройти ще раз
          </Button>
        </Box>
      )}
    </Box>
  );
};

export default DiagnosticTest;
