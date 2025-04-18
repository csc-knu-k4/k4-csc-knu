import { Box, Flex, HStack, IconButton, Menu, Portal, Button } from '@chakra-ui/react';
import { UserAvatar } from '@/shared/ui/Avatar';
import { GiHamburgerMenu } from 'react-icons/gi';
import { useNavigate } from 'react-router-dom';
import { SiteLogo } from '@/shared/ui/SiteLogo';
import { MessagesDrawer } from './MessagesDrawer';
import { useState } from 'react';
import { FaRegBell } from 'react-icons/fa';

interface ToolbarProps {
  onMenuToggle?: () => void;
}

export function Toolbar({ onMenuToggle }: ToolbarProps) {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.clear();
    navigate('/');
  };
  const [open, setOpen] = useState(false);
  const roles: string[] = JSON.parse(localStorage.getItem('userRoles') || '[]');
  const isTeacher = roles.includes('teacher');
  const isStudent = roles.includes('student');
  const isAdmin = roles.includes('admin');

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
        <IconButton
          aria-label="Notifications"
          variant="ghost"
          onClick={() => setOpen(true)}
          colorPalette="orange"
          ml="auto"
          mr={2}
        >
          <FaRegBell />
        </IconButton>
        <Menu.Root>
          <Menu.Trigger asChild>
            <Button maxW="3rem" variant="plain">
              <UserAvatar />
            </Button>
          </Menu.Trigger>
          <Portal>
            <Menu.Positioner>
              <Menu.Content>
                <Menu.Item value="profile" onClick={() => navigate('/course/profile')}>
                  👤️ Профіль
                </Menu.Item>
                {isStudent && (
                  <Menu.Item
                    value="education-plan"
                    onClick={() => navigate('/course/student-education-plan')}
                  >
                    📝 Навчальний план
                  </Menu.Item>
                )}
                {isTeacher && (
                  <Menu.Item value="teacher" onClick={() => navigate('/teacher/class-task')}>
                    👨‍🏫 Викладач
                  </Menu.Item>
                )}
                {isAdmin && (
                  <Menu.Item value="admin" onClick={() => navigate('/admin/subjects')}>
                    🛠 Адмін-панель
                  </Menu.Item>
                )}
                <Menu.Item value="course" onClick={() => navigate('/course')}>
                  📚 Курси
                </Menu.Item>
                <Menu.Item value="logout" onClick={handleLogout}>
                  🚪 Вийти
                </Menu.Item>
              </Menu.Content>
            </Menu.Positioner>
          </Portal>
        </Menu.Root>
      </Flex>
      <MessagesDrawer open={open} onClose={() => setOpen(false)} />
    </Box>
  );
}
