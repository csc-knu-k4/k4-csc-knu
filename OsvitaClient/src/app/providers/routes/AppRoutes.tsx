import BaseLayout from '@/app/layouts/BaseLayout/BaseLayout';
import AddMaterial from '@/pages/admin/addMaterial/ui/AddMaterial';
import AddChapter from '@/pages/admin/addSection/ui/AddChapter';
import AddSubject from '@/pages/admin/addSubject/ui/AddSubject';
import AddTopic from '@/pages/admin/addTopic/ui/AddTopic';
import Materials from '@/pages/admin/materials/ui/Materials';
import Chapters from '@/pages/admin/sections/ui/Chapters';
import Subjects from '@/pages/admin/subjects/ui/Subjects';
import Topics from '@/pages/admin/topics/ui/Topics';
import Index from '@/pages/index/ui/Index';

import { createBrowserRouter, createRoutesFromElements, Route } from 'react-router-dom';

const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Index />} />

      <Route path="/admin" element={<BaseLayout />}>
        <Route path="subjects" element={<Subjects />} />
        <Route path="chapters" element={<Chapters />} />
        <Route path="topics" element={<Topics />} />
        <Route path="materials" element={<Materials />} />
        <Route path="add-subject" element={<AddSubject />} />
        <Route path="add-chapter" element={<AddChapter />} />
        <Route path="add-topic" element={<AddTopic />} />
        <Route path="add-material" element={<AddMaterial />} />
      </Route>
    </>,
  ),
);

export default router;
