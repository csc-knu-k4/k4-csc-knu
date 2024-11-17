import { useState } from 'react';
import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { useMutation, useQueryClient } from 'react-query';
import { deleteSubject, updateSubject } from '@/shared/api/subjectsApi';
import { ActionButtons } from '@/shared/ui/ActionButtons';
import { EditSubjectModal } from '@/features/subjects/EditSubjectModal';
import { useNavigate } from 'react-router-dom';

interface Subject {
  id: number;
  title: string;
  chaptersIds: number[];
}

interface SubjectsTableRowProps {
  item: Subject;
}

export function SubjectsTableRow({ item }: SubjectsTableRowProps) {
  const [isEditOpen, setEditOpen] = useState(false);
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const handleViewChapters = () => {
    navigate(`/admin/chapters`, { state: { subjectId: item.id } });
  };

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
      setEditOpen(false);
    },
  });

  const handleDelete = () => {
    deleteMutation.mutate(item.id);
  };

  const handleUpdate = (newTitle: string) => {
    updateMutation.mutate({ id: item.id, title: newTitle, chaptersIds: item.chaptersIds });
  };

  return (
    <>
      <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
        <Table.Cell w="full">{item.title}</Table.Cell>
        <Table.Cell>
          <ActionButtons
            actions={[
              { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: handleViewChapters },
              { icon: <TbEdit />, ariaLabel: 'Edit', onClick: () => setEditOpen(true) },
              { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
            ]}
          />
        </Table.Cell>
      </Table.Row>

      <EditSubjectModal
        isOpen={isEditOpen}
        onClose={() => setEditOpen(false)}
        onSubmit={handleUpdate}
        initialTitle={item.title}
      />
    </>
  );
}
