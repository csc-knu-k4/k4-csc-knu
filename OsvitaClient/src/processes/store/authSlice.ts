import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AuthState {
  token: string | null;
  userId: number | null;
}

const initialState: AuthState = {
  token: localStorage.getItem('authToken'),
  userId: Number(localStorage.getItem('userId')) || null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setAuthData(state, action: PayloadAction<{ token: string; userId: number }>) {
      state.token = action.payload.token;
      state.userId = action.payload.userId;

      localStorage.setItem('authToken', action.payload.token);
      localStorage.setItem('userId', action.payload.userId.toString());
    },
    clearAuthData(state) {
      state.token = null;
      state.userId = null;

      localStorage.removeItem('authToken');
      localStorage.removeItem('userId');
    },
  },
});

export const { setAuthData, clearAuthData } = authSlice.actions;
export default authSlice.reducer;
