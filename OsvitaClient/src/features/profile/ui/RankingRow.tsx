import { Flex, Text } from '@chakra-ui/react';
import { PiMedal } from 'react-icons/pi';
import { UserAvatar } from '@/shared/ui/Avatar';

type Props = {
  place: number;
  name: string;
  score: number;
  highlight?: boolean;
};

export const RankingRow = ({ place, name, score, highlight }: Props) => (
  <Flex
    borderRadius="1rem"
    borderColor={highlight ? 'orange.400' : 'orange'}
    borderWidth="1px"
    justifyContent="space-between"
    alignItems="center"
    h="45px"
    px={4}
    bg={highlight ? 'orange' : 'transparent'}
    my={1}
  >
    <Flex alignItems="center" gap={2}>
      <Text color={highlight ? 'white' : 'orange.200'} fontSize="lg">
        {place}
      </Text>
      <UserAvatar />
      <Text color={highlight ? 'white' : 'orange.200'} fontSize="lg">
        {name}
      </Text>
    </Flex>
    <Flex gap={1} alignItems="center">
      <Text fontSize="md" fontWeight="semibold" color={highlight ? 'white' : 'orange.200'}>
        {score} балів
      </Text>
      <PiMedal color={highlight ? 'white' : 'rgb(255, 109, 24)'} />
    </Flex>
  </Flex>
);
