import { Box, Flex, IconButton } from '@chakra-ui/react';
import { IoMdClose } from 'react-icons/io';
import { sidebarItems } from './sidebarItems';
import { ClassSidebarButton } from './ClassSidebarButton';

interface SidebarProps {
  onClose?: () => void;
}

export function TeacherSidebar({ onClose }: SidebarProps) {
  return (
    <Box
      h="calc(100vh - 8.25rem)"
      zIndex="9999"
      bg="white"
      borderRadius="1rem"
      position={{ base: 'fixed', md: 'relative' }}
      left={{ base: 0, md: 'auto' }}
      top={0}
      w="16rem"
      p={4}
      shadow={{ base: 'lg', md: 'none' }}
    >
      <Flex justifyContent="space-between" alignItems="center" mb="1rem">
        {onClose && (
          <IconButton
            aria-label="Close Sidebar"
            display={{ base: 'flex', md: 'none' }}
            onClick={onClose}
            size="sm"
            colorPalette="orange"
            variant="outline"
          >
            <IoMdClose />
          </IconButton>
        )}
      </Flex>

      <Flex flexDir="column" borderRadius="1rem" h="full">
        {sidebarItems.map(({ label, image, path }) => (
          <ClassSidebarButton key={path} image={image} label={label} path={path} />
        ))}
      </Flex>
    </Box>
  );
}
