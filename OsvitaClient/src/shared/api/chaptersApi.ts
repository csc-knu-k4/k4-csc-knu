import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

export const getChapters = async () => {
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
