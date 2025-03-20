import { Table, Box } from '@chakra-ui/react';
import { MaterialTableRow } from './MaterialTableRow';
import { Material } from './types';

interface MaterialTableProps {
  items: Material[];
}

export function MaterialTable({ items }: MaterialTableProps) {
  const sortedItems = [...items].sort((a, b) => a.id - b.id);

  return (
    <Box width="100%">
      <Table.ScrollArea w="full" maxW="calc(100vw - 100px)" overflowX="auto">
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
              <MaterialTableRow key={item.id} item={item} />
            ))}
          </Table.Body>
        </Table.Root>
      </Table.ScrollArea>
    </Box>
  );
}
