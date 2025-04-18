import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AuthState {
  token: string | null;
  userId: number | null;
  roles: string[];
}

const initialState: AuthState = {
  token: localStorage.getItem('authToken'),
  userId: Number(localStorage.getItem('userId')) || null,
  roles: JSON.parse(localStorage.getItem('userRoles') || '[]'),
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
      state.roles = [];

      localStorage.removeItem('authToken');
      localStorage.removeItem('userId');
      localStorage.removeItem('userRoles');
    },
    setUserRoles(state, action: PayloadAction<string[]>) {
      state.roles = action.payload;
      localStorage.setItem('userRoles', JSON.stringify(action.payload));
    },
  },
});

export const { setAuthData, clearAuthData, setUserRoles } = authSlice.actions;
export default authSlice.reducer;
