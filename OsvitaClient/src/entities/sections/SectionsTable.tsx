import { Table, Box } from '@chakra-ui/react';
import { SectionsTableRow } from './SectionsTableRow';
import { Section } from './types';

interface SectionsTableProps {
  items: Section[];
}

export function SectionsTable({ items }: SectionsTableProps) {
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
            <SectionsTableRow key={item.id} item={item} />
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
