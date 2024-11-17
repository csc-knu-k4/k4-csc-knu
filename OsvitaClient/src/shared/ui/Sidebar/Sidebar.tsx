import { Box, Flex } from '@chakra-ui/react';
import { SidebarButton } from './SidebarButton';
import { sidebarItems } from './sidebarItems';
import HatIcon from '@/shared/assets/HatIcon';

export function Sidebar() {
  return (
    <Box h="calc(100vh - 2.5rem)" bg="white" borderRadius="1rem">
      <Flex justifyContent="center" alignItems="center" mb="1rem">
        <Flex
          mt={4}
          gap={2}
          justifyContent="center"
          alignItems="center"
          fontFamily="Oswald"
          fontSize="xl"
          bgColor="white"
          color="orange"
          cursor="pointer"
        >
          <HatIcon />
          Підготовка до НМТ
        </Flex>
      </Flex>

      <Flex flexDir="column" borderRadius="1rem" h="full" mt="4.5rem">
        {sidebarItems.map(({ label, icon, path }) => (
          <SidebarButton key={path} icon={icon} label={label} path={path} />
        ))}
      </Flex>
    </Box>
  );
}
