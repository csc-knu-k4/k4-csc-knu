import { createBrowserRouter, createRoutesFromElements, Route } from 'react-router-dom';
import ProtectedRoute from './ProtectedRoute';
import Index from '@/pages/index/ui/Index';
import {
  AddMaterial,
  AddChapter,
  AddSubject,
  AddTopic,
  Materials,
  Chapters,
  Subjects,
  Topics,
  Login,
  Register,
  BaseLayout,
  SelectSubject,
  SubjectTaskList,
  SubjectTaskMaterial,
  SubjectTaskTest,
  CourseLayout,
  AddTests,
  Tests,
  DiagnosticTest,
  TeacherLayout,
} from './LazyImports';
import LazyElement from './LazyElement';

const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Index />} />
      <Route path="/login" element={LazyElement(Login)} />
      <Route path="/register" element={LazyElement(Register)} />
      <Route element={<ProtectedRoute />}>
        <Route path="/admin" element={LazyElement(BaseLayout)}>
          <Route path="subjects" element={LazyElement(Subjects)} />
          <Route path="chapters" element={LazyElement(Chapters)} />
          <Route path="topics" element={LazyElement(Topics)} />
          <Route path="materials" element={LazyElement(Materials)} />
          <Route path="tests" element={LazyElement(Tests)} />
          <Route path="add-subject" element={LazyElement(AddSubject)} />
          <Route path="add-chapter" element={LazyElement(AddChapter)} />
          <Route path="add-topic" element={LazyElement(AddTopic)} />
          <Route path="add-material" element={LazyElement(AddMaterial)} />
          <Route path="add-tests" element={LazyElement(AddTests)} />
        </Route>
        <Route path="/course" element={LazyElement(CourseLayout)}>
          <Route index element={LazyElement(SelectSubject)} />
          <Route path="select-course-list/:subjectId" element={LazyElement(SubjectTaskList)} />
          <Route path="subject-material/:materialId" element={LazyElement(SubjectTaskMaterial)} />
          <Route path="subject-test/:testId" element={LazyElement(SubjectTaskTest)} />
          <Route path="diagnostic-test/:testId" element={LazyElement(DiagnosticTest)} />
        </Route>
        <Route path="/teacher" element={LazyElement(TeacherLayout)}>
          <Route index element={LazyElement(SelectSubject)} />
        </Route>
      </Route>
    </>,
  ),
);

export default router;
