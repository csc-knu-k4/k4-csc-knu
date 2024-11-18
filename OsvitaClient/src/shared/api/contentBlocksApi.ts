import api from './api';

export const getContentBlocks = async () => {
  const response = await api.get('/contentblocks');
  return response.data;
};

export const addContentBlock = async (data: {
  id: number;
  title: string;
  contentBlockModelType: number;
  orderPosition: number;
  materialId: number;
  value: string;
}) => {
  const response = await api.post('/contentblocks', data);
  return response.data;
};
