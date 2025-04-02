import { Button, Flex, Menu, Table } from '@chakra-ui/react';
import { HiDotsVertical } from 'react-icons/hi';

const ClassMarks = () => {
  return (
    <>
      <Table.Root size="sm" showColumnBorder>
        <Table.Header>
          <Table.Row>
            <Table.ColumnHeader fontSize="md">ПІБ</Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">
              <Flex flexDir="row">
                Тест 1. Властивості дій з дійсними числами
                <Menu.Root>
                  <Menu.Trigger asChild>
                    <Button variant="plain">
                      <HiDotsVertical size="42px" />
                    </Button>
                  </Menu.Trigger>
                  <Menu.Positioner>
                    <Menu.Content>
                      <Menu.Item value="View">Переглянути детальніше</Menu.Item>
                      <Menu.Item value="DownloadPDF">Вивантажити статистику PDF</Menu.Item>
                      <Menu.Item value="delete">Видалити</Menu.Item>
                    </Menu.Content>
                  </Menu.Positioner>
                </Menu.Root>
              </Flex>
            </Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">
              <Flex flexDir="row">
                Тест 2. Властивості дій з дійсними числами
                <Menu.Root>
                  <Menu.Trigger asChild>
                    <Button variant="plain">
                      <HiDotsVertical size="42px" />
                    </Button>
                  </Menu.Trigger>
                  <Menu.Positioner>
                    <Menu.Content>
                      <Menu.Item value="View">Переглянути детальніше</Menu.Item>
                      <Menu.Item value="DownloadPDF">Вивантажити статистику PDF</Menu.Item>
                      <Menu.Item value="delete">Видалити</Menu.Item>
                    </Menu.Content>
                  </Menu.Positioner>
                </Menu.Root>
              </Flex>
            </Table.ColumnHeader>
            <Table.ColumnHeader fontSize="md">
              <Flex flexDir="row">
                Тест 3. Властивості дій з дійсними числами
                <Menu.Root>
                  <Menu.Trigger asChild>
                    <Button variant="plain">
                      <HiDotsVertical size="42px" />
                    </Button>
                  </Menu.Trigger>
                  <Menu.Positioner>
                    <Menu.Content>
                      <Menu.Item value="View">Переглянути детальніше</Menu.Item>
                      <Menu.Item value="DownloadPDF">Вивантажити статистику PDF</Menu.Item>
                      <Menu.Item value="delete">Видалити</Menu.Item>
                    </Menu.Content>
                  </Menu.Positioner>
                </Menu.Root>
              </Flex>
            </Table.ColumnHeader>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {items.map((item) => (
            <Table.Row key={item.id}>
              <Table.Cell fontSize="md">{item.name}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark1}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark2}</Table.Cell>
              <Table.Cell fontSize="md">{item.mark3}</Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table.Root>
    </>
  );
};

const items = [
  { id: 1, name: 'Петренко Олександр', mark1: '20/20', mark2: '18/20', mark3: '15/20' },
  { id: 2, name: 'Ковальчук Ірина', mark1: '20/20', mark2: '18/20', mark3: '15/20' },
  { id: 3, name: 'Мельник Сергій', mark1: '20/20', mark2: '18/20', mark3: '15/20' },
];

export default ClassMarks;
