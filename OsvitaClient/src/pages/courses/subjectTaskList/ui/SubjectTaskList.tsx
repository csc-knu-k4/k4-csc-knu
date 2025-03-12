import { Flex, Stack, Text, Button } from '@chakra-ui/react';
import {
  AccordionItem,
  AccordionItemContent,
  AccordionItemTrigger,
  AccordionRoot,
} from '@/components/ui/accordion';
import { Checkbox } from '@/components/ui/checkbox';
import { Link } from 'react-router-dom';

const SubjectTaskList = () => {
  return (
    <Flex justifyContent="center" alignItems="center" flexDir="column">
      <Text fontSize="2xl" fontWeight="bold" mb={4} color="orange">
        Математика
      </Text>
      <Stack width="full">
        <AccordionRoot collapsible defaultValue={['info']}>
          {items.map((item) => (
            <AccordionItem
              px={4}
              py={2}
              key={item.value}
              value={item.value}
              boxShadow="0rem 0.13rem 0.31rem 0rem rgba(0, 0, 0, 0.15);"
              borderRadius="1rem"
              mb={3}
            >
              <AccordionItemTrigger>{item.title}</AccordionItemTrigger>
              <AccordionItemContent>
                <Flex direction="column" gap={3} mt={2}>
                  {item.tasks.map((task, index) => (
                    <Flex key={index} alignItems="center" gap={3}>
                      <Checkbox variant="outline" colorPalette="orange" />
                      <Text flex="1">{task.text}</Text>
                      {task.testLink && (
                        <Link to={task.testLink}>
                          <Button bgColor="orange" w="8rem" h="2.5rem" borderRadius="1rem">
                            Тест
                          </Button>
                        </Link>
                      )}
                      {task.materialLink && (
                        <Link to={task.materialLink}>
                          <Button bgColor="blue" w="8rem" h="2.5rem" borderRadius="1rem">
                            Матеріал
                          </Button>
                        </Link>
                      )}
                    </Flex>
                  ))}
                </Flex>
              </AccordionItemContent>
            </AccordionItem>
          ))}
        </AccordionRoot>
      </Stack>
    </Flex>
  );
};

const items = [
  {
    value: '1',
    title:
      'Раціональні, ірраціональні, степеневі, показникові, логарифмічні, тригонометричні вирази та їх перетворення',
    tasks: [
      { text: 'Основні властивості степенів', testLink: '/test1' },
      { text: 'Раціональні вирази', materialLink: '/material1' },
      { text: 'Логарифмічні вирази', testLink: '/test2', materialLink: '/material2' },
    ],
  },
  {
    value: '2',
    title:
      'Лінійні, квадратні, раціональні, ірраціональні, показникові, логарифмічні, тригонометричні рівняння і нерівності',
    tasks: [
      { text: 'Лінійні рівняння', testLink: '/test3' },
      { text: 'Квадратні рівняння', materialLink: '/material3' },
      { text: 'Тригонометричні рівняння', testLink: '/test4', materialLink: '/material4' },
    ],
  },
  {
    value: '3',
    title:
      'Лінійні, квадратні, раціональні, ірраціональні, показникові, логарифмічні, тригонометричні рівняння і нерівності',
    tasks: [
      { text: 'Лінійні рівняння', testLink: '/test3' },
      { text: 'Квадратні рівняння', materialLink: '/material3' },
      { text: 'Тригонометричні рівняння', testLink: '/test4', materialLink: '/material4' },
    ],
  },
  {
    value: '4',
    title:
      'Лінійні, квадратні, раціональні, ірраціональні, показникові, логарифмічні, тригонометричні рівняння і нерівності',
    tasks: [
      { text: 'Лінійні рівняння', testLink: '/test3' },
      { text: 'Квадратні рівняння', materialLink: '/material3' },
      { text: 'Тригонометричні рівняння', testLink: '/test4', materialLink: '/material4' },
    ],
  },
  {
    value: '5',
    title:
      'Лінійні, квадратні, раціональні, ірраціональні, показникові, логарифмічні, тригонометричні рівняння і нерівності',
    tasks: [
      { text: 'Лінійні рівняння', testLink: '/test3' },
      { text: 'Квадратні рівняння', materialLink: '/material3' },
      { text: 'Тригонометричні рівняння', testLink: '/test4', materialLink: '/material4' },
    ],
  },
];

export default SubjectTaskList;
