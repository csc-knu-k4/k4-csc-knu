import { Box, Flex, IconButton, Button, Spinner, Input, Dialog, Portal } from '@chakra-ui/react';
import { IoMdClose } from 'react-icons/io';
import { ClassSidebarButton } from './ClassSidebarButton';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { getClasses, addClasses } from '@/shared/api/classesApi';
import { useState } from 'react';
import { toaster } from '@/components/ui/toaster';
import { CloseButton } from '@/components/ui/close-button';

interface SidebarProps {
  onClose?: () => void;
}

export function TeacherSidebar({ onClose }: SidebarProps) {
  const queryClient = useQueryClient();
  const [newClassName, setNewClassName] = useState('');

  const { data: classes, isLoading } = useQuery(['classes'], getClasses);

  const createClassMutation = useMutation({
    mutationFn: () => {
      const userId = Number(localStorage.getItem('userId'));
      if (!userId) throw new Error('User ID не знайдено');

      return addClasses({
        id: 0,
        name: newClassName || `Новий клас ${Date.now()}`,
        teacherId: userId,
        educationClassPlanId: 0,
        studentsIds: [],
      });
    },
    onSuccess: () => {
      queryClient.invalidateQueries(['classes']);
      toaster.success({ title: 'Клас створено' });
      setNewClassName('');
    },
    onError: () => {
      toaster.error({ title: 'Не вдалося створити клас' });
    },
  });

  const handleCreateClass = () => {
    if (!newClassName.trim()) {
      toaster.error({ title: 'Введіть назву класу' });
      return;
    }
    createClassMutation.mutate();
  };

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

      <Flex flexDir="column" borderRadius="1rem" h="full" gap={3}>
        {/* Список класів */}
        <Box mt={2} overflowY="auto" maxH="calc(100vh - 300px)">
          {isLoading ? (
            <Spinner />
          ) : (
            classes?.map((cls) => (
              <ClassSidebarButton
                key={cls.id}
                label={cls.name}
                path={`/teacher/${cls.id}/class-task`}
              />
            ))
          )}
        </Box>
        <Dialog.Root>
          <Dialog.Trigger asChild>
            <Button fontSize="md" aria-label="Add class" colorScheme="orange" variant="outline">
              + Створити клас
            </Button>
          </Dialog.Trigger>
          <Portal>
            <Dialog.Backdrop />
            <Dialog.Positioner>
              <Dialog.Content>
                <Dialog.Header>
                  <Dialog.Title>Створити новий клас</Dialog.Title>
                  <Dialog.CloseTrigger asChild>
                    <CloseButton size="sm" />
                  </Dialog.CloseTrigger>
                </Dialog.Header>
                <Dialog.Body>
                  <Input
                    placeholder="Назва класу"
                    value={newClassName}
                    onChange={(e) => setNewClassName(e.target.value)}
                  />
                </Dialog.Body>
                <Dialog.Footer>
                  <Dialog.ActionTrigger asChild>
                    <Button variant="ghost" mr={3}>
                      Скасувати
                    </Button>
                  </Dialog.ActionTrigger>
                  <Button colorPalette="orange" onClick={handleCreateClass}>
                    Створити
                  </Button>
                </Dialog.Footer>
              </Dialog.Content>
            </Dialog.Positioner>
          </Portal>
        </Dialog.Root>
      </Flex>
    </Box>
  );
}
