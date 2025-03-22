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
        flexDir={{ base: 'column', md: 'row' }}
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
        <Text fontSize="xl" textAlign="center">
          {title}
        </Text>
        <Button
          size={{ base: 'xs', md: 'md' }}
          rounded="xl"
          fontSize={{ base: 'xs', md: 'md' }}
          bgColor={showTest ? 'blue' : 'orange'}
          onClick={onToggleMode}
        >
          {showTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
      </Flex>

      {/* Основний контент */}
      <Flex flex="1" overflowY="auto" p={{ base: 'none', md: 4 }}>
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
        flexDir={{ base: 'column', md: 'row' }}
        justifyContent="space-between"
        alignItems="center"
        p={{ base: 'none', md: 5 }}
        position="sticky"
        bottom={{ base: '-5', md: '0' }}
        zIndex="10"
        bg="white"
      >
        <Button
          color="orange"
          variant="ghost"
          fontSize={{ base: 'xs', md: 'md' }}
          onClick={() => navigate(-1)}
        >
          <IoIosArrowBack /> Повернутися назад
        </Button>
        <Button
          size={{ base: 'xs', md: 'md' }}
          rounded="xl"
          fontSize={{ base: 'xs', md: 'md' }}
          bgColor={showTest ? 'blue' : 'orange'}
          onClick={onToggleMode}
        >
          {showTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
        <Button
          color="orange"
          variant="ghost"
          fontSize={{ base: 'xs', md: 'md' }}
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
