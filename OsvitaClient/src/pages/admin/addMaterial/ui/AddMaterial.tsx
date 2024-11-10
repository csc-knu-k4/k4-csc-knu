import { Button } from '@/components/ui/button';
import { Flex, HStack, Input, Text, Textarea } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';

const AddMaterial = () => {
  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати матеріал
      </Text>
      <HStack gap="10" width="full">
        <Field color="#5C6CFF" label="Наповнення" required errorText="Поле обов'язкове">
          <Textarea
            _placeholder={{ color: 'inherit' }}
            color="#B1B8FF"
            borderColor="#5C6CFF"
            placeholder="Начніть друкувати..."
            variant="outline"
          />
        </Field>
      </HStack>
      <Field label="Предмет" required mb={3} color="#5C6CFF">
        <Input
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть предмет"
          color="#B1B8FF"
          borderColor="#5C6CFF"
        />
      </Field>
      <Button bgColor="#5C6CFF">Додати</Button>
    </Flex>
  );
};

export default AddMaterial;
