import { Table, Flex, IconButton } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { Subject } from './types';

interface SubjectsTableRowProps {
  item: Subject;
}

export function SubjectsTableRow({ item }: SubjectsTableRowProps) {
  return (
    <Table.Row bgColor={item.id % 2 === 0 ? 'white' : '#F4F5FF'}>
      <Table.Cell w="full">{item.subjectName}</Table.Cell>
      <Table.Cell textAlign="end" whiteSpace="nowrap">
        {item.date}
      </Table.Cell>
      <Table.Cell>
        <Flex gap={5} justifyContent="flex-end">
          <IconButton aria-label="Watch" bgColor="#E7E9FF">
            <IoEyeOutline color="#5C6CFF" />
          </IconButton>
          <IconButton aria-label="Edit" bgColor="#E7E9FF">
            <TbEdit color="#5C6CFF" />
          </IconButton>
          <IconButton aria-label="Delete" bgColor="#E7E9FF">
            <MdDeleteOutline color="#5C6CFF" />
          </IconButton>
        </Flex>
      </Table.Cell>
    </Table.Row>
  );
}
