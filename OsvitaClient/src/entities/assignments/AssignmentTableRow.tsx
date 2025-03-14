import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { ActionButtons } from '@/shared/ui/ActionButtons';
import { useMutation, useQueryClient } from 'react-query';
import { Assignment, deleteAssignment } from '@/shared/api/testsApi';
import { useState } from 'react';
import { ViewTestModal } from '@/features/assingmentsFeatures';

interface AssignmentTableRowProps {
  item: Assignment;
}

export function AssignmentTableRow({ item }: AssignmentTableRowProps) {
  const queryClient = useQueryClient();
  const [isViewOpen, setViewOpen] = useState(false);

  const deleteMutation = useMutation({
    mutationFn: deleteAssignment,
    onSuccess: () => {
      queryClient.invalidateQueries(['assingments']);
    },
  });

  const handleDelete = () => {
    deleteMutation.mutate(item.id);
  };

  return (
    <>
      <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
        <Table.Cell w="full">{item.problemDescription}</Table.Cell>
        <Table.Cell>
          <ActionButtons
            actions={[
              { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: () => setViewOpen(true) },
              { icon: <TbEdit />, ariaLabel: 'Edit', onClick: () => console.log('Edit') },
              { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
            ]}
          />
        </Table.Cell>
      </Table.Row>

      <ViewTestModal isOpen={isViewOpen} onClose={() => setViewOpen(false)} testId={item.id} />
    </>
  );
}
