import IndexPictureHero from '@/shared/assets/IndexPictureHero';
import { Button, Flex, Text } from '@chakra-ui/react';

const Hero = () => {
  return (
    <Flex
      justifyContent={{ base: 'center', md: 'space-between' }}
      alignItems="center"
      flexDir={{ base: 'column', md: 'row' }}
      mt={4}
      gap={{ base: 6, md: 0 }}
    >
      <Flex
        flexDir="column"
        gap={5}
        w={{ base: '100%', md: '50%', lg: '40%' }}
        textAlign={{ base: 'center', md: 'left' }}
      >
        <Text fontSize={{ base: '2xl', md: '4xl' }} fontWeight="bold">
          Підготовка до НМТ
        </Text>
        <Text fontSize={{ base: 'md', md: 'xl' }}>
          Поринь у світ підготовки до НМТ! Ми пропонуємо тобі всі необхідні інструменти для
          успішного складання.
        </Text>
        <Button
          h={{ base: '2.75rem', md: '3.25rem' }}
          fontSize={{ base: 'lg', md: '2xl' }}
          bgColor="orange"
          borderRadius="1rem"
          alignSelf={{ base: 'center', md: 'flex-start' }}
        >
          Розпочати підготовку
        </Button>
      </Flex>
      <Flex
        w={{ base: '100%', md: '50%', lg: '40%' }}
        justifyContent="center"
        minH={{ base: '300px', md: '565px' }}
      >
        <IndexPictureHero />
      </Flex>
    </Flex>
  );
};

export default Hero;
