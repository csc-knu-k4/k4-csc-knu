import { FaBook, FaFolder, FaTags, FaFile } from 'react-icons/fa';

export const sidebarItems = [
  { label: 'Предмети', icon: <FaBook />, path: '/subjects' },
  { label: 'Розділи', icon: <FaFolder />, path: '/sections' },
  { label: 'Теми', icon: <FaTags />, path: '/topics' },
  { label: 'Матеріали', icon: <FaFile />, path: '/materials' },
];
