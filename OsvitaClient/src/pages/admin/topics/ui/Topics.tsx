import { Button } from '@/components/ui/button';
import { Text, Flex, Table, IconButton, Box } from '@chakra-ui/react';
import { GoPlusCircle } from 'react-icons/go';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { useNavigate } from 'react-router-dom';

const Topics = () => {
  const navigate = useNavigate();

  const handleAddTopic = () => {
    navigate('/add-topic');
  };

  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Теми
        </Text>
        <Button
          onClick={handleAddTopic}
          p={2}
          fontSize="xl"
          variant="outline"
          borderRadius="0.5rem"
          border="1px solid black"
        >
          <GoPlusCircle size="1.5rem" color="#5C6CFF" /> Додати розділ
        </Button>
      </Flex>
      <Box overflowX="auto" w="full">
        <Table.Root size="lg">
          <Table.Header>
            <Table.Row>
              <Table.ColumnHeader color="#5C6CFF">Назва</Table.ColumnHeader>
              <Table.ColumnHeader textAlign="center" color="#5C6CFF">
                Предмет
              </Table.ColumnHeader>
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
              <Table.Row bgColor={item.id % 2 === 0 ? 'white' : '#F4F5FF'} key={item.id}>
                <Table.Cell textAlign="start" whiteSpace="nowrap">
                  {item.topic}
                </Table.Cell>
                <Table.Cell w="full" textAlign="center">
                  {item.sectionName}
                </Table.Cell>
                <Table.Cell textAlign="start" whiteSpace="nowrap">
                  {item.date}
                </Table.Cell>
                <Table.Cell>
                  <Flex gap={5} justifyContent="flex-end">
                    <IconButton aria-label="Watch" bgColor="#E7E9FF">
                      <IoEyeOutline color="#5C6CFF" />
                    </IconButton>
                    <IconButton aria-label="Edit" bgColor="#E7E9FF">
                      <TbEdit color="#5C6CFF" />
                    </IconButton>
                    <IconButton aria-label="Delete" bgColor="#E7E9FF">
                      <MdDeleteOutline color="#5C6CFF" />
                    </IconButton>
                  </Flex>
                </Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table.Root>
      </Box>
    </>
  );
};

const items = [
  { id: 1, topic: 'Властивості коренів', sectionName: 'Числа і вирази', date: '20.10.2024, 16:32' },
  {
    id: 2,
    topic: 'Властивості дій з дійсними числа...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
  {
    id: 3,
    topic: 'Правила порівняння дійсними чис...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
  {
    id: 4,
    topic: 'Правила знаходження найбільшог...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
  {
    id: 5,
    topic: 'Модуль дійсного числа та ймовір...',
    sectionName: 'Числа і вирази',
    date: '20.10.2024, 16:32',
  },
];

export default Topics;
