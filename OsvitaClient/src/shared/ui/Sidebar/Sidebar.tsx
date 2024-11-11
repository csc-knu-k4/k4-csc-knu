import { Box, Button, Flex } from '@chakra-ui/react';
import { SidebarButton } from './SidebarButton';
import { GiTopHat } from 'react-icons/gi';
import { sidebarItems } from './sidebarItems';

export function Sidebar() {
  return (
    <Box h="calc(100vh - 2.5rem)" bg="white" borderRadius="1rem">
      <Flex justifyContent="center" alignItems="center" mb="1rem">
        <Button mt={4} fontSize="sm" bgColor="blue" color="white" borderRadius="0.5rem">
          <GiTopHat />
          Підготовка до НМТ
        </Button>
      </Flex>

      <Flex flexDir="column" borderRadius="1rem" h="full" mt="4.5rem">
        {sidebarItems.map(({ label, icon, path }) => (
          <SidebarButton key={path} icon={icon} label={label} path={path} />
        ))}
      </Flex>
    </Box>
  );
}
