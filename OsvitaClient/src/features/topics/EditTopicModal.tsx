import { useState } from 'react';
import { Button, Input, Text, Flex } from '@chakra-ui/react';
import {
  DialogBody,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogRoot,
} from '@/components/ui/dialog';
import { useMutation, useQueryClient } from 'react-query';
import { updateTopic } from '@/shared/api/topicsApi';

interface EditTopicModalProps {
  isOpen: boolean;
  onClose: () => void;
  initialTitle: string;
  topicId: number;
  chapterId: number;
  orderPosition: number;
  materialIds: number[];
}

export const EditTopicModal = ({
  isOpen,
  onClose,
  initialTitle,
  chapterId,
  orderPosition,
  materialIds,
}: EditTopicModalProps) => {
  const [title, setTitle] = useState(initialTitle);
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (updatedTopic: {
      id: number;
      title: string;
      chapterId: number;
      orderPosition: number;
      materialIds: number[];
    }) => updateTopic(updatedTopic.id, updatedTopic),
    onSuccess: () => {
      queryClient.invalidateQueries(['topics']);
      onClose();
    },
    onError: (error: string) => {
      console.error('Error updating chapter:', error);
    },
  });

  const handleSave = () => {
    if (!title.trim()) {
      alert('Назва не може бути пустою!');
      return;
    }

    mutation.mutate({
      id: chapterId,
      title: title.trim(),
      chapterId,
      orderPosition,
      materialIds,
    });
  };

  return (
    <DialogRoot lazyMount open={isOpen} onOpenChange={() => onClose()}>
      <DialogContent>
        <DialogHeader>Редагувати тему</DialogHeader>
        <DialogBody>
          <Flex flexDir="column" gap={4}>
            <Text fontWeight="medium">Назва</Text>
            <Input
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              placeholder="Введіть нову назву"
            />
          </Flex>
        </DialogBody>
        <DialogFooter>
          <Button variant="ghost" mr={3} onClick={onClose}>
            Скасувати
          </Button>
          <Button bgColor="orange" onClick={handleSave}>
            Зберегти
          </Button>
        </DialogFooter>
      </DialogContent>
    </DialogRoot>
  );
};
