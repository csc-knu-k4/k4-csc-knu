import { Tabs } from '@chakra-ui/react';
import ClassTask from '../../ClassTask/ui/ClassTask';
import ClassStudents from '../../ClassStudents/ui/ClassStudents';

const ClassTabs = () => {
  return (
    <Tabs.Root defaultValue="ClassTask">
      <Tabs.List justifyContent="center" gap={8}>
        <Tabs.Trigger value="ClassTask" fontSize="md" colorPalette="orange">
          Завдання
        </Tabs.Trigger>
        <Tabs.Trigger value="ClassStudents" fontSize="md" colorPalette="orange">
          Учні
        </Tabs.Trigger>
        <Tabs.Trigger value="ClassMarks" fontSize="md" colorPalette="orange">
          Оцінки
        </Tabs.Trigger>
      </Tabs.List>
      <Tabs.Content value="ClassTask">
        <ClassTask />
      </Tabs.Content>
      <Tabs.Content value="ClassStudents">
        <ClassStudents />
      </Tabs.Content>
      <Tabs.Content value="ClassMarks">Class Marks</Tabs.Content>
    </Tabs.Root>
  );
};

export default ClassTabs;
