import { Input, chakra, Flex } from '@chakra-ui/react';
import AutoResize from 'react-textarea-autosize';
import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { toaster } from '@/components/ui/toaster';

const StyledAutoResize = chakra(AutoResize);

export const ContactForm = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');

  const handleSubmit = () => {
    toaster.create({
      title: `Повідомлення відправлено`,
      type: 'success',
    });
    setName('');
    setEmail('');
    setMessage('');
  };

  return (
    <Flex flexDir="column">
      <Flex flexDir="row" gap={6} mt={4}>
        <Input
          bgColor="white"
          fontSize="sm"
          placeholder="Ім’я"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <Input
          bgColor="white"
          fontSize="sm"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
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
        value={message}
        onChange={(e) => setMessage(e.target.value)}
      />
      <Button maxW="17.5rem" mt={4} bgColor="orange" borderRadius="1rem" onClick={handleSubmit}>
        Відправити повідомлення
      </Button>
    </Flex>
  );
};
