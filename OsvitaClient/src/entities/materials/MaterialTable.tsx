import { Table, Box } from '@chakra-ui/react';
import { MaterialTableRow } from './MaterialTableRow';
import { Material } from './types';

interface MaterialTableProps {
  items: Material[];
}

export function MaterialTable({ items }: MaterialTableProps) {
  return (
    <Box overflowX="auto" w="full">
      <Table.Root size="lg">
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader color="#5C6CFF">Тема</Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="#5C6CFF">
              Створено
            </Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="#5C6CFF">
              Дії
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {items.map((item) => (
            <MaterialTableRow key={item.id} item={item} />
          ))}
        </Table.Body>
      </Table.Root>
    </Box>
  );
}
