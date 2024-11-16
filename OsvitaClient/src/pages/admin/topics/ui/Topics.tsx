import { Text, Flex } from '@chakra-ui/react';
import { AddTopicButton } from '@/features/topics';
import { TopicsTable } from '@/entities/topics';
import { useQuery } from 'react-query';
import { getTopics } from '@/shared/api/topicsApi';

const Topics = () => {
  const { data: topics, isLoading } = useQuery(['topics'], getTopics);

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Теми
        </Text>
        <AddTopicButton />
      </Flex>
      <TopicsTable items={topics} />
    </>
  );
};

export default Topics;
