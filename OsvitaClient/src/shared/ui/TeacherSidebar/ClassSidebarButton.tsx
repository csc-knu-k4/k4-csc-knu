import { Avatar } from '@/components/ui/avatar';
import { Flex, Text } from '@chakra-ui/react';
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
      <Flex
        align="center"
        gap={3}
        h="3.5rem"
        px={4}
        cursor="pointer"
        bg={isActive ? 'orange.200' : 'white'}
        color={isActive ? 'orange.800' : 'black'}
        _hover={{ bg: 'orange.100' }}
        borderRight={isActive ? '3px solid' : 'none'}
        borderRightColor={isActive ? 'orange' : 'none'}
      >
        <Avatar name={label} src={image} />
        <Text fontSize="lg" fontWeight="medium">
          {label}
        </Text>
      </Flex>
    </Link>
  );
}
