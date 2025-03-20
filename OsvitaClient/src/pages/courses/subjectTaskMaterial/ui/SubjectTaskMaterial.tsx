import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getMaterialContentById } from '@/shared/api/materialsApi';
import { Button } from '@/components/ui/button';
import { Flex, Text, Spinner, Box } from '@chakra-ui/react';
import { IoIosArrowForward, IoIosArrowBack } from 'react-icons/io';

interface MaterialContent {
  id: number;
  title: string;
  contentBlockModelType: number;
  orderPosition: number;
  materialId: number;
  value: string;
}

const SubjectTaskMaterial = () => {
  const { materialId } = useParams<{ materialId: string }>();
  const [content, setContent] = useState<MaterialContent | null>(null);
  const [loading, setLoading] = useState(true);
  const [isTest, setIsTest] = useState(false);

  useEffect(() => {
    if (materialId) {
      getMaterialContentById(Number(materialId))
        .then((data) => setContent(data[0]))
        .catch((error) => console.error('Помилка завантаження матеріалу:', error))
        .finally(() => setLoading(false));
    }
  }, [materialId]);

  return (
    <Flex minHeight="100vh" flexDir="column">
      <Flex
        flexDir="row"
        justifyContent="space-between"
        alignItems="center"
        p={5}
        position="sticky"
        top="0"
        zIndex="10"
      >
        <Text fontSize="2xl" fontWeight="bold" color="orange">
          Матеріал
        </Text>
        <Text fontSize="xl">{content?.title || 'Завантаження...'}</Text>
        <Button rounded="xl" fontSize="md" bgColor="orange" onClick={() => setIsTest(!isTest)}>
          {isTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
      </Flex>

      {/* Контент з прокруткою */}
      <Box flex="1" overflowY="auto" p={5}>
        {loading ? (
          <Spinner size="xl" />
        ) : (
          <Text>{isTest ? 'Тут буде тест' : content?.value || 'Немає контенту'}</Text>
        )}
      </Box>

      <Flex
        flexDir="row"
        justifyContent="space-between"
        alignItems="center"
        p={5}
        position="sticky"
        bottom="0"
        zIndex="10"
      >
        <Button color="orange" variant="ghost" fontSize="md">
          <IoIosArrowBack /> Повернутися назад
        </Button>
        <Button rounded="xl" fontSize="md" bgColor="orange" onClick={() => setIsTest(!isTest)}>
          {isTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
        <Button color="orange" variant="ghost" fontSize="md">
          Наступна тема <IoIosArrowForward />
        </Button>
      </Flex>
    </Flex>
  );
};

export default SubjectTaskMaterial;
