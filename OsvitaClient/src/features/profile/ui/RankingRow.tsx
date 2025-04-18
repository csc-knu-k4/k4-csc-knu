import { Flex, Text } from '@chakra-ui/react';
import { PiMedal } from 'react-icons/pi';
import { UserAvatar } from '@/shared/ui/Avatar';

type Props = {
  place: number;
  name: string;
  points: number;
};

export const RankingRow = ({ place, name, points }: Props) => (
  <Flex
    borderRadius="1rem"
    borderColor="orange"
    borderWidth="1px"
    justifyContent="space-between"
    alignItems="center"
    h="45px"
    px={4}
  >
    <Flex alignItems="center" gap={2}>
      <Text color="orange.200" fontSize="lg">
        {place}
      </Text>
      <UserAvatar />
      <Text color="orange.200" fontSize="lg">
        {name}
      </Text>
    </Flex>
    <Flex gap={1} alignItems="center">
      <Text fontSize="md" fontWeight="semibold" color="orange.200">
        {points} балів
      </Text>
      <PiMedal color="rgb(255, 109, 24)" />
    </Flex>
  </Flex>
);
