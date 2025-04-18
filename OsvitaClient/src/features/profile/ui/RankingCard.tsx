import { Flex, Text } from '@chakra-ui/react';
import { FaArrowTrendUp } from 'react-icons/fa6';
import { RankingRow } from './RankingRow';

export const RankingCard = () => {
  const users = [
    { place: 1, name: 'Костюк Світлана', points: 345 },
    { place: 2, name: 'Костюк Світлана', points: 345 },
    { place: 3, name: 'Костюк Світлана', points: 345 },
    { place: 4, name: 'Костюк Світлана', points: 345 },
    { place: 5, name: 'Костюк Світлана', points: 345 },
  ];

  return (
    <Flex
      p="25px"
      borderRadius="1rem"
      boxShadow="sm"
      fontSize="xl"
      fontWeight="semibold"
      h="auto"
      w="100%"
      maxW="500px"
      flexDir="column"
    >
      <Flex gap={2} alignItems="center" justifyContent="center" mb={5}>
        <FaArrowTrendUp color="rgb(255, 109, 24)" />
        <Text color="orange">Рейтинг</Text>
      </Flex>
      <Flex gap={2} flexDir="column" maxH="280px" overflowY="auto">
        {users.map((u) => (
          <RankingRow key={u.place} {...u} />
        ))}
      </Flex>
    </Flex>
  );
};
