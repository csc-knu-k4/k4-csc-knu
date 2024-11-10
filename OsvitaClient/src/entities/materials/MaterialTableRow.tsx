import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { Material } from './types';
import { ActionButtons } from '@/shared/ui/ActionButtons';

interface MaterialTableRowProps {
  item: Material;
}

export function MaterialTableRow({ item }: MaterialTableRowProps) {
  const handleView = () => {
    console.log('View', item.id);
  };

  const handleEdit = () => {
    console.log('Edit', item.id);
  };

  const handleDelete = () => {
    console.log('Delete', item.id);
  };

  return (
    <Table.Row bgColor={item.id % 2 === 0 ? 'white' : '#F4F5FF'}>
      <Table.Cell w="full">{item.topic}</Table.Cell>
      <Table.Cell textAlign="end" whiteSpace="nowrap">
        {item.date}
      </Table.Cell>
      <Table.Cell>
        <ActionButtons
          actions={[
            { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: handleView },
            { icon: <TbEdit />, ariaLabel: 'Edit', onClick: handleEdit },
            { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
          ]}
        />
      </Table.Cell>
    </Table.Row>
  );
}
