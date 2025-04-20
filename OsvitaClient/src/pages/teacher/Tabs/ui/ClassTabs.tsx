import { Tabs } from '@chakra-ui/react';
import { Link, useLocation, useParams } from 'react-router-dom';
import ClassTask from '../../ClassTask/ui/ClassTask';
import ClassStudents from '../../ClassStudents/ui/ClassStudents';
import ClassMarks from '../../ClassMarks/ui/ClassMarks';

const getTabFromPath = (path: string): string => {
  if (path.includes('/class-task')) return 'ClassTask';
  if (path.includes('/class-students')) return 'ClassStudents';
  if (path.includes('/class-marks')) return 'ClassMarks';
  return 'ClassTask';
};

const ClassTabs = () => {
  const location = useLocation();
  const { classId } = useParams<{ classId: string }>();
  const tabValue = getTabFromPath(location.pathname);

  const basePath = `/teacher/${classId}`;

  return (
    <Tabs.Root value={tabValue}>
      <Tabs.List justifyContent="center" gap={8}>
        <Tabs.Trigger value="ClassTask" asChild>
          <Link to={`${basePath}/class-task`}>Завдання</Link>
        </Tabs.Trigger>
        <Tabs.Trigger value="ClassStudents" asChild>
          <Link to={`${basePath}/class-students`}>Учні</Link>
        </Tabs.Trigger>
        <Tabs.Trigger value="ClassMarks" asChild>
          <Link to={`${basePath}/class-marks`}>Оцінки</Link>
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
