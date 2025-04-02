import { Box, Flex, HStack, IconButton, Menu, Portal, Button } from '@chakra-ui/react';
import { UserAvatar } from '@/shared/ui/Avatar';
import { GiHamburgerMenu } from 'react-icons/gi';
import { useLocation, useNavigate } from 'react-router-dom';
import { SiteLogo } from '@/shared/ui/SiteLogo';

interface ToolbarProps {
  onMenuToggle?: () => void;
}

export function Toolbar({ onMenuToggle }: ToolbarProps) {
  const location = useLocation();
  const navigate = useNavigate();

  const isAdmin = location.pathname.startsWith('/admin');
  const isCourse = location.pathname.startsWith('/course');

  return (
    <Box bg="white" p={{ base: 2, md: 4 }} borderRadius="1rem" w="full">
      <Flex justifyContent="space-between" alignItems="center">
        <HStack>
          <IconButton
            aria-label="Toggle Sidebar"
            display={{ base: 'flex', md: 'none' }}
            onClick={onMenuToggle}
            colorPalette="orange"
            variant="outline"
          >
            <GiHamburgerMenu />
          </IconButton>
          <SiteLogo />
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
                <Menu.Item value="teacher" onClick={() => navigate('/teacher/class-task')}>
                  👨‍🏫 Викладач
                </Menu.Item>
                <Menu.Item
                  value="admin"
                  onClick={() => navigate('/admin/subjects')}
                  disabled={isAdmin}
                >
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
