import { Flex } from '@chakra-ui/react';
import HatIcon from '@/shared/assets/HatIcon';

export const SiteLogo = () => {
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
    >
      <HatIcon />
      Підготовка до НМТ
    </Flex>
  );
};
