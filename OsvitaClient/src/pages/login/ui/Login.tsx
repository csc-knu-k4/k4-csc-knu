import { Button } from '@/components/ui/button';
import { Text, Highlight, Input, Flex } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { Checkbox } from '@/components/ui/checkbox';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useDispatch } from 'react-redux';

import Layout from '@/app/layouts/AuthLayout/AuthLayout';
import { login } from '@/shared/api/auth';

const Login = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleLogin = async () => {
    setIsLoading(true);
    try {
      await login({ email, password }, dispatch);
      console.log('Зашов');
      navigate('/admin');
    } catch (error) {
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Layout>
      <Text mb={6} color="orange" fontSize="2xl" fontWeight="bold">
        Вхід
      </Text>
      <Field label="Електронна пошта">
        <Input
          type="email"
          variant="flushed"
          placeholder="Введіть електронну пошту"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </Field>
      <Field label="Пароль" mt={6}>
        <Input
          type="password"
          variant="flushed"
          placeholder="Введіть пароль"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
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
      <Button
        fontSize="lg"
        w="full"
        borderRadius="1.5rem"
        bgColor="orange"
        loading={isLoading}
        onClick={handleLogin}
      >
        Увійти
      </Button>
      <Text onClick={() => navigate('/register')} color="gray" mt={6}>
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
