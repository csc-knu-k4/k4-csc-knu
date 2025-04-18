import { Navigate, Outlet } from 'react-router-dom';
import { useQuery } from 'react-query';
import { getUserById } from '@/shared/api/userApi';

const getUserIdFromStorage = () => Number(localStorage.getItem('userId'));
const getToken = () => localStorage.getItem('accessToken');

interface RoleProtectedRouteProps {
  allowedRoles: string[];
  redirectPath?: string;
}

const RoleProtectedRoute = ({ allowedRoles, redirectPath = '/login' }: RoleProtectedRouteProps) => {
  const token = getToken();
  const userId = getUserIdFromStorage();

  const { data: user, isLoading } = useQuery(['user', userId], () => getUserById(userId), {
    enabled: !!token && !!userId,
  });

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  if (isLoading) return null;

  if (!user || !user.roles.some((role) => allowedRoles.includes(role))) {
    return <Navigate to={redirectPath} replace />;
  }

  return <Outlet />;
};

export default RoleProtectedRoute;
