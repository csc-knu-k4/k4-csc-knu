import { useEffect, useState } from 'react';
import { Button, Flex, Menu, Table, Text } from '@chakra-ui/react';
import { HiDotsVertical } from 'react-icons/hi';
import { useNavigate, useParams } from 'react-router-dom';
import {
  getClassesEducationPlan,
  getClassesStatisticAssignmentsByAssingmentSetId,
} from '@/shared/api/classesApi';
import { downloadClassAssignmentReportPdf } from '@/shared/api/reportsApi';

const ClassMarks = () => {
  const navigate = useNavigate();
  const { classId } = useParams<{ classId: string }>();
  const [loading, setLoading] = useState(true);
  const [studentsData, setStudentsData] = useState<any[]>([]);
  const [assignmentSets, setAssignmentSets] = useState<any[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      if (!classId) return;
      setLoading(true);
      try {
        const plan = await getClassesEducationPlan(Number(classId));
        const assignmentSetDetails = plan.assignmentSetPlanDetails || [];

        const assignmentSetIds = assignmentSetDetails.map((detail: any) => detail.assignmentSetId);
        setAssignmentSets(assignmentSetIds);

        const studentsMap: { [key: number]: any } = {};

        for (const assignmentSetId of assignmentSetIds) {
          const stats = await getClassesStatisticAssignmentsByAssingmentSetId(
            Number(classId),
            assignmentSetId,
          );
          const reports = stats.assignmetSetReportModels || [];

          for (const report of reports) {
            const studentId = report.userId;
            if (!studentsMap[studentId]) {
              studentsMap[studentId] = {
                id: studentId,
                name: `${report.userSecondName} ${report.userFirstName}`,
                marks: {},
              };
            }
            studentsMap[studentId].marks[assignmentSetId] = report.completedDate
              ? `${report.score}/${report.maxScore}`
              : 'Немає';
          }
        }

        setStudentsData(Object.values(studentsMap));
      } catch (error) {
        console.error('Помилка при завантаженні даних:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [classId]);

  if (loading) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <Table.ScrollArea w="full" maxW="calc(100vw - 100px)" overflowX="auto">
      <Table.Root size="sm" showColumnBorder>
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader fontSize="md">ПІБ</Table.ColumnHeader>
            {assignmentSets.map((assignmentSetId, index) => (
              <Table.ColumnHeader key={assignmentSetId} fontSize="md">
                <Flex flexDir="row" alignItems="center">
                  Тест {index + 1}
                  <Menu.Root>
                    <Menu.Trigger asChild>
                      <Button variant="plain">
                        <HiDotsVertical size="20px" />
                      </Button>
                    </Menu.Trigger>
                    <Menu.Positioner>
                      <Menu.Content>
                        <Menu.Item
                          value="View"
                          onClick={() =>
                            navigate(`/teacher/${classId}/class-marks/details/${assignmentSetId}`)
                          }
                        >
                          Переглянути детальніше
                        </Menu.Item>
                        <Menu.Item
                          value="DownloadPDF"
                          onClick={() =>
                            downloadClassAssignmentReportPdf(Number(classId), assignmentSetId)
                          }
                        >
                          Вивантажити статистику PDF
                        </Menu.Item>
                      </Menu.Content>
                    </Menu.Positioner>
                  </Menu.Root>
                </Flex>
              </Table.ColumnHeader>
            ))}
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {studentsData.map((student) => (
            <Table.Row key={student.id}>
              <Table.Cell fontSize="md">{student.name}</Table.Cell>
              {assignmentSets.map((assignmentSetId) => (
                <Table.Cell key={assignmentSetId} fontSize="md">
                  {student.marks[assignmentSetId] || 'Немає'}
                </Table.Cell>
              ))}
            </Table.Row>
          ))}
        </Table.Body>
      </Table.Root>
    </Table.ScrollArea>
  );
};

export default ClassMarks;
