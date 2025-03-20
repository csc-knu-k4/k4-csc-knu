import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getMaterialContentById } from '@/shared/api/materialsApi';
import { Button } from '@/components/ui/button';
import { Flex, Text, Spinner } from '@chakra-ui/react';
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
  const { materialId } = useParams<{ materialId: string }>(); // Отримуємо ID як string
  const [content, setContent] = useState<MaterialContent | null>(null); // Вказуємо тип для content
  const [loading, setLoading] = useState(true);
  const [isTest, setIsTest] = useState(false);

  useEffect(() => {
    if (materialId) {
      getMaterialContentById(Number(materialId)) // Перетворюємо materialId у число
        .then((data) => setContent(data[0])) // API повертає масив, беремо перший елемент
        .catch((error) => console.error('Помилка завантаження матеріалу:', error))
        .finally(() => setLoading(false));
    }
  }, [materialId]);

  return (
    <Flex h="full" flexDir="column" justifyContent="space-between" p={5}>
      <Flex flexDir="row" justifyContent="space-between" alignItems="center">
        <Text fontSize="2xl" fontWeight="bold" color="orange">
          Матеріал
        </Text>
        <Text fontSize="xl">{content?.title || 'Завантаження...'}</Text>
        <Button rounded="xl" fontSize="md" bgColor="orange" onClick={() => setIsTest(!isTest)}>
          {isTest ? 'Матеріал' : 'Пройти тест'}
        </Button>
      </Flex>

      {loading ? (
        <Spinner size="xl" />
      ) : (
        <Text>{isTest ? 'Тут буде тест' : content?.value || 'Немає контенту'}</Text>
      )}

      <Flex flexDir="row" justifyContent="space-between" alignItems="center">
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
