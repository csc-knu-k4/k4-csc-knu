import { Container, Flex } from '@chakra-ui/react';
import { ProfileCard } from '@/features/profile/ui/ProfileCard';
import { DailyTaskCard } from '@/features/profile/ui/DailyTaskCard';
import { RankingCard } from '@/features/profile/ui/RankingCard';

const Profile = () => {
  return (
    <Container maxW="100%" h="calc(100dvh - 180px)" overflowY="auto" py={6}>
      <Flex
        flexDir="row"
        flexWrap="wrap"
        gap={8}
        justify="center" // центрування по горизонталі
      >
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
