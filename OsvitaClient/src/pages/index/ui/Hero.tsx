import IndexPictureHero from '@/shared/assets/IndexPictureHero';
import { Button, Flex, Text } from '@chakra-ui/react';
const Hero = () => {
  return (
    <>
      <Flex justifyContent="space-between" alignItems="center">
        <Flex flexDir="column" gap={5} w="29rem">
          <Text fontSize="4xl" fontWeight="bold">
            Підготовка до НМТ
          </Text>
          <Text fontSize="xl">
            Поринь у світ підготовки до НМТ! Ми пропонуємо тобі всі необхідні інструменти для
            успішного складання.
          </Text>
          <Button h="3.25rem" fontSize="2xl" bgColor="orange" borderRadius="1rem">
            Розпочати підготовку
          </Button>
        </Flex>
        <IndexPictureHero />
      </Flex>
    </>
  );
};

export default Hero;
