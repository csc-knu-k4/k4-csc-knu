import { Button, Flex, VStack, Text, Menu, Portal } from '@chakra-ui/react';
import { LuPlus } from 'react-icons/lu';
import { HiDotsVertical } from 'react-icons/hi';
import { UserAvatar } from '@/shared/ui/Avatar';

const ClassStudents = () => {
  return (
    <>
      <Button borderRadius="1rem" colorPalette="orange">
        <LuPlus /> Додати учня
      </Button>
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
