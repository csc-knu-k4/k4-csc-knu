import { Button } from '@/components/ui/button';
import { Text, Highlight, Input, Flex } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { Checkbox } from '@/components/ui/checkbox';
import { useNavigate } from 'react-router-dom';
import Layout from '@/app/layouts/AuthLayout/AuthLayout';

const Login = () => {
  const navigate = useNavigate();

  const handleRegister = () => {
    navigate('/register');
  };

  return (
    <Layout>
      <Text mb={6} color="orange" fontSize="2xl" fontWeight="bold">
        Вхід
      </Text>
      <Field label="Електронна пошта">
        <Input type="email" variant="flushed" placeholder="Введіть електронну пошту" />
      </Field>
      <Field label="Пароль" mt={6}>
        <Input type="password" variant="flushed" placeholder="Введіть пароль" />
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
      <Text onClick={handleRegister} color="gray" mt={6}>
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
    </Layout>
  );
};

export default Login;
