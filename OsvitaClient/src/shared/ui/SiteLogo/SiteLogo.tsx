import { Flex } from '@chakra-ui/react';
import HatIcon from '@/shared/assets/HatIcon';
import { useNavigate } from 'react-router-dom';

export const SiteLogo = () => {
  const navigate = useNavigate();

  return (
    <Flex
      gap={2}
      justifyContent="center"
      alignItems="center"
      fontFamily="Oswald"
      fontSize="xl"
      bgColor="white"
      color="orange"
      cursor="pointer"
      onClick={() => navigate('/course')}
    >
      <HatIcon />
      Підготовка до НМТ
    </Flex>
  );
};
