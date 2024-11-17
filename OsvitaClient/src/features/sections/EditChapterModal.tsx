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
import { updateChapter } from '@/shared/api/chaptersApi';

interface EditChapterModalProps {
  isOpen: boolean;
  onClose: () => void;
  initialTitle: string;
  chapterId: number;
  subjectId: number;
  orderPosition: number;
  topicsIds: number[];
}

export const EditChapterModal = ({
  isOpen,
  onClose,
  initialTitle,
  chapterId,
  subjectId,
  orderPosition,
  topicsIds,
}: EditChapterModalProps) => {
  const [title, setTitle] = useState(initialTitle);
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (updatedChapter: {
      id: number;
      title: string;
      subjectId: number;
      orderPosition: number;
      topicsIds: number[];
    }) => updateChapter(updatedChapter.id, updatedChapter),
    onSuccess: () => {
      queryClient.invalidateQueries(['chapters']);
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
      subjectId,
      orderPosition,
      topicsIds,
    });
  };

  return (
    <DialogRoot lazyMount open={isOpen} onOpenChange={() => onClose()}>
      <DialogContent>
        <DialogHeader>Редагувати розділ</DialogHeader>
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
