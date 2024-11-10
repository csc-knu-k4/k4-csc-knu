import { Button } from '@chakra-ui/react';
import { Link, useLocation } from 'react-router-dom';
import { ReactElement } from 'react';

interface SidebarButtonProps {
  label: string;
  icon: ReactElement;
  path: string;
}

export function SidebarButton({ label, icon, path }: SidebarButtonProps) {
  const location = useLocation();
  const isActive = location.pathname === path;

  return (
    <Link to={path}>
      <Button
        borderRadius="none"
        bgColor={isActive ? '#E4E7FF' : 'white'}
        color={isActive ? '#5C6CFF' : 'black'}
        borderRight={isActive ? '3px solid #5C6CFF' : 'none'}
        justifyContent="flex-start"
        w="full"
        fontSize="xl"
        h="3.5rem"
      >
        {icon}
        {label}
      </Button>
    </Link>
  );
}
