import api from './api';

export type Class = {
  id: number;
  name: string;
  teacherId: number;
  educationClassPlanId: number;
  studentsIds: number[];
};

export type EducationPlan = {
  id: number;
  educationPlanId: number;
  topicId: number;
  educationClassPlanId: number;
};

export type EducationPlanAssignment = {
  id: number;
  assignmentSetId: number;
  educationClassPlanId: number;
};

export const getClasses = async (teacherId?: number): Promise<Class[]> => {
  const response = await api.get('/classes', {
    params: teacherId ? { teacherId } : {},
  });
  return response.data;
};

export const addClasses = async (classData: Class) => {
  const response = await api.post('/classes', classData);
  return response.data;
};

export const updateClass = async (id: number, classData: Class) => {
  const response = await api.put(`/classes/${id}`, classData);
  return response.data;
};

export const deleteClass = async (id: number) => {
  await api.delete(`/classes/${id}`);
};

export const getClassById = async (id: number) => {
  const response = await api.get(`/classes/${id}`);
  return response.data;
};

export const addClassesEducationPlanTopics = async (
  educationPlan: EducationPlan,
  classid: number,
) => {
  const response = await api.post(`/classes/${classid}/educationplan/topics`, educationPlan);
  return response.data;
};

export const addClassesEducationPlanAssignments = async (
  educationPlan: EducationPlanAssignment,
  classid: number,
) => {
  const response = await api.post(`/classes/${classid}/educationplan/assignments`, educationPlan);
  return response.data;
};

export const deleteClassesEducationPlanTopicsById = async (topicId: number, classId: number) => {
  const response = await api.delete(`/classes/${classId}/educationplan/topics/${topicId}`);
  return response.data;
};

export const getClassesEducationPlan = async (classid: number) => {
  const response = await api.get(`/classes/${classid}/educationplan`);
  return response.data;
};

export const getClassesEducationPlanVm = async (classid: number) => {
  const response = await api.get(`/classes/${classid}/educationplanvm`);
  return response.data;
};

export const addStudentToClass = async (studentEmail: string, classid: number) => {
  const response = await api.post(`/classes/${classid}/students`, { email: studentEmail });
  return response.data;
};

export const getClassesStatisticAssignmentsByAssingmentSetId = async (
  classid: number,
  assignmentsSetId: number,
) => {
  const response = await api.get(`/classes/${classid}/statistic/assignments/${assignmentsSetId}`);
  return response.data;
};
