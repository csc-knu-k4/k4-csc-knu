import { AppDispatch } from '@/processes/store';
import api from './api';
import { setAuthData } from '@/processes/store/authSlice';

export const login = async (data: { email: string; password: string }, dispatch: AppDispatch) => {
  const response = await api.post('/account/Login', data);
  const { token, id } = response.data;

  dispatch(setAuthData({ token, userId: id }));

  return response.data;
};

export const register = async (data: {
  email: string;
  password: string;
  firstName: string;
  secondName: string;
  roles: string[];
}) => {
  const response = await api.post('/account/Register', data);
  return response.data;
};
