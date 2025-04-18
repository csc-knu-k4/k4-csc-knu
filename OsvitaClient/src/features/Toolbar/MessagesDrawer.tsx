import { getRecomendationMessages } from '@/shared/api/recomendationMessagesApi';
import { Drawer, Portal, Button, Text, VStack, Box } from '@chakra-ui/react';
import { useQuery } from 'react-query';

interface Props {
  open: boolean;
  onClose: () => void;
}

export const MessagesDrawer = ({ open, onClose }: Props) => {
  const userId = Number(localStorage.getItem('userId'));

  const { data: messages } = useQuery({
    queryKey: ['messages', userId],
    queryFn: () => getRecomendationMessages(userId),
    enabled: open && !!userId,
  });

  return (
    <Drawer.Root size="sm" open={open} onOpenChange={(e) => !e.open && onClose()}>
      <Portal>
        <Drawer.Backdrop />
        <Drawer.Positioner>
          <Drawer.Content>
            <Drawer.Body pt="6" px="5" pb="8" bgColor="white">
              <Text color="black" fontSize="xl" fontWeight="bold" mb={4}>
                📩 Повідомлення
              </Text>

              {messages && messages.length > 0 ? (
                <VStack align="start" gap={4}>
                  {messages.map((msg, idx) => (
                    <Box
                      key={idx}
                      p={3}
                      border="1px solid"
                      borderColor={msg.isRead ? 'gray.200' : 'orange.300'}
                      borderRadius="md"
                      bg={msg.isRead ? 'gray.50' : 'orange.50'}
                      w="100%"
                    >
                      <Text fontSize="sm" color="black">
                        {new Date(msg.creationDate).toLocaleString()}
                      </Text>
                      <Text color="gray.600" fontWeight={msg.isRead ? 'normal' : 'semibold'}>
                        {msg.recomendationText}
                      </Text>
                    </Box>
                  ))}
                </VStack>
              ) : (
                <Text>Немає нових повідомлень</Text>
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
