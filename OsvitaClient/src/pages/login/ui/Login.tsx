import { Button } from '@/components/ui/button';
import { Container, Flex, Text, Highlight } from '@chakra-ui/react';
import { Input } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { Checkbox } from '@/components/ui/checkbox';

const Login = () => {
  return (
    <Container maxW="500px" centerContent>
      <Text color="orange" fontSize="2xl" fontWeight="bold">
        Вхід
      </Text>
      <Field label="Електронна пошта">
        <Input variant="flushed" placeholder="Введіть електронну пошту" />
      </Field>
      <Field label="Пароль" mt={6}>
        <Input variant="flushed" placeholder="Введіть пароль" />
      </Field>
      <Flex my={7} w="full" flexDir="row" justifyContent="space-between" alignItems="center">
        <Checkbox color="gray" colorPalette="orange">
          Запам’ятати мене
        </Checkbox>
        <Text
          color="orange"
          _hover={{
            textDecoration: 'underline',
          }}
        >
          Забули пароль?
        </Text>
      </Flex>
      <Button fontSize="lg" w="full" borderRadius="1.5rem" bgColor="orange">
        Увійти
      </Button>
      <Text color="gray" mt={6}>
        Не маєте акаунту?{' '}
        <Highlight
          query="Створити акаунт"
          styles={{
            color: 'orange',
            _hover: {
              textDecoration: 'underline',
            },
          }}
        >
          Створити акаунт
        </Highlight>
      </Text>
    </Container>
  );
};

export default Login;
