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

export const getUserById = async (userId: number): Promise<User> => {
  const response = await api.get(`/users/${userId}`);
  return response.data;
};

export const getUserEducationPlanById = async (userId: number) => {
  const response = await api.get(`/users/${userId}/educationplanvm`);
  return response.data;
};

export const getUserClassEducationPlansById = async (userId: number) => {
  const response = await api.get(`/users/${userId}/classes/educationplans`);
  return response.data;
};
