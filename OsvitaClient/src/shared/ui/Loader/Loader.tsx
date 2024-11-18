import { Flex, Spinner } from '@chakra-ui/react';

const Loader = () => (
  <Flex h="calc(100dvh - 12rem)" overflow="hidden" justifyContent="center" alignItems="center">
    <Spinner size="lg" color="orange.400" />
  </Flex>
);

export default Loader;
