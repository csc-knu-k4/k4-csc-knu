import { Table, Box } from '@chakra-ui/react';
import { AssignmentTableRow } from './AssignmentTableRow';
import { Assignment } from './types';

interface AssignmentTableProps {
  items: Assignment[];
}

export function AssignmentTable({ items }: AssignmentTableProps) {
  const sortedItems = [...items].sort((a, b) => a.id - b.id);

  return (
    <Box overflowX="auto" w="full">
      <Table.Root size="lg">
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader color="orange">Тема</Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="orange">
              Дії
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {sortedItems.map((item) => (
            <AssignmentTableRow key={item.id} item={item} />
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
