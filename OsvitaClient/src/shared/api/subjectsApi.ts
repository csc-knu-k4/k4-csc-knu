import api from './api';

export interface Subject {
  id: number;
  title: string;
  chaptersIds: number[];
}

export const getSubjects = async (): Promise<Subject[]> => {
  const response = await api.get('/subjects');
  return response.data;
};

export const addSubject = async (subject: { title: string; chaptersIds: number[] }) => {
  const response = await api.post('/subjects', subject);
  return response.data;
};

export const updateSubject = async (
  id: number,
  subject: { title: string; chaptersIds: number[] },
) => {
  const response = await api.put(`/subjects/${id}`, subject);
  return response.data;
};

export const deleteSubject = async (id: number) => {
  await api.delete(`/subjects/${id}`);
};

export const getSubjectById = async (id: number) => {
  const response = await api.get(`/subjects/${id}`);
  return response.data;
};
