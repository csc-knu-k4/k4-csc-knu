import { Box, Button, Flex, Highlight, VStack, Text } from '@chakra-ui/react';
import {
  DialogBody,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogRoot,
} from '@/components/ui/dialog';
import { useQuery } from 'react-query';
import { Assignment, getAssignmentById } from '@/shared/api/testsApi';
import { Checkbox } from '@/components/ui/checkbox';

interface ViewTestModalProps {
  isOpen: boolean;
  onClose: () => void;
  testId: number;
}

export const ViewTestModal = ({ isOpen, onClose, testId }: ViewTestModalProps) => {
  const { data: test, isLoading } = useQuery(['test', testId], () => getAssignmentById(testId), {
    enabled: isOpen,
  });

  if (isLoading || !test) {
    return null;
  }

  return (
    <DialogRoot lazyMount open={isOpen} onOpenChange={() => onClose()}>
      <DialogContent>
        <DialogHeader fontSize="lg">{test.problemDescription}</DialogHeader>
        <DialogBody>
          {test.assignmentModelType === 0 && (
            <VStack align="start">
              {test.answers.map((answer: { value: string; isCorrect: boolean }, index: number) => (
                <Flex gap={2} key={index}>
                  <Highlight
                    query={answer.value}
                    styles={{ fontWeight: 'semibold', color: answer.isCorrect ? 'green' : 'black' }}
                  >
                    {`${String.fromCharCode(1040 + index)} ${answer.value}`}
                  </Highlight>
                </Flex>
              ))}
            </VStack>
          )}

          {test.assignmentModelType === 1 && (
            <Box>
              <Text fontSize="md" my={3}>
                Відповідь:{' '}
                <Text as="span" fontWeight="bold" color="green">
                  {test.answers[0]?.value}
                </Text>
              </Text>
            </Box>
          )}

          {test.assignmentModelType === 2 && test.childAssignments && (
            <Box>
              <Text fontSize="md" mb={3}>
                Встановіть відповідність:
              </Text>
              <Flex flexDir="column" alignItems="center">
                <Flex ml={6} flexDir="row" gap={2} mb={2}>
                  {test.childAssignments
                    .flatMap((c: Assignment) => c.answers)
                    .map((answer: { value: string }, index: number) => (
                      <Text
                        key={index}
                        fontWeight="bold"
                        color="orange"
                        textAlign="center"
                        w="2rem"
                      >
                        {answer.value}
                      </Text>
                    ))}
                </Flex>
                {test.childAssignments.map((child: Assignment, rowIndex: number) => (
                  <Flex key={rowIndex} flexDir="row" alignItems="center" gap={4} mb={2}>
                    <Text fontWeight="bold" color="orange" w="0.5rem" textAlign="left">
                      {child.problemDescription}
                    </Text>
                    {test.childAssignments
                      ?.flatMap((c: Assignment) => c.answers)
                      .map((answer: { value: string }, colIndex: number) => {
                        const isCorrect = child.answers.some(
                          (a: { value: string; isCorrect: boolean }) =>
                            a.value === answer.value && a.isCorrect,
                        );
                        return (
                          <Checkbox
                            key={colIndex}
                            size="lg"
                            checked={isCorrect}
                            readOnly
                            colorPalette={isCorrect ? 'green' : 'red'}
                          />
                        );
                      })}
                  </Flex>
                ))}
              </Flex>
            </Box>
          )}
        </DialogBody>
        <DialogFooter>
          <Button variant="outline" mr={3} onClick={onClose}>
            Закрити
          </Button>
        </DialogFooter>
      </DialogContent>
    </DialogRoot>
  );
};
