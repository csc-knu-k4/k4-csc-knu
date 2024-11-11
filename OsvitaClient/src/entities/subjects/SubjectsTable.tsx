import { Table, Box } from '@chakra-ui/react';
import { SubjectsTableRow } from './SubjectsTableRow';
import { Subject } from './types';

interface SubjectsTableProps {
  items: Subject[];
}

export function SubjectsTable({ items }: SubjectsTableProps) {
  return (
    <Box overflowX="auto" w="full">
      <Table.Root size="lg">
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader color="blue">Назва</Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="blue">
              Створено
            </Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="blue">
              Дії
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {items.map((item) => (
            <SubjectsTableRow key={item.id} item={item} />
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
