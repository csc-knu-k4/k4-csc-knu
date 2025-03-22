import { Flex, Text, Button } from '@chakra-ui/react';
import { IoIosArrowForward, IoIosArrowBack } from 'react-icons/io';
import { useNavigate } from 'react-router-dom';

const SubjectTaskLayout = ({
  title,
  showTest,
  onToggleMode,
  onNextTopic,
  isNextDisabled = false,
  nextTopicTitle = '',
  children,
}: {
  title: string;
  showTest: boolean;
  onToggleMode: () => void;
  onNextTopic?: () => void;
  isNextDisabled?: boolean;
  nextTopicTitle?: string;
  children: React.ReactNode;
}) => {
  const navigate = useNavigate();

  return (
    <Flex minHeight="100vh" flexDir="column">
      {/* Header */}
      <Flex
        flexDir="row"
        justifyContent="space-between"
        alignItems="center"
        px={3}
        position="sticky"
        top="0"
        zIndex="10"
        bg="white"
      >
        <Text fontSize="2xl" fontWeight="bold" color="orange">
          {showTest ? 'Тестування' : 'Матеріал'}
        </Text>
        <Text fontSize="xl">{title}</Text>
        <Button rounded="xl" fontSize="md" bgColor="orange" onClick={onToggleMode}>
          {showTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
      </Flex>

      {/* Основний контент */}
      <Flex flex="1" overflowY="auto" p={5}>
        <Flex flexDir="column" w="full">
          {children}
          {nextTopicTitle && (
            <Text mt={4} fontSize="md" fontWeight="semibold" color="orange.500">
              Наступна тема: {nextTopicTitle}
            </Text>
          )}
        </Flex>
      </Flex>

      {/* Footer */}
      <Flex
        flexDir="row"
        justifyContent="space-between"
        alignItems="center"
        p={5}
        position="sticky"
        bottom="0"
        zIndex="10"
        bg="white"
      >
        <Button color="orange" variant="ghost" fontSize="md" onClick={() => navigate(-1)}>
          <IoIosArrowBack /> Повернутися назад
        </Button>
        <Button rounded="xl" fontSize="md" bgColor="orange" onClick={onToggleMode}>
          {showTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
        <Button
          color="orange"
          variant="ghost"
          fontSize="md"
          onClick={onNextTopic}
          disabled={isNextDisabled}
        >
          Наступна тема <IoIosArrowForward />
        </Button>
      </Flex>
    </Flex>
  );
};

export default SubjectTaskLayout;
