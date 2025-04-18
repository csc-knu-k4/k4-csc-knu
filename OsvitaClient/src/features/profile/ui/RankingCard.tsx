import { Flex, Text, Box, Skeleton } from '@chakra-ui/react';
import { FaArrowTrendUp } from 'react-icons/fa6';
import { useRating } from '../model/hooks';
import { useMemo } from 'react';
import { RankingRow } from './RankingRow';
import { RankingFixedRow } from './RankingFixedRow';
import { useInView } from 'react-intersection-observer';

const getUserIdFromStorage = (): number | null => {
  const raw = localStorage.getItem('userId');
  return raw ? Number(raw) : null;
};

export const RankingCard = () => {
  const { data: rating = [], isLoading } = useRating();
  const userId = getUserIdFromStorage();

  const sortedRating = useMemo(() => [...rating].sort((a, b) => a.place - b.place), [rating]);
  const currentUser = sortedRating.find((item) => item.userModel.id === userId);

  const { ref: userInViewRef, inView: isUserVisible } = useInView({
    threshold: 0.1,
  });

  return (
    <Flex
      p="25px"
      borderRadius="1rem"
      boxShadow="sm"
      fontSize="xl"
      fontWeight="semibold"
      w="100%"
      maxW="500px"
      flexDir="column"
      position="relative"
      bg="white"
    >
      <Flex gap={2} alignItems="center" justifyContent="center" mb={5}>
        <FaArrowTrendUp color="rgb(255, 109, 24)" />
        <Text color="orange">Рейтинг</Text>
      </Flex>

      <Box overflowY="auto" maxH="340px" pr={1} mb={2}>
        {isLoading
          ? [...Array(8)].map((_, i) => (
              <Skeleton
                key={i}
                h="45px"
                borderRadius="1rem"
                mb={2}
                css={{
                  '--start-color': 'colors.orange.100',
                  '--end-color': 'colors.orange.200',
                }}
              />
            ))
          : sortedRating.map((item) => {
              const isCurrent = item.userModel.id === userId;

              return (
                <Box key={item.userModel.id} ref={isCurrent ? userInViewRef : null}>
                  <RankingRow
                    place={item.place}
                    name={`${item.userModel.firstName} ${item.userModel.secondName}`}
                    score={item.score}
                    highlight={isCurrent}
                  />
                </Box>
              );
            })}
      </Box>

      {!isUserVisible && currentUser && !isLoading && (
        <RankingFixedRow
          place={currentUser.place}
          name={`${currentUser.userModel.firstName} ${currentUser.userModel.secondName}`}
          score={currentUser.score}
        />
      )}
    </Flex>
  );
};
