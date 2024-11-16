import { Button } from '@/components/ui/button';
import { Flex, Input, Text } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';

const AddSection = () => {
  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати розділ
      </Text>
      <Field label="Назва" required mb={3} color="orange">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть назву"
          color="orange.placeholder"
          borderColor="orange"
        />
      </Field>
      <Field label="Предмет" required mb={3} color="orange">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть предмет"
          color="orange.placeholder"
          borderColor="orange"
        />
      </Field>
      <Button bgColor="orange">Додати</Button>
    </Flex>
  );
};

export default AddSection;
