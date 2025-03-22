import api from './api';

export const getUserStatisticbyId = async (userId: number): Promise<any> => {
  const response = await api.get(`/users/${userId}/statistic`);
  return response.data;
};

export const addStatisticAssignments = async (userId: number, assignments: any) => {
  const response = await api.post(`/users/${userId}/statistic/assignments`, assignments);
  return response.data;
};

export const addStatisticTopic = async (userId: number, topic: any) => {
  const response = await api.post(`/users/${userId}/statistic/topics`, topic);
  return response.data;
};

export const updateStatisticTopic = async (userId: number, topic: any) => {
  const response = await api.put(`/users/${userId}/statistic/topics`, topic);
  return response.data;
};
