import api from './api';

export interface RatingItem {
  userModel: {
    id: number;
    email: string;
    firstName: string;
    secondName: string;
    statisticModelId: number | null;
    educationClassesIds: number[];
    roles: string[];
  };
  score: number;
  place: number;
}

export const getDailyAssignmentSet = async (userId: number) => {
  const response = await api.get(`/users/${userId}/dailyassignmentset`);
  return response.data;
};

export const getDailyAssignmentStreak = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/dailyassignment/streak`);
  return response.data;
};

export const getDailyAssignmentIsDone = async (userId: number) => {
  const response = await api.get(`/users/${userId}/statistic/dailyassignment/isdone`);
  return response.data;
};

export const getDailyAssignmentRating = async (): Promise<RatingItem[]> => {
  const response = await api.get(`/users/dailyassignment/rating`);
  return response.data;
};
