import api from './api';

export type RecomendationMessage = {
  id: number;
  recomendationText: string;
  isRead: boolean;
  isSent: boolean;
  creationDate: Date;
  userId: number;
};

export const getRecomendationMessages = async (userId: number): Promise<RecomendationMessage[]> => {
  const response = await api.get(`users/${userId}/recomendationmessages`);
  return response.data;
};

export const updateRecomendationMessage = async (
  userId: number,
  messageId: string,
  messageData: RecomendationMessage,
) => {
  const response = await api.put(`/users/${userId}/recomendationmessages/${messageId}`, {
    ...messageData,
    isRead: true,
  });
  return response.data;
};

export const deleteRecomendationMessage = async (userId: number, messageId: string) => {
  await api.delete(`/users/${userId}/recomendationmessages/${messageId}`);
};
