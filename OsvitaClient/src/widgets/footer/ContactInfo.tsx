import { Text, Highlight, Flex } from '@chakra-ui/react';

export const ContactInfo = () => (
  <Flex flexDir="column">
    <Text fontSize="2xl" fontWeight="bold">
      <Highlight query="Зв’яжіться" styles={{ color: 'orange' }}>
        Зв’яжіться
      </Highlight>{' '}
      з нами
    </Text>
    <Text fontSize="sm">У вас є запитання чи пропозиції?</Text>
    <Text fontSize="sm">Напишіть нам, і ми з радістю відповімо на всі ваші запитання!</Text>
  </Flex>
);
