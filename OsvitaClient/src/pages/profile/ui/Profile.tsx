import { Container, Flex } from '@chakra-ui/react';
import { ProfileCard, RankingCard, DailyTaskCard } from '@/features/profile';

const Profile = () => {
  return (
    <Container maxW="100%" h="calc(100dvh - 180px)" overflowY="auto" py={6}>
      <Flex flexDir="row" flexWrap="wrap" gap={8} justify="center">
        <Flex flexDir="column" alignItems="center" gap={8}>
          <ProfileCard />
          <DailyTaskCard />
        </Flex>
        <RankingCard />
      </Flex>
    </Container>
  );
};

export default Profile;
