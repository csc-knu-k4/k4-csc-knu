import { Avatar } from '@/components/ui/avatar';
import { Badge, Flex, Text } from '@chakra-ui/react';

export const ProfileCard = () => (
  <Flex
    borderRadius="1rem"
    boxShadow="sm"
    flexDir="column"
    justifyContent="center"
    alignItems="center"
    h="270px"
    w={{ base: '100%', md: '500px' }}
    px={4}
  >
    <Avatar size="2xl" />
    <Text fontSize="xl" my={4} fontWeight="bold">
      Костюк Світлана
    </Text>
    <Badge colorPalette="gray">Учень</Badge>
  </Flex>
);
