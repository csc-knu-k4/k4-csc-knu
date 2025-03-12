import { FaBook, FaFolder, FaTags, FaFile, FaQuestion } from 'react-icons/fa';

export const sidebarItems = [
  { label: 'Предмети', icon: <FaBook />, path: '/admin/subjects' },
  { label: 'Розділи', icon: <FaFolder />, path: '/admin/chapters' },
  { label: 'Теми', icon: <FaTags />, path: '/admin/topics' },
  { label: 'Матеріали', icon: <FaFile />, path: '/admin/materials' },
  { label: 'Тести', icon: <FaQuestion />, path: '/admin/tests' },
];
