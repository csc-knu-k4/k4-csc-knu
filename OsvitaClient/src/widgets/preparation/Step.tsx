import { Flex, Text, Circle, Box } from '@chakra-ui/react';

interface StepProps {
  number: number;
  title: string;
  description: string;
  isLast?: boolean;
}

export const Step = ({ number, title, description, isLast }: StepProps) => (
  <Flex align="flex-start" position="relative">
    <Flex direction="column" align="center" mr={4} position="relative">
      <Circle size="3.15rem" bg="orange" color="white" fontWeight="bold" fontSize="lg" zIndex="1">
        {number}
      </Circle>
      {!isLast && (
        <Box
          position="absolute"
          top="2rem"
          w="2px"
          h={{ base: 'calc(100% + 7rem)', sm: 'calc(100% + 5rem)' }}
          bg="orange"
        />
      )}
    </Flex>
    <Box pl={4}>
      <Text fontWeight="bold" fontSize="xl" mb={2}>
        {title}
      </Text>
      <Text fontSize="sm">{description}</Text>
    </Box>
  </Flex>
);
