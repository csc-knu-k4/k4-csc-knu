import { Text, Flex } from '@chakra-ui/react';
import { AddTopicButton } from '@/features/topics';
import { TopicsTable } from '@/entities/topics';
import { useQuery } from 'react-query';
import { getTopics, getTopicsByChapter } from '@/shared/api/topicsApi';
import { useLocation } from 'react-router-dom';

const Topics = () => {
  const location = useLocation();
  const chapterId = location.state?.chapterId;

  const { data: topics, isLoading } = useQuery(
    ['topics', chapterId],
    () => (chapterId ? getTopicsByChapter(chapterId) : getTopics()),
    { keepPreviousData: true },
  );

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          {chapterId ? `Теми для розділу #${chapterId}` : 'Теми'}
        </Text>
        <AddTopicButton />
      </Flex>
      <TopicsTable items={topics} />
    </>
  );
};

export default Topics;
