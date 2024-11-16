import { useState } from 'react';
import { Table, Button, Input, Text, Flex } from '@chakra-ui/react';
import {
  DialogBody,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogRoot,
} from '@/components/ui/dialog';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { useMutation, useQueryClient } from 'react-query';
import { deleteSubject, updateSubject } from '@/shared/api/subjectsApi';
import { ActionButtons } from '@/shared/ui/ActionButtons';

interface Subject {
  id: number;
  title: string;
  chaptersIds: number[];
}

interface SubjectsTableRowProps {
  item: Subject;
}

export function SubjectsTableRow({ item }: SubjectsTableRowProps) {
  const [open, setOpen] = useState(false);
  const [title, setTitle] = useState(item.title);
  const queryClient = useQueryClient();

  const deleteMutation = useMutation({
    mutationFn: deleteSubject,
    onSuccess: () => {
      queryClient.invalidateQueries(['subjects']);
    },
  });

  const updateMutation = useMutation({
    mutationFn: (updatedSubject: Subject) => updateSubject(item.id, updatedSubject),
    onSuccess: () => {
      queryClient.invalidateQueries(['subjects']);
      setOpen(false);
    },
  });

  const handleDelete = () => {
    deleteMutation.mutate(item.id);
  };

  const handleUpdate = () => {
    if (!title.trim()) {
      alert('Назва не може бути пустою!');
      return;
    }
    updateMutation.mutate({ id: item.id, title: title.trim(), chaptersIds: item.chaptersIds });
  };

  return (
    <>
      <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
        <Table.Cell w="full">{item.title}</Table.Cell>
        <Table.Cell>
          <ActionButtons
            actions={[
              { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: () => console.log('View') },
              { icon: <TbEdit />, ariaLabel: 'Edit', onClick: () => setOpen(true) },
              { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
            ]}
          />
        </Table.Cell>
      </Table.Row>

      {/* Modal for Editing */}
      <DialogRoot lazyMount open={open} onOpenChange={(e) => setOpen(e.open)}>
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
            <Button variant="ghost" mr={3} onClick={() => setOpen(false)}>
              Скасувати
            </Button>
            <Button bgColor="orange" onClick={handleUpdate}>
              Зберегти
            </Button>
          </DialogFooter>
        </DialogContent>
      </DialogRoot>
    </>
  );
}
