import api from './api';

export const getDailyAssignmentSet = async (userId: number) => {
  const response = await api.get(`/users/${userId}/dailyassignmentset`);
  return response.data;
};

export const getDailyAssignmentStreak = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/dailyassignment/streak`);
  return response.data;
};

export const getDailyAssignmentIsDone = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/dailyassignment/isdone`);
  return response.data;
};

export const getDailyAssignmentRating = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/dailyassignment/rating`);
  return response.data;
};
