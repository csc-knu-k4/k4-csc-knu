import IconBox from '@/shared/ui/IconBox/IconBox';
import { Flex, Text } from '@chakra-ui/react';
import { subjectsConfig } from './SelectSubjectConfig';
import { VscBook } from 'react-icons/vsc';

const SelectSubject = () => {
  return (
    <>
      <Flex
        flexDir="row"
        flexWrap="wrap"
        justifyContent="center"
        alignItems="center"
        gap="3.5rem"
        mt={5}
      >
        {subjectsConfig.map((subject, index) =>
          subject.isActive ? (
            <IconBox key={index} icon={VscBook} title={subject.title} />
          ) : (
            <Flex
              key={index}
              justifyContent="center"
              alignItems="center"
              w="17.6rem"
              h="12.2rem"
              bgColor="gray.300"
              borderRadius="1rem"
            >
              <Text fontSize="xl" color="white">
                {subject.title}
              </Text>
            </Flex>
          ),
        )}
      </Flex>
    </>
  );
};

export default SelectSubject;
