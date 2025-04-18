import { useState } from 'react';
import { Outlet } from 'react-router-dom';
import { Box, Input, Button, Text, VStack } from '@chakra-ui/react';

const ADMIN_CREDENTIALS = {
  login: 'admin',
  password: 'admin',
};

export const AdminAuthGuard = () => {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [authFailed, setAuthFailed] = useState(false);

  const isAuthenticated = localStorage.getItem('isAdmin') === 'true';

  const handleLogin = () => {
    if (login === ADMIN_CREDENTIALS.login && password === ADMIN_CREDENTIALS.password) {
      localStorage.setItem('isAdmin', 'true');
      window.location.reload();
    } else {
      setAuthFailed(true);
    }
  };

  if (isAuthenticated) return <Outlet />;

  return (
    <Box maxW="400px" mx="auto" p={6} boxShadow="md" borderRadius="lg" bg="white">
      <VStack gap={4}>
        <Text fontSize="xl" fontWeight="bold" color="orange.500">
          Вхід до адмін-панелі
        </Text>
        <Input placeholder="Логін" value={login} onChange={(e) => setLogin(e.target.value)} />
        <Input
          placeholder="Пароль"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <Button onClick={handleLogin} colorScheme="orange" w="full">
          Увійти
        </Button>
        {authFailed && (
          <Text color="red.500" fontSize="sm">
            Неправильний логін або пароль
          </Text>
        )}
      </VStack>
    </Box>
  );
};
