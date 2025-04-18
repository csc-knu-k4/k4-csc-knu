import { NativeSelect, Box, Text, Flex } from '@chakra-ui/react';
import { ResponsiveBar } from '@nivo/bar';
import { useState } from 'react';

interface TopicStat {
  objectId: number;
  objectName: string;
  score: number;
  maxScore: number;
}

export const TopicBarChart = ({ data }: { data: TopicStat[] }) => {
  const [selectedTopicId, setSelectedTopicId] = useState<number | null>(null);

  const topics = data.filter((d) => d.maxScore > 0);
  const selected = selectedTopicId ? topics.find((t) => t.objectId === selectedTopicId) : topics[0];

  if (!selected) return <Text>Немає даних для відображення</Text>;

  const chartData = [
    {
      topic: selected.objectName,
      Правильні: selected.score,
      Неправильні: selected.maxScore - selected.score,
    },
  ];

  return (
    <Flex
      p="25px"
      borderRadius="1rem"
      boxShadow="sm"
      fontSize="xl"
      fontWeight="semibold"
      w="100%"
      h="280px"
      maxW="710px"
      flexDir="column"
      position="relative"
      bg="white"
    >
      <NativeSelect.Root size="sm" width="100%" maxW="320px" mb={4}>
        <NativeSelect.Field
          value={selected.objectId}
          onChange={(value) => setSelectedTopicId(Number(value))}
        >
          {topics.map((topic) => (
            <option key={topic.objectId} value={topic.objectId}>
              {topic.objectName}
            </option>
          ))}
        </NativeSelect.Field>
        <NativeSelect.Indicator />
      </NativeSelect.Root>

      <Box h="300px" position="relative">
        <ResponsiveBar
          data={chartData}
          keys={['Правильні', 'Неправильні']}
          indexBy="topic"
          margin={{ top: 50, right: 30, bottom: 50, left: 60 }}
          padding={0.3}
          layout="horizontal"
          colors={['#38A169', '#E53E3E']}
          borderRadius={3}
          labelSkipWidth={12}
          labelSkipHeight={12}
          labelTextColor="#fff"
          axisLeft={null}
          tooltip={({ id, value }) => (
            <strong>
              {id}: {value}
            </strong>
          )}
        />
      </Box>
    </Flex>
  );
};
