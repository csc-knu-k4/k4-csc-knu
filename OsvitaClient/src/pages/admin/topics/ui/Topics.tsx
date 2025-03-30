import { Text, Flex, Box } from '@chakra-ui/react';
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
    <Flex
      position="sticky"
      top="0"
      bg="white"
      zIndex="3"
      p={4}
      justifyContent="space-between"
      alignItems="center"
      borderBottom="1px solid #ddd"
    >
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
    <Box height="calc(100vh - 180px)" display="flex" flexDirection="column">
      {header}
      <Box flex="1" overflowY="auto" p={4}>
        {topics && topics.length > 0 ? <TopicsTable items={topics} /> : <Text>Дані відсутні.</Text>}
      </Box>
    </Box>
  );
};

export default Topics;
