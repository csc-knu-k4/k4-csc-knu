import { Avatar } from '@/components/ui/avatar';
import { Button, Flex } from '@chakra-ui/react';
import { Link, useLocation } from 'react-router-dom';

interface SidebarButtonProps {
  label: string;
  image?: string;
  path: string;
}

export function ClassSidebarButton({ label, image, path }: SidebarButtonProps) {
  const location = useLocation();
  const isActive = location.pathname === path;

  return (
    <Link to={path}>
      <Flex flexDir="row" justifyContent="flex-start" alignItems="center">
        <Avatar name={label} src={image} />
        <Button
          borderRadius="none"
          bgColor={isActive ? 'orange.200' : 'white'}
          color={isActive ? 'orange' : 'black'}
          borderRight={isActive ? '3px solid' : 'none'}
          borderRightColor={isActive ? 'orange' : 'none'}
          justifyContent="flex-start"
          fontSize="xl"
          h="3.5rem"
        >
          {label}
        </Button>
      </Flex>
    </Link>
  );
}
