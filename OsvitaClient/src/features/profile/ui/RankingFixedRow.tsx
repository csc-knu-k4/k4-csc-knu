import { Box } from '@chakra-ui/react';
import { RankingRow } from './RankingRow';

type Props = {
  place: number;
  name: string;
  score: number;
};

export const RankingFixedRow = ({ place, name, score }: Props) => {
  return (
    <Box
      position="absolute"
      bottom="10px"
      left="0"
      width="calc(100% - 2rem)"
      px={0}
      zIndex={10}
      ml={2}
    >
      <Box maxW="500px" mx="auto" px={4}>
        <RankingRow place={place} name={name} score={score} highlight />
      </Box>
    </Box>
  );
};
