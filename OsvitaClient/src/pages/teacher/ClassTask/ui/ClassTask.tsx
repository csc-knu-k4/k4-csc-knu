import { Button, Flex, VStack, Text, Menu } from '@chakra-ui/react';
import { BsFileEarmarkCheckFill } from 'react-icons/bs';
import { LuPlus } from 'react-icons/lu';
import { HiDotsVertical } from 'react-icons/hi';

const ClassTask = () => {
  return (
    <>
      <Button borderRadius="1rem" colorPalette="orange">
        <LuPlus /> Призначити завдання
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
            <BsFileEarmarkCheckFill size="32px" color="rgb(234, 88, 12)" />
            <Text fontSize="md">Тест 1. Властивості дій з дійсними числами</Text>
          </Flex>
          <Menu.Root>
            <Menu.Trigger asChild>
              <Button variant="plain">
                <HiDotsVertical size="42px" color="rgb(234, 88, 12)" />
              </Button>
            </Menu.Trigger>
            <Menu.Positioner>
              <Menu.Content>
                <Menu.Item value="edit">Редагувати</Menu.Item>
                <Menu.Item value="delete">Видалити</Menu.Item>
              </Menu.Content>
            </Menu.Positioner>
          </Menu.Root>
        </Flex>
      </VStack>
    </>
  );
};

export default ClassTask;
