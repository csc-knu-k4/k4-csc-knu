import { sendAssistantMessage } from '@/shared/api/assistantApt';
import { Button, Drawer, Input, Text, VStack, Spinner, Portal } from '@chakra-ui/react';
import { useState } from 'react';

export const AssistantDrawer = () => {
  const [open, setOpen] = useState(false);
  const [request, setRequest] = useState('');
  const [response, setResponse] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const userId = Number(localStorage.getItem('userId'));

  const handleSend = async () => {
    setLoading(true);
    try {
      const data = await sendAssistantMessage({ userId, requestText: request });
      setResponse(data.responseText);
    } catch {
      setResponse('Виникла помилка під час запиту.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Drawer.Root open={open} onOpenChange={(e) => setOpen(e.open)} size="sm">
        <Drawer.Trigger asChild>
          <Button
            position="fixed"
            bottom="20px"
            right="20px"
            colorScheme="orange"
            borderRadius="full"
            boxShadow="md"
            zIndex={9999}
          >
            🤖 Асистент
          </Button>
        </Drawer.Trigger>
        <Portal>
          <Drawer.Backdrop />
          <Drawer.Positioner>
            <Drawer.Content>
              <Drawer.Header bgColor="white" color="black" fontSize="lg" fontWeight="bold">
                🧠 Асистент
              </Drawer.Header>
              <Drawer.Body bgColor="white">
                <VStack gap={2}>
                  <Input
                    borderRadius="1rem"
                    placeholder="Введіть свій запит"
                    color="black"
                    value={request}
                    onChange={(e) => setRequest(e.target.value)}
                  />
                  <Button
                    onClick={handleSend}
                    w="100%"
                    disabled={!request.trim()}
                    fontSize="lg"
                    color="white"
                    bgColor="orange"
                    borderRadius="1rem"
                  >
                    Надіслати
                  </Button>

                  {loading && <Spinner />}
                  {response && (
                    <Text color="black" whiteSpace="pre-wrap">
                      {response}
                    </Text>
                  )}
                </VStack>
              </Drawer.Body>
            </Drawer.Content>
          </Drawer.Positioner>
        </Portal>
      </Drawer.Root>
    </>
  );
};
