import api from './api';

export const getUserStatisticbyId = async (userId: number): Promise<any> => {
  const response = await api.get(`/users/${userId}/statistic`);
  return response.data;
};

export const addStatisticAssignments = async (userId: number, assignments: any) => {
  const response = await api.post(`/users/${userId}/statistic/assignments`, assignments);
  return response.data;
};
