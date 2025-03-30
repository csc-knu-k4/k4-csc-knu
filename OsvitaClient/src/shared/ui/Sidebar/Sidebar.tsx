import { Box, Flex, IconButton } from '@chakra-ui/react';
import { SidebarButton } from './SidebarButton';
import { sidebarItems } from './sidebarItems';
import { IoMdClose } from 'react-icons/io';
import { SiteLogo } from '@/shared/ui/SiteLogo';

interface SidebarProps {
  onClose?: () => void;
}

export function Sidebar({ onClose }: SidebarProps) {
  return (
    <Box
      h="calc(100vh - 2.5rem)"
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
        <SiteLogo />
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

      <Flex flexDir="column" borderRadius="1rem" h="full" mt="4.5rem">
        {sidebarItems.map(({ label, icon, path }) => (
          <SidebarButton key={path} icon={icon} label={label} path={path} />
        ))}
      </Flex>
    </Box>
  );
}
