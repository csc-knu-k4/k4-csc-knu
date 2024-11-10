import { Button } from '@/components/ui/button';
import { Flex, Input, Text } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';

const AddTopic = () => {
  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати тему
      </Text>
      <Field label="Назва" required mb={3} color="#5C6CFF">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть назву"
          color="#B1B8FF"
          borderColor="#5C6CFF"
        />
      </Field>
      <Field label="Розділ" required mb={3} color="#5C6CFF">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть розділ"
          color="#B1B8FF"
          borderColor="#5C6CFF"
        />
      </Field>
      <Button bgColor="#5C6CFF">Додати</Button>
    </Flex>
  );
};

export default AddTopic;
