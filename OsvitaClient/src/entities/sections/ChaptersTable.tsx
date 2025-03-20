import { Table, Box } from '@chakra-ui/react';
import { ChaptersTableRow } from './ChaptersTableRow';
import { Section } from './types';

interface ChaptersTableProps {
  items: Section[];
}

export function ChaptersTable({ items }: ChaptersTableProps) {
  const sortedItems = [...items].sort((a, b) => a.id - b.id);

  return (
    <Box width="100%">
      <Table.ScrollArea w="full" maxW="calc(100vw - 100px)" overflowX="auto">
        <Table.Root size="lg" minWidth="800px">
          <Table.Header>
            <Table.Row>
              <Table.ColumnHeader color="orange" minWidth="250px">
                Назва
              </Table.ColumnHeader>
              <Table.ColumnHeader textAlign="center" color="orange" minWidth="200px">
                Предмет
              </Table.ColumnHeader>
              <Table.ColumnHeader textAlign="start" color="orange" minWidth="150px">
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
      </Table.ScrollArea>
    </Box>
  );
}
