import { Button } from '@/components/ui/button';
import { Flex, Text } from '@chakra-ui/react';
import { IoIosArrowForward, IoIosArrowBack } from 'react-icons/io';

const SubjectTaskMaterial = () => {
  return (
    <Flex h="full" flexDir="column" justifyContent="space-between">
      <Flex flexDir="row" justifyContent="space-between" alignItems="center">
        <Text fontSize="2xl" fontWeight="bold" color="orange">
          Математика
        </Text>
        <Text fontSize="xl">Властивості дій з дійсними числами</Text>
        <Button rounded="xl" fontSize="md" bgColor="orange">
          Пройти тест
        </Button>
      </Flex>

      <Text>Subject Task Material</Text>

      <Flex flexDir="row" justifyContent="space-between" alignItems="center">
        <Button color="orange" variant="ghost" fontSize="md">
          <IoIosArrowBack /> Наступна тема
        </Button>
        <Button rounded="xl" fontSize="md" bgColor="orange">
          Пройти тест
        </Button>
        <Button color="orange" variant="ghost" fontSize="md">
          Наступна тема <IoIosArrowForward />
        </Button>
      </Flex>
    </Flex>
  );
};

export default SubjectTaskMaterial;
