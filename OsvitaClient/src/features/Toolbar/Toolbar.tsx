import { Box, Flex, HStack, IconButton, Menu, Portal, Button } from '@chakra-ui/react';
import { SearchInput } from '@/shared/ui/SearchInput';
import { UserAvatar } from '@/shared/ui/Avatar';
import { GiHamburgerMenu } from 'react-icons/gi';
import { useLocation, useNavigate } from 'react-router-dom';

interface ToolbarProps {
  onMenuToggle?: () => void;
}

export function Toolbar({ onMenuToggle }: ToolbarProps) {
  const location = useLocation();
  const navigate = useNavigate();

  const isAdmin = location.pathname.startsWith('/admin');
  const isCourse = location.pathname.startsWith('/course');

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

        <Menu.Root>
          <Menu.Trigger asChild>
            <Button maxW="3rem" variant="plain">
              <UserAvatar />
            </Button>
          </Menu.Trigger>
          <Portal>
            <Menu.Positioner>
              <Menu.Content>
                <Menu.Item value="admin" onClick={() => navigate('/admin')} disabled={isAdmin}>
                  🛠 Адмін-панель
                </Menu.Item>
                <Menu.Item value="course" onClick={() => navigate('/course')} disabled={isCourse}>
                  📚 Курси
                </Menu.Item>
              </Menu.Content>
            </Menu.Positioner>
          </Portal>
        </Menu.Root>
      </Flex>
    </Box>
  );
}
