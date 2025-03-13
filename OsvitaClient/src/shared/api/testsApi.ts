import api from './api';

export interface Assignment {
  id: number;
  objectId: number;
  problemDescription: string;
  explanation: string;
  assignmentModelType: number;
  parentAssignmentId: number;
  answers: {
    id: number;
    value: string;
    isCorrect: boolean;
    points: number;
    assignmentId: number;
  }[];
  childAssignments: Assignment[];
}

export const getAssignments = async (): Promise<Assignment[]> => {
  const response = await api.get('/assignments');
  return response.data;
};

export const addAssignments = async (test: Assignment) => {
  const response = await api.post('/assignments', test);
  return response.data;
};

export const updateAssignment = async (id: number, test: Assignment) => {
  const response = await api.put(`/assignments/${id}`, test);
  return response.data;
};

export const deleteAssignment = async (id: number) => {
  await api.delete(`/assignments/${id}`);
};

export const getAssignmentById = async (id: number) => {
  const response = await api.get(`/assignments/${id}`);
  return response.data;
};
