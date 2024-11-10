import { Button } from '@/components/ui/button';
import { Flex, Input, Text } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';

const AddSection = () => {
  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати розділ
      </Text>
      <Field label="Назва" required mb={3} color="blue">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть назву"
          color="blue.100"
          borderColor="blue"
        />
      </Field>
      <Field label="Предмет" required mb={3} color="blue">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть предмет"
          color="blue.100"
          borderColor="blue"
        />
      </Field>
      <Button bgColor="blue">Додати</Button>
    </Flex>
  );
};

export default AddSection;
