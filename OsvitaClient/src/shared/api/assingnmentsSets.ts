import api from './api';

export const getAssignmentsSets = async (id: number) => {
  const response = await api.get(`/assignments/sets/${id}`);
  return response.data;
};

export const addAssignmentsSets = async (assignment: any) => {
  const response = await api.post('/assignments/sets', assignment);
  return response.data;
};
