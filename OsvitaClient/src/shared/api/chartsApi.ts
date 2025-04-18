import api from './api';

export type User = {
  id: number;
  email: string;
  firstName: string;
  secondName: string;
  statisticModelId: number;
  educationClassesIds: number[];
  roles: string[];
};

export const getChartSubject = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/charts/subjects`);
  return response.data;
};

export const getChartTopic = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/charts/topics`);
  return response.data;
};
