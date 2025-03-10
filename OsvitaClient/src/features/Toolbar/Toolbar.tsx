import { Box, Flex, HStack, IconButton } from '@chakra-ui/react';
import { SearchInput } from '@/shared/ui/SearchInput';
import { UserAvatar } from '@/shared/ui/Avatar';
import { GiHamburgerMenu } from 'react-icons/gi';

interface ToolbarProps {
  onMenuToggle?: () => void;
}

export function Toolbar({ onMenuToggle }: ToolbarProps) {
  return (
    <Box bg="white" p={4} borderRadius="1rem" w="full">
      <Flex justifyContent="space-between" alignItems="center">
        <HStack gap="10">
          <IconButton
            aria-label="Toggle Sidebar"
            display={{ base: 'flex', md: 'none' }}
            onClick={onMenuToggle}
            colorPalette="orange"
            variant="outline"
          >
            <GiHamburgerMenu />
          </IconButton>
          <SearchInput />
        </HStack>
        <UserAvatar />
      </Flex>
    </Box>
  );
}
