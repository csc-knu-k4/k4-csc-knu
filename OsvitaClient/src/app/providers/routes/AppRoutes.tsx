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
          <Route path="add-subject" element={LazyElement(AddSubject)} />
          <Route path="add-chapter" element={LazyElement(AddChapter)} />
          <Route path="add-topic" element={LazyElement(AddTopic)} />
          <Route path="add-material" element={LazyElement(AddMaterial)} />
        </Route>
      </Route>
    </>,
  ),
);

export default router;
