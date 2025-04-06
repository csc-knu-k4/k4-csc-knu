import { Button } from '@/components/ui/button';
import { Text, Highlight, Input, Flex } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

import Layout from '@/app/layouts/AuthLayout/AuthLayout';
import { register } from '@/shared/api/auth';
import { toaster } from '@/components/ui/toaster';

const Register = () => {
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [secondName, setSecondName] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [selectedRole, setSelectedRole] = useState<'student' | 'teacher' | null>(null);

  const handleRegister = async () => {
    if (!selectedRole) {
      toaster.error({
        title: 'Будь ласка, оберіть роль!',
        type: 'error',
      });
      return;
    }

    setIsLoading(true);
    try {
      await register({
        email,
        password,
        firstName,
        secondName,
        roles: [selectedRole],
      });
      toaster.success({
        title: 'Реєстрація успішна',
        type: 'info',
      });
      navigate('/login');
    } catch (error) {
      toaster.error({
        title: `Помилка при реєстрації ${error}`,
        type: 'error',
      });
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
      <Text mt={6} fontSize="lg">
        Виберіть роль
      </Text>
      <Flex mt={3} gap={2} justifyContent="center">
        <Button
          colorPalette={selectedRole === 'student' ? 'orange' : 'gray'}
          variant={selectedRole === 'student' ? 'solid' : 'outline'}
          fontSize="lg"
          w="10rem"
          onClick={() => setSelectedRole('student')}
        >
          Учень
        </Button>
        <Button
          colorPalette={selectedRole === 'teacher' ? 'orange' : 'gray'}
          variant={selectedRole === 'teacher' ? 'solid' : 'outline'}
          fontSize="lg"
          w="10rem"
          onClick={() => setSelectedRole('teacher')}
        >
          Вчитель
        </Button>
      </Flex>
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
