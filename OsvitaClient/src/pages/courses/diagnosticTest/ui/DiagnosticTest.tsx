import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Text, Box } from '@chakra-ui/react';
import { getDiagnosticalRecomendation } from '@/shared/api/userStatisticApi';
import TestRenderer from '../../subjectTaskTest/ui/TestRenderer';

const DiagnosticTest = () => {
  const { testId } = useParams();
  const [recommendation, setRecommendation] = useState<string | null>(null);
  const [isTestFinished, setIsTestFinished] = useState(false);

  const userId = Number(localStorage.getItem('userId'));

  useEffect(() => {
    if (isTestFinished) {
      getDiagnosticalRecomendation(userId, Number(testId))
        .then((data) => {
          if (data?.length) setRecommendation(data.recomendationText);
        })
        .catch(() => {
          setRecommendation('Не вдалося завантажити рекомендацію');
        });
    }
  }, [isTestFinished]);

  return (
    <Box>
      <TestRenderer testId={Number(testId)} onFinish={() => setIsTestFinished(true)} />
      {isTestFinished && recommendation && (
        <Box mt={5} p={4} borderWidth={1} borderRadius="xl" bg="gray.50">
          <Text fontSize="lg" fontWeight="bold" mb={2}>
            Рекомендація:
          </Text>
          <Text whiteSpace="pre-wrap">{recommendation}</Text>
        </Box>
      )}
    </Box>
  );
};

export default DiagnosticTest;
