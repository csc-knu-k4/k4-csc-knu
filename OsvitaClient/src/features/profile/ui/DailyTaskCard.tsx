import { Button, Flex, Skeleton, Text } from '@chakra-ui/react';
import { CiCalendar } from 'react-icons/ci';
import { FaRegCheckCircle } from 'react-icons/fa';
import { FaFire } from 'react-icons/fa6'; // Вогник
import { useDailyAssignmentStatus, useDailyStreak } from '../model/hooks';
import { useNavigate } from 'react-router-dom';
import { getDailyAssignmentSet } from '@/shared/api/dailyAssignmentApt';

export const DailyTaskCard = () => {
  const { data: isDone, isLoading: isStatusLoading, isError } = useDailyAssignmentStatus();
  const { data: streak, isLoading: isStreakLoading } = useDailyStreak();

  const navigate = useNavigate();

  const handleStartTest = async () => {
    try {
      const userId = Number(localStorage.getItem('userId'));
      const data = await getDailyAssignmentSet(userId);

      if (!data?.id && data?.id !== 0) {
        console.error('Щоденне завдання не знайдено або має неправильний ID');
        return;
      }

      navigate(`/course/daily-assignment/${data.id}`);
    } catch (error) {
      console.error('Помилка при отриманні щоденного завдання:', error);
    }
  };

  return (
    <Flex
      p="25px"
      h="auto"
      w="100%"
      maxW="500px"
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
        onClick={handleStartTest}
      >
        <CiCalendar size="1.5rem" />
        Щоденні Завдання
      </Button>

      <Flex justifyContent="space-between" alignItems="center" flexWrap="wrap" gap={4}>
        {/* Статус */}
        <Flex gap={1} alignItems="center">
          {isStatusLoading ? (
            <Skeleton h="20px" w="200px" />
          ) : isError ? (
            <Text color="red.400">Не вдалося завантажити</Text>
          ) : isDone ? (
            <>
              <FaRegCheckCircle size="1.5rem" color="green" />
              <Text color="gray">Сьогоднішнє завдання виконане</Text>
            </>
          ) : (
            <>
              <FaRegCheckCircle size="1.5rem" color="gray" />
              <Text color="gray">Сьогоднішнє завдання ще не виконане</Text>
            </>
          )}
        </Flex>

        {/* Стік */}
        <Flex gap={1} alignItems="center">
          {isStreakLoading ? (
            <Skeleton h="20px" w="80px" />
          ) : (
            <Flex flexDir="row" alignItems="center" gap={1}>
              <FaFire size="1.5rem" color="orange" />
              <Text mt={1} fontSize="lg" fontWeight="semibold" color="orange">
                {streak} днів поспіль
              </Text>
            </Flex>
          )}
        </Flex>
      </Flex>
    </Flex>
  );
};
