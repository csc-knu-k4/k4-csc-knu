import { Text, Highlight, Flex } from '@chakra-ui/react';
import { Accordion } from './Accordion';
import { faqItems } from './faqConfig';

const FAQSection = () => (
  <Flex flexDir="column" justifyContent="center" alignItems="center">
    <Text fontSize="2xl" fontWeight="bold" mt="130px" textAlign="center">
      Часті{' '}
      <Highlight query="запитання" styles={{ color: 'orange' }}>
        запитання
      </Highlight>
    </Text>
    <Accordion items={faqItems} />
  </Flex>
);

export default FAQSection;
