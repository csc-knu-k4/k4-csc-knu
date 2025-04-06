import api from './api';

export interface ContentBlock {
  id: number;
  title: string;
  contentBlockModelType: 0 | 1; // 0 — текст, 1 — картинка
  orderPosition: number;
  materialId: number;
  value: string;
}

export interface Material {
  id: number;
  title: string;
  topicId: number;
  orderPosition: number;
  contentBlocksIds: number[];
  contentBlocks?: ContentBlock[];
}

export const getMaterials = async (): Promise<Material[]> => {
  const response = await api.get('/materials');
  return response.data;
};

export const addMaterial = async (material: Material): Promise<Material> => {
  const response = await api.post('/materials', material);
  return response.data;
};

export const updateMaterial = async (
  id: number,
  material: { title: string; topicId: number; orderPosition: number; contentBlocksIds: number[] },
) => {
  const response = await api.put(`/materials/${id}`, material);
  return response.data;
};

export const deleteMaterial = async (id: number) => {
  await api.delete(`/materials/${id}`);
};

export const getMaterialById = async (id: number) => {
  const response = await api.get(`/materials/${id}`);
  return response.data;
};

export const getMaterialsByTopic = async (topicId: number) => {
  const response = await api.get(`/topics/${topicId}/materials`);
  return response.data;
};

export const getMaterialContentById = async (materialId: number) => {
  const response = await api.get(`/materials/${materialId}/contentblocks`);
  return response.data;
};
