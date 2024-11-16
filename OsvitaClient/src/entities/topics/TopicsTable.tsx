import { Table, Box } from '@chakra-ui/react';
import { TopicsTableRow } from './TopicsTableRow';
import { Topic } from './types';

interface TopicsTableProps {
  items: Topic[];
}

export function TopicsTable({ items }: TopicsTableProps) {
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
              Створено
            </Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="orange">
              Дії
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {items.map((item) => (
            <TopicsTableRow key={item.id} item={item} />
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
