import { Box } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';

export function ContentArea() {
  return (
    <Box
      bg="white"
      p={{ base: 3, md: 6 }}
      borderRadius="1rem"
      h="calc(100vh - 8.25rem)"
      overflowY="hidden"
    >
      <Outlet />
    </Box>
  );
}
