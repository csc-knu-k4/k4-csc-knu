import { Button } from '@/components/ui/button';
import { Text, Highlight } from '@chakra-ui/react';
import { Input } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { useNavigate } from 'react-router-dom';
import Layout from '@/app/layouts/AuthLayout/AuthLayout';

const Register = () => {
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate('/login');
  };

  return (
    <Layout>
      <Text mb={6} color="orange" fontSize="2xl" fontWeight="bold">
        Реєстрація
      </Text>
      <Field label="Електронна пошта">
        <Input variant="flushed" placeholder="Введіть електронну пошту" />
      </Field>
      <Field label="Пароль" mt={6}>
        <Input variant="flushed" placeholder="Введіть пароль" />
      </Field>
      <Field label="Ім’я" mt={6}>
        <Input variant="flushed" placeholder="Введіть ім’я" />
      </Field>
      <Field label="Прізвище" mt={6}>
        <Input variant="flushed" placeholder="Введіть прізвище" />
      </Field>
      <Button mt={6} fontSize="lg" w="full" borderRadius="1.5rem" bgColor="orange">
        Зареєструватись
      </Button>
      <Text onClick={handleLogin} color="gray" mt={6}>
        Вже маєте акаунт?{' '}
        <Highlight
          query="Увійти"
          styles={{
            color: 'orange',
            _hover: {
              textDecoration: 'underline',
            },
          }}
        >
          Увійти
        </Highlight>
      </Text>
    </Layout>
  );
};

export default Register;
