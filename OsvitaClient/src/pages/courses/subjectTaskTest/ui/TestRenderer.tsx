import { useEffect, useState } from 'react';
import { IoArrowBack } from 'react-icons/io5';
import { Box, Button, Flex, IconButton, Spinner, Text } from '@chakra-ui/react';
import { getAssignmentsSets } from '@/shared/api/assingnmentsSets';
import SingleChoiceQuestion from './SingleChoiceQuestion';
import OpenAnswerQuestion from './OpenAnswerQuestion';
import MatchAssignment from './MatchAssignment';
import { addStatisticAssignments } from '@/shared/api/userStatisticApi';
import { toaster } from '@/components/ui/toaster';
import { useLocation, useNavigate } from 'react-router-dom';

interface TestRendererProps {
  testId: number;
  onFinish?: (assignmentSetProgressDetailId: number) => void;
  showCorrectAnswers?: boolean;
}

const TestRenderer: React.FC<TestRendererProps> = ({ testId, onFinish, showCorrectAnswers }) => {
  const [assignments, setAssignments] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [isFinished, setIsFinished] = useState(false);
  const [userAnswers, setUserAnswers] = useState<any[]>([]);
  const [score, setScore] = useState(0);
  const location = useLocation();
  const navigate = useNavigate();
  const isDiagnosticTestPage = location.pathname.startsWith('/course/diagnostic-test/');
  const isStudentTestPage = location.pathname.startsWith('/course/student/test/');
  const isStudentDailyTestPage = location.pathname.startsWith('/course/daily-assignment/');

  const containerHeight =
    isDiagnosticTestPage || isStudentTestPage || isStudentDailyTestPage
      ? 'calc(100vh - 220px)'
      : 'calc(100vh - 350px)';

  const userId = Number(localStorage.getItem('userId'));

  useEffect(() => {
    getAssignmentsSets(testId)
      .then((res) => setAssignments(res.assignments))
      .finally(() => setLoading(false));
  }, [testId]);

  const handleAnswer = (assignmentId: number, value: string) => {
    setUserAnswers((prev) => {
      const updated = [...prev];
      const existing = updated.find((a) => a.assignmentId === assignmentId);
      if (existing) {
        existing.answerValue = value;
      } else {
        updated.push({
          assignmentId,
          answerValue: value,
        });
      }
      return updated;
    });
  };

  const handleFinishTest = async () => {
    const result: any[] = [];
    let localScore = 0;

    assignments.forEach((assignment: any) => {
      if (assignment.assignmentModelType === 0) {
        const userAnswer = userAnswers.find((a) => a.assignmentId === assignment.id);
        const correct = assignment.answers.find((a: any) => a.isCorrect);
        if (userAnswer?.answerValue === correct?.value) localScore += 1;

        result.push({
          assignmentSetProgressDetailId: 0,
          assignmentId: assignment.id,
          answerValue: userAnswer?.answerValue || '',
          isCorrect: false,
          points: 0,
          maxPoints: 0,
        });
      }

      if (assignment.assignmentModelType === 1) {
        const userAnswer = userAnswers.find((a) => a.assignmentId === assignment.id);
        const correct = assignment.answers[0]?.value;
        if (userAnswer?.answerValue?.trim() === correct?.trim()) localScore += 1;

        result.push({
          assignmentSetProgressDetailId: 0,
          assignmentId: assignment.id,
          answerValue: userAnswer?.answerValue || '',
          isCorrect: false,
          points: 0,
          maxPoints: 0,
        });
      }

      if (assignment.assignmentModelType === 2) {
        assignment.childAssignments.forEach((child: any) => {
          const userAnswer = userAnswers.find((a) => a.assignmentId === child.id);
          const correct = child.answers.find((a: any) => a.isCorrect);
          if (userAnswer?.answerValue === correct?.value) localScore += 1;

          result.push({
            assignmentSetProgressDetailId: 0,
            assignmentId: child.id,
            answerValue: userAnswer?.answerValue || '',
            isCorrect: false,
            points: 0,
            maxPoints: 0,
          });
        });
      }
    });

    const payload = {
      id: 0,
      statisticId: userId,
      assignmentSetId: testId,
      score: 0,
      maxScore: 0,
      isCompleted: true,
      completedDate: new Date().toISOString(),
      assignmentProgressDetails: result,
    };

    try {
      const progressDetailId = Number(await addStatisticAssignments(userId, payload));
      setScore(localScore);
      setIsFinished(true);
      if (onFinish) onFinish(progressDetailId);
    } catch (err) {
      toaster.create({
        title: `Помилка збереження результату: ${err}`,
        type: 'error',
      });
    }
  };

  const renderQuestion = (question: any, index: number) => {
    const commonProps = {
      data: question,
      index,
      isFinished,
      userAnswers,
      onAnswer: handleAnswer,
      showCorrectAnswers,
    };

    switch (question.assignmentModelType) {
      case 0:
        return <SingleChoiceQuestion key={question.id} {...commonProps} />;
      case 1:
        return <OpenAnswerQuestion key={question.id} {...commonProps} />;
      case 2:
        return <MatchAssignment key={question.id} {...commonProps} />;
      default:
        return null;
    }
  };

  if (loading) return <Spinner size="xl" />;

  return (
    <Flex w="full" flexDir="column" gap={5} height={containerHeight} overflowY="auto">
      <Box position="sticky" top={0} zIndex={10} bg="white" pb={3}>
        {!location.pathname.startsWith('/course/subject-test/') && (
          <IconButton
            aria-label="Назад"
            onClick={() => navigate(-1)}
            size="sm"
            variant="outline"
            colorScheme="orange"
          >
            <IoArrowBack />
          </IconButton>
        )}
      </Box>
      {assignments.map(renderQuestion)}

      {!isFinished ? (
        <Button onClick={handleFinishTest} colorPalette="orange" maxW="200px">
          Завершити тест
        </Button>
      ) : (
        <Text fontSize="xl" fontWeight="bold" color="green.500">
          Загальна кількість балів: {score}
        </Text>
      )}
    </Flex>
  );
};

export default TestRenderer;
