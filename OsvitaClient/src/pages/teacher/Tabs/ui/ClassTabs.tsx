import { Tabs } from '@chakra-ui/react';
import { Link, useLocation } from 'react-router-dom';
import ClassTask from '../../ClassTask/ui/ClassTask';
import ClassStudents from '../../ClassStudents/ui/ClassStudents';
import ClassMarks from '../../ClassMarks/ui/ClassMarks';

const pathToTab: Record<string, string> = {
  '/teacher/class-task': 'ClassTask',
  '/teacher/class-students': 'ClassStudents',
  '/teacher/class-marks': 'ClassMarks',
};

const ClassTabs = () => {
  const location = useLocation();
  const tabValue = pathToTab[location.pathname] || 'ClassTask';

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
        <ClassMarks />
      </Tabs.Content>
    </Tabs.Root>
  );
};

export default ClassTabs;
