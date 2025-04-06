import { Tabs } from '@chakra-ui/react';
import { Link, Route, Routes, useLocation } from 'react-router-dom';
import ClassTask from '../../ClassTask/ui/ClassTask';
import ClassStudents from '../../ClassStudents/ui/ClassStudents';
import ClassMarks from '../../ClassMarks/ui/ClassMarks';
import { ClassMarksDetails } from '@/app/providers/routes/LazyImports';

const getTabFromPath = (path: string): string => {
  if (path.startsWith('/teacher/class-task')) return 'ClassTask';
  if (path.startsWith('/teacher/class-students')) return 'ClassStudents';
  if (path.startsWith('/teacher/class-marks')) return 'ClassMarks';
  return 'ClassTask';
};

const ClassTabs = () => {
  const location = useLocation();
  const tabValue = getTabFromPath(location.pathname);

  return (
    <Tabs.Root value={tabValue}>
      <Tabs.List justifyContent="center" gap={8}>
        <Tabs.Trigger value="ClassTask" asChild>
          <Link to="/teacher/class-task">Завдання</Link>
        </Tabs.Trigger>
        <Tabs.Trigger value="ClassStudents" asChild>
          <Link to="/teacher/class-students">Учні</Link>
        </Tabs.Trigger>
        <Tabs.Trigger value="ClassMarks" asChild>
          <Link to="/teacher/class-marks">Оцінки</Link>
        </Tabs.Trigger>
      </Tabs.List>

      <Tabs.Content value="ClassTask">
        <ClassTask />
      </Tabs.Content>
      <Tabs.Content value="ClassStudents">
        <ClassStudents />
      </Tabs.Content>
      <Tabs.Content value="ClassMarks">
        <Routes>
          <Route index element={<ClassMarks />} />
          <Route path="details/:testId" element={<ClassMarksDetails />} />
        </Routes>
      </Tabs.Content>
    </Tabs.Root>
  );
};

export default ClassTabs;
