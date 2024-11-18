import api from './api';

export interface Chapter {
  id: number;
  title: string;
  subjectId: number;
  orderPosition: number;
  topicIds: number[];
}

export const getChapters = async (): Promise<Chapter[]> => {
  const response = await api.get('/chapters');
  return response.data;
};

export const addChapter = async (chapter: {
  title: string;
  subjectId: number;
  orderPosition: number;
  topicsIds: number[];
}) => {
  const response = await api.post('/chapters', chapter);
  return response.data;
};

export const updateChapter = async (id: number, chapter: any) => {
  const response = await api.put(`/chapters/${id}`, chapter);
  return response.data;
};

export const deleteChapter = async (id: number) => {
  await api.delete(`/chapters/${id}`);
};

export const getChapterById = async (id: number) => {
  const response = await api.get(`/chapters/${id}`);
  return response.data;
};

export const getChaptersBySubject = async (subjectId: number) => {
  const response = await api.get(`/subjects/${subjectId}/chapters`);
  return response.data;
};
