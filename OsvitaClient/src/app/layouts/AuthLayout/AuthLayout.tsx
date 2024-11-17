import { Flex, Box, Container } from '@chakra-ui/react';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <Flex
      h="100vh"
      w="100vw"
      justifyContent="center"
      alignItems="center"
      position="relative"
      bg="gray.50"
      overflow="hidden"
    >
      {/* Круг в правом верхньом куті */}
      <Box
        position="absolute"
        top="-8.25rem"
        right="-16.25rem"
        w="32.5rem"
        h="32.5rem"
        bg="orange.200"
        borderRadius="50%"
        zIndex="1"
      />

      {/* Круг в нижньому лівому куті */}
      <Box
        position="absolute"
        bottom="-5.25rem"
        left="-12.25rem"
        w="32.5rem"
        h="32.5rem"
        bg="orange.200"
        borderRadius="50%"
        zIndex="1"
      />

      {/* Маленький круг */}
      <Box
        position="absolute"
        bottom="24.25rem"
        left="-4.25rem"
        w="13.5rem"
        h="13.5rem"
        bg="orange.100"
        borderRadius="50%"
        zIndex="1"
      />

      <Container
        maxW="500px"
        centerContent
        zIndex="2"
        bg="white"
        borderRadius="lg"
        boxShadow="md"
        p={6}
      >
        {children}
      </Container>
    </Flex>
  );
};

export default Layout;
