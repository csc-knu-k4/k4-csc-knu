import { Table, Box } from '@chakra-ui/react';
import { AssignmentTableRow } from './AssignmentTableRow';
import { Assignment } from '@/shared/api/testsApi';

interface AssignmentTableProps {
  items: Assignment[];
}

export function AssignmentTable({ items }: AssignmentTableProps) {
  const sortedItems = [...items].sort((a, b) => a.id - b.id);

  return (
    <Box width="100%">
      <Table.ScrollArea w="full" maxW="calc(100vw - 100px)" overflowX="auto">
        <Table.Root size="lg">
          <Table.Header justifyContent="space-between">
            <Table.Row>
              <Table.ColumnHeader color="orange">Умова тесту</Table.ColumnHeader>
              <Table.ColumnHeader color="orange">Дії</Table.ColumnHeader>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {sortedItems.map((item) => (
              <AssignmentTableRow key={item.id} item={item} />
            ))}
          </Table.Body>
        </Table.Root>
      </Table.ScrollArea>
    </Box>
  );
}
