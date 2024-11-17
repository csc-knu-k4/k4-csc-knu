import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

export const getTopics = async () => {
  const response = await api.get('/topics');
  return response.data;
};

export const addTopic = async (topic: {
  title: string;
  chapterId: number;
  orderPosition: number;
  materialsIds: number[];
}) => {
  const response = await api.post('/topics', topic);
  return response.data;
};

export const updateTopic = async (id: number, topic: any) => {
  const response = await api.put(`/topics/${id}`, topic);
  return response.data;
};

export const deleteTopic = async (id: number) => {
  await api.delete(`/topics/${id}`);
};

export const getTopicById = async (id: number) => {
  const response = await api.get(`/topics/${id}`);
  return response.data;
};

export const getTopicsByChapter = async (chapterId: number) => {
  const response = await api.get(`/chapters/${chapterId}/topics`);
  return response.data;
};
