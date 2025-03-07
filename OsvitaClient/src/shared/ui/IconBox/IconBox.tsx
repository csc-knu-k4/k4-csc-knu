import { Flex, Text } from '@chakra-ui/react';
import { IconType } from 'react-icons';

interface IconBoxProps {
  icon: IconType;
  title: string;
  description?: string;
}

const IconBox = ({ icon: Icon, title, description }: IconBoxProps) => (
  <Flex
    w="17.6rem"
    h="12.2rem"
    border="0.06rem solid #5C6CFF"
    borderRadius="1.25rem"
    boxShadow="0rem 0rem 0.5rem 0rem #5C6CFF"
    flexDir="column"
  >
    <Flex
      bgColor="orange"
      justifyContent="center"
      alignItems="center"
      boxShadow="inset 0.13rem 0.13rem 0.94rem 0rem rgba(0, 0, 0, 0.25);"
      px={4}
      borderRadius="1.25rem 0rem 1.25rem 0rem"
      w="5rem"
      h="3.5rem"
    >
      <Icon size="2rem" color="white" />
    </Flex>
    <Flex
      flexDir="column"
      mx={5}
      flex={1}
      justifyContent={description ? 'flex-start' : 'center'}
      alignItems={description ? 'flex-start' : 'center'}
      mt={description ? 4 : -14}
    >
      <Text fontSize="2xl" fontWeight="bold" textAlign="center">
        {title}
      </Text>
      {description && (
        <Text mt={1} fontSize="sm" lineHeight="1rem">
          {description}
        </Text>
      )}
    </Flex>
  </Flex>
);

export default IconBox;
