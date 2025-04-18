import { Chart, useChart } from '@chakra-ui/charts';
import { Flex } from '@chakra-ui/react';
import { Cell, Label, Pie, PieChart, Tooltip } from 'recharts';

const DonutChart = () => {
  const chart = useChart({
    data: [
      { name: 'windows', value: 400, color: 'blue.solid' },
      { name: 'mac', value: 300, color: 'orange.solid' },
      { name: 'linux', value: 300, color: 'pink.solid' },
      { name: 'other', value: 200, color: 'green.solid' },
    ],
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
      <Chart.Root boxSize="200px" chart={chart} mx="auto">
        <PieChart>
          <Tooltip cursor={false} animationDuration={100} content={<Chart.Tooltip hideLabel />} />
          <Pie
            innerRadius={80}
            outerRadius={100}
            isAnimationActive={false}
            data={chart.data}
            dataKey={chart.key('value')}
            nameKey="name"
          >
            <Label
              content={({ viewBox }) => (
                <Chart.RadialText
                  viewBox={viewBox}
                  title={chart.getTotal('value').toLocaleString()}
                  description="users"
                />
              )}
            />
            {chart.data.map((item) => (
              <Cell key={item.color} fill={chart.color(item.color)} />
            ))}
          </Pie>
        </PieChart>
      </Chart.Root>
    </Flex>
  );
};

export default DonutChart;
