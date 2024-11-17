import { Button } from '@/components/ui/button';
import { Container, Text, Highlight } from '@chakra-ui/react';
import { Input } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { useNavigate } from 'react-router-dom';

const Register = () => {
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate('/login');
  };

  return (
    <Container maxW="500px" centerContent>
      <Text color="orange" fontSize="2xl" fontWeight="bold">
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
    </Container>
  );
};

export default Register;
