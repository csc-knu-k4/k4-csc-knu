import { Button } from '@/components/ui/button';
import { Text, Highlight, Input } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

import Layout from '@/app/layouts/AuthLayout/AuthLayout';
import { register } from '@/shared/api/auth';

const Register = () => {
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [secondName, setSecondName] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleRegister = async () => {
    setIsLoading(true);
    try {
      await register({
        email,
        password,
        firstName,
        secondName,
        roles: ['student'],
      });
      navigate('/login');
    } catch (error) {
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Layout>
      <Text mb={6} color="orange" fontSize="2xl" fontWeight="bold">
        Реєстрація
      </Text>
      <Field label="Електронна пошта">
        <Input
          variant="flushed"
          placeholder="Введіть електронну пошту"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </Field>
      <Field label="Пароль" mt={6}>
        <Input
          variant="flushed"
          placeholder="Введіть пароль"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </Field>
      <Field label="Ім’я" mt={6}>
        <Input
          variant="flushed"
          placeholder="Введіть ім’я"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
      </Field>
      <Field label="Прізвище" mt={6}>
        <Input
          variant="flushed"
          placeholder="Введіть прізвище"
          value={secondName}
          onChange={(e) => setSecondName(e.target.value)}
        />
      </Field>
      <Button
        mt={6}
        fontSize="lg"
        w="full"
        borderRadius="1.5rem"
        bgColor="orange"
        loading={isLoading}
        onClick={handleRegister}
      >
        Зареєструватись
      </Button>
      <Text onClick={() => navigate('/login')} color="gray" mt={6}>
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
