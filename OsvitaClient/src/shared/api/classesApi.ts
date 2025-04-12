import api from './api';

export type Class = {
  id: number;
  name: string;
  teacherId: number;
  educationClassPlanId: number;
  studentsIds: number[];
};

export const getClasses = async (): Promise<Class[]> => {
  const response = await api.get('/classes');
  return response.data;
};

export const addClasses = async (classData: Class) => {
  const response = await api.post('/classes', classData);
  return response.data;
};

export const updateClass = async (id: number, classData: Class) => {
  const response = await api.put(`/classes/${id}`, classData);
  return response.data;
};

export const deleteClass = async (id: number) => {
  await api.delete(`/classes/${id}`);
};

export const getClassById = async (id: number) => {
  const response = await api.get(`/classes/${id}`);
  return response.data;
};
