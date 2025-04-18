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
  ClassTabs,
  Profile,
  ClassTaskCreate,
  ClassMarksDetails,
  StudentEducationPlan,
  StudentTestPage,
  SubjectTaskTopic,
  DailyAssignmentPage,
} from './LazyImports';
import LazyElement from './LazyElement';
import RoleProtectedRoute from './RoleProtectedRoute';

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
          <Route path="profile" element={LazyElement(Profile)} />
          <Route element={<RoleProtectedRoute allowedRoles={['student']} />}>
            <Route
              path="/course/student-education-plan"
              element={LazyElement(StudentEducationPlan)}
            />
          </Route>
          <Route path="student/test/:assignmentSetId" element={LazyElement(StudentTestPage)} />
          <Route path="subject-topic/:topicId" element={LazyElement(SubjectTaskTopic)} />
          <Route path="daily-assignment/:id" element={<DailyAssignmentPage />} />
        </Route>
        <Route element={<RoleProtectedRoute allowedRoles={['teacher']} />}>
          <Route path="/teacher/:classId" element={LazyElement(TeacherLayout)}>
            <Route path="class-task" element={LazyElement(ClassTabs)} />
            <Route path="class-task/create" element={LazyElement(ClassTaskCreate)} />
            <Route path="class-marks" element={LazyElement(ClassTabs)} />
            <Route path="class-students" element={LazyElement(ClassTabs)} />
            <Route path="class-marks/details/:testId" element={LazyElement(ClassMarksDetails)} />
          </Route>
        </Route>
      </Route>
    </>,
  ),
);

export default router;
