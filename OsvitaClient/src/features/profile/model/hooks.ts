import {
  getDailyAssignmentIsDone,
  getDailyAssignmentRating,
  getDailyAssignmentSet,
  getDailyAssignmentStreak,
  RatingItem,
} from '@/shared/api/dailyAssignmentApt';
import { getUserById, User } from '@/shared/api/userApi';
import { useQuery } from 'react-query';

export const getUserIdFromStorage = (): number | null => {
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

export const useDailyAssignmentStatus = () => {
  const userId = getUserIdFromStorage();

  return useQuery<boolean>({
    queryKey: ['dailyAssignmentIsDone', userId],
    queryFn: () => getDailyAssignmentIsDone(userId!),
    enabled: !!userId,
    staleTime: 1000 * 60 * 5,
  });
};

export const useDailyStreak = () => {
  const userId = getUserIdFromStorage();

  return useQuery<number>({
    queryKey: ['dailyAssignmentStreak', userId],
    queryFn: () => getDailyAssignmentStreak(userId!),
    enabled: !!userId,
    staleTime: 1000 * 60 * 5,
  });
};
export const useRating = () => {
  return useQuery<RatingItem[]>({
    queryKey: ['dailyRating'],
    queryFn: getDailyAssignmentRating,
    staleTime: 5 * 60 * 1000,
  });
};

export const useDailyAssignmentSet = () => {
  const userId = getUserIdFromStorage();

  return useQuery({
    queryKey: ['dailyAssignmentSet', userId],
    queryFn: () => getDailyAssignmentSet(userId!),
    enabled: !!userId,
    staleTime: 1000 * 60 * 5,
  });
};
