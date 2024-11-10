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
        <Field color="blue" label="Наповнення" required errorText="Поле обов'язкове">
          <Textarea
            _placeholder={{ color: 'inherit' }}
            color="blue.100"
            borderColor="blue"
            placeholder="Начніть друкувати..."
            variant="outline"
          />
        </Field>
      </HStack>
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

export default AddMaterial;
