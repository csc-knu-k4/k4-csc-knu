import BaseLayout from '@/app/layouts/BaseLayout/BaseLayout';
import Materials from '@/pages/admin/materials/ui/Materials';
import Sections from '@/pages/admin/sections/ui/Sections';
import Subjects from '@/pages/admin/subjects/ui/Subjects';
import Topics from '@/pages/admin/topics/ui/Topics';

import { createBrowserRouter, createRoutesFromElements, Route } from 'react-router-dom';

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<BaseLayout />}>
      <Route path="subjects" element={<Subjects />} />
      <Route path="sections" element={<Sections />} />
      <Route path="topics" element={<Topics />} />
      <Route path="materials" element={<Materials />} />
    </Route>,
  ),
);

export default router;
