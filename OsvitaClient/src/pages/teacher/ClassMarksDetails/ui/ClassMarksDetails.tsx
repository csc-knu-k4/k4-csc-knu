import { UserAvatar } from '@/shared/ui/Avatar';
import { Table } from '@chakra-ui/react';

const ClassMarksDetails = () => {
  return (
    <>
      <Table.Root size="sm" showColumnBorder>
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader fontSize="md">ПІБ</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">1</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">2</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">3</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">4</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">5</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">6</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">7</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">8</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">9</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">10</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">Сума</Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {items.map((item) => (
            <Table.Row key={item.id}>
              <Table.Cell
                fontSize="md"
                display="flex"
                gap={2}
                justifyContent="flex-start"
                alignItems="center"
              >
                <UserAvatar />
                {item.name}
              </Table.Cell>
              <Table.Cell fontSize="md">{item.mark1}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark2}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark3}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark4}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark5}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark6}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark7}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark8}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark9}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark10}</Table.Cell>
              <Table.Cell fontSize="md">20/20</Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table.Root>
    </>
  );
};

const items = [
  {
    id: 1,
    name: 'Петренко Олександр',
    mark1: '1/1',
    mark2: '1/1',
    mark3: '1/1',
    mark4: '1/1',
    mark5: '1/1',
    mark6: '1/1',
    mark7: '1/1',
    mark8: '1/1',
    mark9: '3/3',
    mark10: '2/2',
  },
  {
    id: 2,
    name: 'Ковальчук Ірина',
    mark1: '1/1',
    mark2: '1/1',
    mark3: '1/1',
    mark4: '1/1',
    mark5: '1/1',
    mark6: '1/1',
    mark7: '1/1',
    mark8: '1/1',
    mark9: '3/3',
    mark10: '2/2',
  },
  {
    id: 3,
    name: 'Мельник Сергій',
    mark1: '1/1',
    mark2: '1/1',
    mark3: '1/1',
    mark4: '1/1',
    mark5: '1/1',
    mark6: '1/1',
    mark7: '1/1',
    mark8: '1/1',
    mark9: '3/3',
    mark10: '2/2',
  },
];

export default ClassMarksDetails;
