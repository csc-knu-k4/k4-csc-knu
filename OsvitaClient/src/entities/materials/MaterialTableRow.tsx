import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { Material } from './types';
import { ActionButtons } from '@/shared/ui/ActionButtons';
import { useState } from 'react';
import { useMutation, useQueryClient } from 'react-query';
import { deleteMaterial } from '@/shared/api/materialsApi';
import { EditMaterialModal } from '@/features/materials/EditMaterialModal';

interface MaterialTableRowProps {
  item: Material;
}

export function MaterialTableRow({ item }: MaterialTableRowProps) {
  const [isEditOpen, setEditOpen] = useState(false);
  const queryClient = useQueryClient();

  const deleteMutation = useMutation({
    mutationFn: deleteMaterial,
    onSuccess: () => {
      queryClient.invalidateQueries(['materials']);
    },
  });

  const handleDelete = () => {
    deleteMutation.mutate(item.id);
  };

  return (
    <>
      <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
        <Table.Cell w="full">{item.title}</Table.Cell>
        <Table.Cell w="full" fontWeight="bold">
          {item.id}
        </Table.Cell>
        <Table.Cell>
          <ActionButtons
            actions={[
              { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: () => console.log('View') },
              { icon: <TbEdit />, ariaLabel: 'Edit', onClick: () => setEditOpen(true) },
              { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
            ]}
          />
        </Table.Cell>
      </Table.Row>

      <EditMaterialModal
        isOpen={isEditOpen}
        onClose={() => setEditOpen(false)}
        initialTitle={item.title}
        topicId={item.topicId}
        orderPosition={item.orderPosition}
        contentBlocksIds={item.contentBlocksIds}
      />
    </>
  );
}
