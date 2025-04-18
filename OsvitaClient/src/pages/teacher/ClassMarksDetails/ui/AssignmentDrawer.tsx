import { getAssignmentById } from '@/shared/api/testsApi';
import { Drawer, Portal, Spinner, Text, Image, Box, Button } from '@chakra-ui/react';
import { useQuery } from 'react-query';

interface Props {
  assignmentId: number;
  open: boolean;
  onClose: () => void;
}

export const AssignmentDrawer = ({ assignmentId, open, onClose }: Props) => {
  const { data, isLoading, isError } = useQuery({
    queryKey: ['assignment', assignmentId],
    queryFn: () => getAssignmentById(assignmentId),
    enabled: open,
  });

  const renderProblem = (problem: string, img?: string | null) => (
    <Box mb={4}>
      <Text fontSize="md" mb={2}>
        {problem}
      </Text>
      {img && (
        <Image
          src={`${'http://localhost:5134/'}${img}`}
          alt="problem"
          borderRadius="md"
          maxW="100%"
        />
      )}
    </Box>
  );

  return (
    <Drawer.Root size="sm" open={open} onOpenChange={(e) => !e.open && onClose()}>
      <Portal>
        <Drawer.Backdrop />
        <Drawer.Positioner>
          <Drawer.Content>
            <Drawer.Body pt="6" px="5" pb="8" bgColor="white">
              {isLoading ? (
                <Spinner />
              ) : isError || !data ? (
                <Text color="red.500">Не вдалося завантажити завдання</Text>
              ) : (
                <>
                  <Box color="black">
                    <Text color="black" fontWeight="bold" fontSize="lg" mb={2}>
                      Умова:
                    </Text>
                    {renderProblem(data.problemDescription, data.problemDescriptionImage)}
                  </Box>
                  {data.assignmentModelType === 2 ? (
                    <Box color="black">
                      {Array.isArray(data.childAssignments) && (
                        <Box color="black" mt={6}>
                          <Text fontWeight="bold" fontSize="lg" mb={2}>
                            Відповідність:
                          </Text>

                          {data.childAssignments.map((child: any) => (
                            <Box key={child.id} mb={2}>
                              <Text>{child.problemDescription}</Text>
                            </Box>
                          ))}

                          <Box mt={4}>
                            <Text fontWeight="bold" fontSize="lg" mb={2}>
                              Варіанти відповідей:
                            </Text>

                            {data.childAssignments
                              .flatMap((child: any) => child.answers || [])
                              .map((answer: any) => (
                                <Box key={answer.id} mb={1}>
                                  <Text>- {answer.value}</Text>
                                  {answer.valueImage && (
                                    <Image
                                      src={`${'http://localhost:5134/'}${answer.valueImage}`}
                                      alt="answer image"
                                      maxW="100%"
                                      borderRadius="md"
                                    />
                                  )}
                                </Box>
                              ))}
                          </Box>
                        </Box>
                      )}
                    </Box>
                  ) : null}
                </>
              )}
            </Drawer.Body>
            <Drawer.CloseTrigger asChild>
              <Button
                onClick={onClose}
                color="white"
                fontWeight="bold"
                fontSize="lg"
                colorPalette="orange"
              >
                Закрити
              </Button>
            </Drawer.CloseTrigger>
          </Drawer.Content>
        </Drawer.Positioner>
      </Portal>
    </Drawer.Root>
  );
};
