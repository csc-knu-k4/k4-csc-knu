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
        bgColor={isActive ? 'blue.300' : 'white'}
        color={isActive ? 'blue' : 'black'}
        borderRight={isActive ? '3px solid' : 'none'}
        borderRightColor={isActive ? 'blue' : 'none'}
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
