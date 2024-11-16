import { useState } from 'react';
import { Button, Input, Text, Flex } from '@chakra-ui/react';
import {
  DialogBody,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogRoot,
} from '@/components/ui/dialog';

interface EditChapterModalProps {
  isOpen: boolean;
  onClose: () => void;
  initialTitle: string;
}

export const EditChapterModal = ({ isOpen, onClose, initialTitle }: EditChapterModalProps) => {
  const [title, setTitle] = useState(initialTitle);

  const handleSave = () => {
    if (!title.trim()) {
      alert('Назва не може бути пустою!');
      return;
    }
  };

  return (
    <DialogRoot lazyMount open={isOpen} onOpenChange={() => onClose()}>
      <DialogContent>
        <DialogHeader>Редагувати предмет</DialogHeader>
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
