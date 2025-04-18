import { getRecomendationMessages } from '@/shared/api/recomendationMessagesApi';
import { Drawer, Portal, Button, Text, VStack, Box, Flex, IconButton } from '@chakra-ui/react';
import { useQuery } from 'react-query';
import { MdError, MdDeleteOutline } from 'react-icons/md';

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
            <Drawer.Body pt="6" px={0} pb="8" bgColor="white">
              <Text ml={4} color="black" fontSize="xl" fontWeight="bold" mb={4}>
                📩 Повідомлення
              </Text>

              {messages && messages.length > 0 ? (
                <VStack align="start" gap={0}>
                  {messages.map((msg, idx) => (
                    <Box
                      key={idx}
                      p={3}
                      borderLeft="3px solid"
                      borderColor={msg.isRead ? 'transperent' : 'orange.300'}
                      bg={msg.isRead ? 'gray.50' : 'orange.50'}
                      w="100%"
                    >
                      <Flex flexDir="column" gap={2}>
                        <Flex flexDir="row" alignItems="center" gap={2}>
                          <MdError color="#F97316" size="1.5rem" />
                          <Text fontSize="lg" fontWeight="bold" color="black">
                            Непрочитане повідомлення
                          </Text>
                          <IconButton
                            aria-label="Delete notification"
                            variant="ghost"
                            colorPalette="red"
                            ml="auto"
                            mr={2}
                          >
                            <MdDeleteOutline size="1.5rem" />
                          </IconButton>
                        </Flex>
                        <Text color="black" fontWeight={msg.isRead ? 'normal' : 'normal'}>
                          {msg.recomendationText}
                        </Text>
                        <Text fontSize="sm" color="gray.600">
                          {new Date(msg.creationDate).toLocaleString()}
                        </Text>
                      </Flex>
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
