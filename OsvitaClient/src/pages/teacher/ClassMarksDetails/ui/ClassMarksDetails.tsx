import { useEffect, useState } from 'react';
import { Table } from '@chakra-ui/react';
import { UserAvatar } from '@/shared/ui/Avatar';
import { useParams } from 'react-router-dom';
import { getClassesStatisticAssignmentsByAssingmentSetId } from '@/shared/api/classesApi';

interface Assignment {
  assignmentId: number;
  assignmentNumber: number;
  assignmentType: number;
  topicName: string;
  topicId: number;
  isCorrect: boolean;
  points: number;
  maxPoints: number;
}

interface AssignmetSetReportModel {
  userId: number;
  userEmail: string;
  userFirstName: string;
  userSecondName: string;
  objectId: number;
  objectName: string;
  objectType: number;
  completedDate: string;
  score: number;
  maxScore: number;
  assignments: Assignment[];
}

const ClassMarksDetails = () => {
  const { classId, testId } = useParams<{ classId: string; testId: string }>();
  const [data, setData] = useState<AssignmetSetReportModel[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      if (classId && testId) {
        try {
          const response = await getClassesStatisticAssignmentsByAssingmentSetId(
            Number(classId),
            Number(testId),
          );
          setData(response.assignmetSetReportModels || []);
        } catch (error) {
          console.error('Error fetching data:', error);
        }
      }
    };

    fetchData();
  }, [classId, testId]);

  const maxAssignments = Math.max(...data.map((item) => item.assignments.length), 0);

  return (
    <>
      <Table.ScrollArea w="full" maxW="calc(100vw - 100px)" overflowX="auto">
        <Table.Root size="sm" showColumnBorder>
          <Table.Header>
            <Table.Row>
              <Table.ColumnHeader fontSize="md">ПІБ</Table.ColumnHeader>
              {Array.from({ length: maxAssignments }, (_, index) => (
                <Table.ColumnHeader fontSize="md" key={index}>
                  {index + 1}
                </Table.ColumnHeader>
              ))}
              <Table.ColumnHeader fontSize="md">Сума</Table.ColumnHeader>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {data.map((item) => (
              <Table.Row key={item.userId}>
                <Table.Cell
                  fontSize="md"
                  display="flex"
                  gap={2}
                  justifyContent="flex-start"
                  alignItems="center"
                >
                  <UserAvatar />
                  {`${item.userSecondName} ${item.userFirstName}`}
                </Table.Cell>
                {item.assignments.map((assignment, index) => {
                  let bgColor = '';
                  if (assignment.isCorrect) {
                    bgColor = 'green.100';
                  } else if (assignment.points > 0) {
                    bgColor = 'yellow.100';
                  } else {
                    bgColor = 'red.100';
                  }
                  return (
                    <Table.Cell fontSize="md" key={index} bg={bgColor} textAlign="center">
                      {`${assignment.points}/${assignment.maxPoints}`}
                    </Table.Cell>
                  );
                })}
                {/* Fill in empty cells if assignments are fewer than maxAssignments */}
                {Array.from({ length: maxAssignments - item.assignments.length }).map(
                  (_, index) => (
                    <Table.Cell fontSize="md" key={`empty-${index}`} />
                  ),
                )}
                <Table.Cell fontSize="md" textAlign="center">
                  {`${item.score}/${item.maxScore}`}
                </Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table.Root>
      </Table.ScrollArea>
    </>
  );
};

export default ClassMarksDetails;
