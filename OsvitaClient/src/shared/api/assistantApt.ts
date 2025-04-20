import api from './api';

export type ResponseText = {
  userId: number;
  creationDate: Date;
  requestText: string;
  responseText: string;
};

export type RequestText = {
  userId: number;
  requestText: string;
};

export const sendAssistantMessage = async (requestText: RequestText): Promise<ResponseText> => {
  const response = await api.post(`/assistant/definition`, requestText);
  return response.data;
};
