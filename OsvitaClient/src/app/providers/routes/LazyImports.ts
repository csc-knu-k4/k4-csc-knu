import { lazy } from 'react';

export const BaseLayout = lazy(() => import('@/app/layouts/BaseLayout/BaseLayout'));
export const CourseLayout = lazy(() => import('@/app/layouts/CourseLayout/CourseLayout'));
export const AddMaterial = lazy(() => import('@/pages/admin/addMaterial/ui/AddMaterial'));
export const AddChapter = lazy(() => import('@/pages/admin/addSection/ui/AddChapter'));
export const AddSubject = lazy(() => import('@/pages/admin/addSubject/ui/AddSubject'));
export const AddTopic = lazy(() => import('@/pages/admin/addTopic/ui/AddTopic'));
export const Materials = lazy(() => import('@/pages/admin/materials/ui/Materials'));
export const Chapters = lazy(() => import('@/pages/admin/sections/ui/Chapters'));
export const Subjects = lazy(() => import('@/pages/admin/subjects/ui/Subjects'));
export const Topics = lazy(() => import('@/pages/admin/topics/ui/Topics'));
export const Index = lazy(() => import('@/pages/index/ui/Index'));
export const Login = lazy(() => import('@/pages/login/ui/Login'));
export const Register = lazy(() => import('@/pages/register/ui/Register'));
export const SelectSubject = lazy(() => import('@/pages/admin/selectSubject/ui/SelectSubject'));
export const AddTests = lazy(() => import('@/pages/admin/addTests/ui/AddTests'));
export const Tests = lazy(() => import('@/pages/admin/testsPage/ui/Tests'));
export const DiagnosticTest = lazy(
  () => import('@/pages/courses/diagnosticTest/ui/DiagnosticTest'),
);
export const SubjectTaskList = lazy(
  () => import('@/pages/courses/subjectTaskList/ui/SubjectTaskList'),
);
export const SubjectTaskMaterial = lazy(
  () => import('@/pages/courses/subjectTaskMaterial/ui/SubjectTaskMaterial'),
);
export const SubjectTaskTest = lazy(
  () => import('@/pages/courses/subjectTaskTest/ui/SubjectTaskTest'),
);
