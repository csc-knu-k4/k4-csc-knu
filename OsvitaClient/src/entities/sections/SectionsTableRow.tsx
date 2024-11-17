import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { Section } from './types';
import { ActionButtons } from '@/shared/ui/ActionButtons';

interface SectionsTableRowProps {
  item: Section;
}

export function SectionsTableRow({ item }: SectionsTableRowProps) {
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
    <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
      <Table.Cell textAlign="start" whiteSpace="nowrap">
        {item.topic}
      </Table.Cell>
      <Table.Cell w="full" textAlign="center">
        {item.subjectName}
      </Table.Cell>
      <Table.Cell textAlign="start" whiteSpace="nowrap">
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
