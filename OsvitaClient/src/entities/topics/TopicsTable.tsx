import { Table, Box } from '@chakra-ui/react';
import { TopicsTableRow } from './TopicsTableRow';
import { Topic } from './types';

interface TopicsTableProps {
  items: Topic[];
}

export function TopicsTable({ items }: TopicsTableProps) {
  const sortedItems = [...items].sort((a, b) => a.id - b.id);

  return (
    <Box width="100%">
      <Table.ScrollArea w="full" maxW="calc(100vw - 100px)" overflowX="auto">
        <Table.Root size="lg">
          <Table.Header>
            <Table.Row>
              <Table.ColumnHeader color="orange">Назва</Table.ColumnHeader>
              <Table.ColumnHeader textAlign="center" color="orange">
                Розділ
              </Table.ColumnHeader>
              <Table.ColumnHeader textAlign="start" color="orange">
                Дії
              </Table.ColumnHeader>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {sortedItems.map((item) => (
              <TopicsTableRow key={item.id} item={item} />
            ))}
          </Table.Body>
        </Table.Root>
      </Table.ScrollArea>
    </Box>
  );
}
