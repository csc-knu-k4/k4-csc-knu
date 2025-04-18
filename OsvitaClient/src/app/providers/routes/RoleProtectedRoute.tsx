import { Navigate, Outlet } from 'react-router-dom';
import { useQuery } from 'react-query';
import { getUserById } from '@/shared/api/userApi';
import { Center, Spinner } from '@chakra-ui/react';

const getUserId = () => Number(localStorage.getItem('userId'));
const getToken = () => localStorage.getItem('authToken');

interface RoleProtectedRouteProps {
  allowedRoles: string[];
  redirectPath?: string;
}

const RoleProtectedRoute = ({ allowedRoles, redirectPath = '/login' }: RoleProtectedRouteProps) => {
  const userId = getUserId();
  const token = getToken();

  const { data: user, isLoading } = useQuery(['user', userId], () => getUserById(userId), {
    enabled: !!userId && !!token,
  });

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  if (isLoading) {
    return (
      <Center h="100vh">
        <Spinner size="xl" color="orange.400" />
      </Center>
    );
  }

  if (!user || !user.roles.some((r) => allowedRoles.includes(r))) {
    return <Navigate to={redirectPath} replace />;
  }

  return <Outlet />;
};

export default RoleProtectedRoute;
