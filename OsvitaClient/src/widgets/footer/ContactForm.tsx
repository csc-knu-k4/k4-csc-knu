import { Input, chakra, Flex } from '@chakra-ui/react';
import AutoResize from 'react-textarea-autosize';
import { Button } from '@/components/ui/button';

const StyledAutoResize = chakra(AutoResize);

export const ContactForm = () => (
  <Flex flexDir="column">
    <Flex flexDir="row" gap={6} mt={4}>
      <Input bgColor="white" fontSize="sm" placeholder="Ім’я" />
      <Input bgColor="white" fontSize="sm" placeholder="Email" />
    </Flex>
    <StyledAutoResize
      p={3}
      fontSize="sm"
      bgColor="white"
      placeholder="Повідомлення"
      mt={3}
      minH="100px"
      resize="none"
      overflow="hidden"
      lineHeight="inherit"
    />
    <Button maxW="17.5rem" mt={4} bgColor="orange" borderRadius="1rem">
      Відправити повідомлення
    </Button>
  </Flex>
);
