import { ResponsivePie } from '@nivo/pie';
import { Flex, Text } from '@chakra-ui/react';

interface SubjectStat {
  objectId: number;
  objectName: string;
  score: number;
  maxScore: number;
}

export const DonutChart = ({ data }: { data: SubjectStat[] }) => {
  const math = data.find((d) => d.objectName === 'Математика');

  if (!math || math.maxScore === 0) {
    return <Text>Недостатньо даних по Математиці</Text>;
  }

  const percent = Math.round((math.score / math.maxScore) * 100);

  const chartData = [
    { id: 'Правильні', label: 'Правильні', value: math.score },
    { id: 'Неправильні', label: 'Неправильні', value: math.maxScore - math.score },
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
      maxW="280px"
      flexDir="column"
      position="relative"
      bg="white"
    >
      <Text textAlign="center" mb={4}>
        Математика
      </Text>
      <ResponsivePie
        data={chartData}
        innerRadius={0.65}
        padAngle={1.8}
        cornerRadius={5}
        activeOuterRadiusOffset={8}
        colors={['#48BB78', '#E53E3E']}
        enableArcLabels={false}
        enableArcLinkLabels={false}
        borderWidth={2}
        borderColor={{ from: 'color', modifiers: [['darker', 0.3]] }}
      />
      <Text
        position="absolute"
        top="50%"
        left="50%"
        transform="translate(-50%, -50%)"
        fontSize="2xl"
        fontWeight="bold"
      >
        {percent}%
      </Text>
    </Flex>
  );
};
