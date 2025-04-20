import { Container, Flex } from '@chakra-ui/react';
import { ProfileCard, RankingCard, DailyTaskCard } from '@/features/profile';
import { useQuery } from 'react-query';
import { getChartSubject, getChartTopic } from '@/shared/api/chartsApi';
import { DonutChart } from '@/features/profile/ui/DonutChart';
import { TopicBarChart } from '@/features/profile/ui/TopicBarChart';

const Profile = () => {
  const userId = Number(localStorage.getItem('userId'));
  const { data: chartData } = useQuery(['subjectChart', userId], () => getChartSubject(userId));
  const { data: topicData } = useQuery(['topicChart', userId], () => getChartTopic(userId));

  return (
    <Container maxW="100%" h="calc(100dvh - 180px)" overflowY="auto" py={6}>
      <Flex flexDir="column" gap={8} justifyContent="center">
        <Flex flexDir="row" flexWrap="wrap" gap={8} justify="center">
          <Flex flexDir="column" alignItems="center" gap={8}>
            <ProfileCard />
            <DailyTaskCard />
          </Flex>
          <RankingCard />
        </Flex>
        <Flex flexDir="row" flexWrap="wrap" gap={8} justifyContent="center">
          <DonutChart data={chartData ?? []} />
          <TopicBarChart data={topicData ?? []} />
        </Flex>
      </Flex>
    </Container>
  );
};

export default Profile;
