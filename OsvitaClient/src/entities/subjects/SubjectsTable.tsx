import { Table, Box } from '@chakra-ui/react';
import { SubjectsTableRow } from './SubjectsTableRow';

interface Subject {
  id: number;
  title: string;
  chaptersIds: number[];
}

interface SubjectsTableProps {
  items: Subject[];
}

export function SubjectsTable({ items }: SubjectsTableProps) {
  return (
    <Box overflowX="auto" w="full">
      <Table.Root size="lg">
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader color="orange">Назва</Table.ColumnHeader>
            <Table.ColumnHeader textAlign="start" color="orange">
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
