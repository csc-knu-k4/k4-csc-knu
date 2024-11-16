import { useState } from 'react';
import {
  DialogBody,
  DialogCloseTrigger,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogRoot,
  DialogTitle,
  Button,
  Input,
  Text,
  Flex,
} from '@chakra-ui/react';
import { toaster } from '@/components/ui/toaster';
import { useMutation, useQueryClient } from 'react-query';
import { updateSubject } from '@/shared/api/subjectsApi';

interface EditSubjectModalProps {
  open: boolean;
  subject: { id: number; title: string; chaptersIds: number[] };
}

export const EditSubjectModal = ({ open, subject }: EditSubjectModalProps) => {
  const [title, setTitle] = useState(subject.title);
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: (updatedSubject: { id: number; title: string; chaptersIds: number[] }) =>
      updateSubject(updatedSubject.id, updatedSubject),
    onSuccess: () => {
      queryClient.invalidateQueries(['subjects']);
      toaster.create({
        title: 'Предмет оновлено!',
        type: 'success',
        duration: 3000,
      });
    },
    onError: () => {
      toaster.create({
        title: 'Помилка при оновленні предмета.',
        type: 'error',
        duration: 3000,
      });
    },
  });

  const handleSave = () => {
    if (!title.trim()) {
      toaster.create({
        title: 'Назва предмета не може бути порожньою.',
        type: 'warning',
        duration: 3000,
      });
      return;
    }

    mutation.mutate({ id: subject.id, title: title.trim(), chaptersIds: subject.chaptersIds });
  };

  return (
    <DialogRoot open={open}>
      <DialogCloseTrigger />
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Редагувати предмет</DialogTitle>
        </DialogHeader>
        <DialogBody>
          <Flex flexDir="column" gap={4}>
            <Text>Назва</Text>
            <Input
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              placeholder="Введіть назву предмета"
            />
          </Flex>
        </DialogBody>
        <DialogFooter>
          <Button mr={3}>Скасувати</Button>
          <Button onClick={handleSave} bgColor="orange">
            Зберегти
          </Button>
        </DialogFooter>
      </DialogContent>
    </DialogRoot>
  );
};
