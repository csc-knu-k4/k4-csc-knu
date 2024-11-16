import PreparationPicture from '@/shared/assets/PreparationPicture';
import { Flex, Text, Highlight, VStack, Container } from '@chakra-ui/react';
import { Step } from './Step';
import { steps } from './stepsConfig';

const Preparation = () => (
  <Flex bgColor="orange.100" mt="130px">
    <Container maxW="1232px" px="1rem">
      <Flex
        justifyContent="center"
        alignItems="center"
        flexDir={{ base: 'column', lg: 'row' }}
        bgColor="orange.100"
      >
        <PreparationPicture />
        <Flex flexDir="column">
          <Text fontSize="2xl" fontWeight="bold" mt={10} ml={5}>
            Як проходить{' '}
            <Highlight query="підготовка ?" styles={{ color: 'orange' }}>
              підготовка ?
            </Highlight>
          </Text>
          <VStack align="stretch" gap={5} position="relative" p={5} maxW="600px" mx="auto">
            {steps.map((step, index) => (
              <Step key={step.number} {...step} isLast={index === steps.length - 1} />
            ))}
          </VStack>
        </Flex>
      </Flex>
    </Container>
  </Flex>
);

export default Preparation;
