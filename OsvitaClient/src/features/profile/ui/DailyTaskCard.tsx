import { Button, Flex, Text } from '@chakra-ui/react';
import { CiCalendar } from 'react-icons/ci';
import { FaRegCheckCircle } from 'react-icons/fa';
import { PiMedal } from 'react-icons/pi';

export const DailyTaskCard = () => (
  <Flex
    p="25px"
    h="auto"
    w={{ base: '100%', md: '500px' }}
    borderRadius="1rem"
    boxShadow="sm"
    flexDir="column"
  >
    <Button
      mb={6}
      borderRadius="1rem"
      fontSize="lg"
      fontWeight="semibold"
      colorPalette="orange"
      justifyContent="start"
    >
      <CiCalendar size="1.5rem" />
      Щоденні Завдання
    </Button>
    <Flex justifyContent="space-between" alignItems="center" flexWrap="wrap" gap={4}>
      <Flex gap={1} alignItems="center">
        <FaRegCheckCircle size="1.5rem" color="green" />
        <Text color="gray">Сьогоднішнє завдання виконане</Text>
      </Flex>
      <Flex gap={1} alignItems="center">
        <PiMedal size="1.5rem" color="rgb(255, 109, 24)" />
        <Text fontSize="lg" fontWeight="semibold" color="orange">
          345 балів
        </Text>
      </Flex>
    </Flex>
  </Flex>
);
