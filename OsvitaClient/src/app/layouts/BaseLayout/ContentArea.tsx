import { Box } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';

export function ContentArea() {
  return (
    <Box bg="white" p={5} borderRadius="1rem" h="calc(100vh - 8.25rem)" overflowY="auto">
      <Outlet />
    </Box>
  );
}
