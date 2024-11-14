import HatIcon from '@/shared/assets/HatIcon';
import { Button, Flex, Text } from '@chakra-ui/react';
import { useNavigate } from 'react-router-dom';
const Header = () => {
  const navigate = useNavigate();

  const routeToAdmin = () => {
    navigate('/admin');
  };

  return (
    <>
      <Flex justifyContent="space-between">
        <Flex justifyContent="center" alignItems="center" flexDir="row" gap={2}>
          <HatIcon />
          <Text fontFamily="Oswald" fontSize="xl" color="#FF6D18">
            Підготовка до НМТ
          </Text>
        </Flex>
        <Button
          fontSize="md"
          px="2.5rem"
          bgColor="#FF6D18"
          borderRadius="0.6rem"
          onClick={routeToAdmin}
        >
          Увійти
        </Button>
      </Flex>
    </>
  );
};

export default Header;
