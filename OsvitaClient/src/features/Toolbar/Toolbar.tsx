import { Box, Flex, HStack } from '@chakra-ui/react';
import { SearchInput } from '@/shared/ui/SearchInput';
import { UserAvatar } from '@/shared/ui/Avatar';

export function Toolbar() {
  return (
    <Box bg="white" p={4} borderRadius="1rem" w="full">
      <Flex justifyContent="space-between" alignItems="center">
        <HStack gap="10">
          <SearchInput />
        </HStack>
        <UserAvatar />
      </Flex>
    </Box>
  );
}
