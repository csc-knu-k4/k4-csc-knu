import api from './api';

export const getChartSubject = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/charts/subjects`);
  return response.data;
};

export const getChartTopic = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/charts/topics`);
  return response.data;
};
