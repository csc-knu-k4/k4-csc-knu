import { getUserById, User } from '@/shared/api/userApi';
import { useQuery } from 'react-query';

const getUserIdFromStorage = (): number | null => {
  const raw = localStorage.getItem('userId');
  return raw ? Number(raw) : null;
};

export const useUser = () => {
  const userId = getUserIdFromStorage();

  return useQuery<User>({
    queryKey: ['user', userId],
    queryFn: () => getUserById(userId!),
    enabled: !!userId,
    staleTime: 5 * 60 * 1000,
  });
};
