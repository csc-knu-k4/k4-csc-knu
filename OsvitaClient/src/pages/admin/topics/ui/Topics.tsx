import { Text, Flex } from '@chakra-ui/react';
import { AddTopicButton } from '@/features/topics';
import { TopicsTable } from '@/entities/topics';
import { useQuery } from 'react-query';
import { getTopics, getTopicsByChapter } from '@/shared/api/topicsApi';
import { useLocation } from 'react-router-dom';

const Topics = () => {
  const location = useLocation();
  const chapterId = location.state?.chapterId;

  const {
    data: topics,
    isLoading,
    isError,
  } = useQuery(
    ['topics', chapterId],
    () => (chapterId ? getTopicsByChapter(chapterId) : getTopics()),
    { keepPreviousData: true },
  );

  const header = (
    <Flex justifyContent="space-between" alignItems="center" mb={2}>
      <Text fontSize="2xl" fontWeight="medium">
        {chapterId ? `Теми для розділу #${chapterId}` : 'Теми'}
      </Text>
      <AddTopicButton />
    </Flex>
  );

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  if (isError) {
    return (
      <>
        {header}
        <Text color="red.500">Помилка завантаження даних.</Text>
      </>
    );
  }

  return (
    <>
      {header}
      {topics && topics.length > 0 ? <TopicsTable items={topics} /> : <Text>Дані відсутні.</Text>}
    </>
  );
};

export default Topics;
