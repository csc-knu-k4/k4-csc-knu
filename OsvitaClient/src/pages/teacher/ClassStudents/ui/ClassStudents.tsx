import {
  Button,
  Flex,
  VStack,
  Text,
  Menu,
  Portal,
  Popover,
  IconButton,
  Input,
} from '@chakra-ui/react';
import { LuPlus } from 'react-icons/lu';
import { HiDotsVertical } from 'react-icons/hi';
import { UserAvatar } from '@/shared/ui/Avatar';
import { IoPaperPlane } from 'react-icons/io5';

const ClassStudents = () => {
  return (
    <>
      <Popover.Root>
        <Popover.Trigger asChild>
          <Button borderRadius="1rem" colorPalette="orange">
            <LuPlus /> Додати учня
          </Button>
        </Popover.Trigger>
        <Portal>
          <Popover.Positioner>
            <Popover.Content css={{ '--popover-bg': 'white' }}>
              <Popover.Arrow />
              <Popover.Body>
                <Popover.Title fontWeight="medium" color="black" fontSize="md">
                  Запросити учня
                </Popover.Title>
                <Flex flexDir="row" justifyContent="center" alignItems="center" mt={4}>
                  <Input
                    colorPalette="orange"
                    borderRadius="1rem"
                    placeholder="Введіть електронну пошту"
                    size="md"
                    color="black"
                  />
                  <IconButton borderRadius="1rem" aria-label="Invite user" variant="ghost">
                    <IoPaperPlane color="#f97316" size={28} />
                  </IconButton>
                </Flex>
              </Popover.Body>
            </Popover.Content>
          </Popover.Positioner>
        </Portal>
      </Popover.Root>
      <VStack gap={4} mt={6}>
        <Flex
          borderRadius="1rem"
          w="full"
          boxShadow="sm"
          p="1rem"
          flexDir="row"
          justifyContent="space-between"
          alignItems="center"
        >
          <Flex gap={4} flexDir="row" justifyContent="space-between" alignItems="center">
            <UserAvatar />
            <Text fontSize="md">Петренко Олександр</Text>
          </Flex>
          <Menu.Root>
            <Menu.Trigger asChild>
              <Button maxW="3rem" variant="plain">
                <HiDotsVertical size="42px" color="rgb(234, 88, 12)" />
              </Button>
            </Menu.Trigger>
            <Portal>
              <Menu.Positioner>
                <Menu.Content>
                  <Menu.Item value="edit">Редагувати</Menu.Item>
                  <Menu.Item value="delete">Видалити</Menu.Item>
                </Menu.Content>
              </Menu.Positioner>
            </Portal>
          </Menu.Root>
        </Flex>
      </VStack>
    </>
  );
};

export default ClassStudents;
