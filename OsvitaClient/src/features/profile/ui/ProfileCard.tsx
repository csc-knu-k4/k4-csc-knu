import { Badge, Flex, Skeleton, Text } from '@chakra-ui/react';
import { Avatar } from '@/components/ui/avatar';
import { useUser } from '../model/hooks';

export const ProfileCard = () => {
  const { data: user, isLoading, isError } = useUser();

  if (isLoading) {
    return <Skeleton w="100%" maxW="500px" h="270px" borderRadius="1rem" />;
  }

  if (isError || !user) {
    return (
      <Text color="red.500" w="100%" textAlign="center">
        Помилка при завантаженні профілю
      </Text>
    );
  }

  return (
    <Flex
      borderRadius="1rem"
      boxShadow="sm"
      flexDir="column"
      justifyContent="center"
      alignItems="center"
      h="270px"
      w="100%"
      maxW="500px"
      px={4}
    >
      <Avatar size="2xl" />
      <Text fontSize="xl" my={4} fontWeight="bold">
        {user.firstName} {user.secondName}
      </Text>
      <Badge colorScheme="gray">
        {user.roles[0].charAt(0).toUpperCase() + user.roles[0].slice(1)}
      </Badge>
    </Flex>
  );
};
