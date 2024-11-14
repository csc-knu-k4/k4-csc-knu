import { Text, Highlight, Flex } from '@chakra-ui/react';
import {
  AccordionItem,
  AccordionItemContent,
  AccordionItemTrigger,
  AccordionRoot,
} from '@/components/ui/accordion';

const items = [
  { value: 'a', title: 'Що таке НМТ ?', text: 'Some value 1...' },
  { value: 'b', title: 'Як почати підготовку ?', text: 'Some value 2...' },
  { value: 'c', title: 'Як відслідковувати прогрес ?', text: 'Some value 3...' },
  { value: 'd', title: 'Чи можна обирати теми ?', text: 'Some value 3...' },
];

const FAQSection = () => {
  return (
    <Flex flexDir="column" justifyContent="center" alignItems="center">
      <Text fontSize="2xl" fontWeight="bold" mt="130px" textAlign="center">
        Часті{' '}
        <Highlight query="запитання" styles={{ color: '#FF6D18' }}>
          запитання
        </Highlight>
      </Text>
      <AccordionRoot multiple mt={8} maxW="52.5rem">
        {items.map((item, index) => (
          <AccordionItem borderColor="#5C6CFF" key={index} value={item.value}>
            <AccordionItemTrigger>{item.title}</AccordionItemTrigger>
            <AccordionItemContent fontSize="md">{item.text}</AccordionItemContent>
          </AccordionItem>
        ))}
      </AccordionRoot>
    </Flex>
  );
};

export default FAQSection;
