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
