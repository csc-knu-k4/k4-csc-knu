import { Table, Box } from '@chakra-ui/react';
import { ChaptersTableRow } from './ChaptersTableRow';
import { Section } from './types';

interface ChaptersTableProps {
  items: Section[];
}

export function ChaptersTable({ items }: ChaptersTableProps) {
  const sortedItems = [...items].sort((a, b) => a.id - b.id);

  return (
    <Box overflowX="auto" w="full">
      <Table.Root size="lg">
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader color="orange">Назва</Table.ColumnHeader>
            <Table.ColumnHeader textAlign="center" color="orange">
              Предмет
            </Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="orange">
              Дії
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {sortedItems.map((item) => (
            <ChaptersTableRow key={item.id} item={item} />
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
