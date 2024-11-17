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
import { updateMaterial } from '@/shared/api/materialsApi';

interface EditMaterialModalProps {
  isOpen: boolean;
  onClose: () => void;
  initialTitle: string;
  topicId: number;
  orderPosition: number;
  contentBlocksIds: number[];
}

export const EditMaterialModal = ({
  isOpen,
  onClose,
  initialTitle,
  orderPosition,
  topicId,
  contentBlocksIds,
}: EditMaterialModalProps) => {
  const [title, setTitle] = useState(initialTitle);
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (updatedMaterial: {
      id: number;
      title: string;
      topicId: number;
      orderPosition: number;
      contentBlocksIds: number[];
    }) => updateMaterial(updatedMaterial.id, updatedMaterial),
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
      id: topicId,
      title: title.trim(),
      topicId,
      orderPosition,
      contentBlocksIds,
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
