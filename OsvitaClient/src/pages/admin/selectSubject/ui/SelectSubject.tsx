import IconBox from '@/shared/ui/IconBox/IconBox';
import { Flex, Text, Grid } from '@chakra-ui/react';
import { subjectsConfig } from './SelectSubjectConfig';
import { VscBook } from 'react-icons/vsc';

const SelectSubject = () => {
  return (
    <Flex justifyContent="center" mt={5} px={4}>
      <Grid
        templateColumns="repeat(auto-fit, minmax(17.6rem, 1fr))"
        gap={{ base: '1rem', md: '2rem', lg: '3.5rem' }}
        justifyContent="center"
        maxWidth="100%"
      >
        {subjectsConfig.map((subject, index) =>
          subject.isActive ? (
            <Flex
              key={index}
              justifyContent="center"
              alignItems="center"
              width="17.6rem"
              height="12.2rem"
              borderRadius="1rem"
            >
              <IconBox icon={VscBook} title={subject.title} />
            </Flex>
          ) : (
            <Flex
              key={index}
              justifyContent="center"
              alignItems="center"
              width="17.6rem"
              height="12.2rem"
              bgColor="gray.300"
              borderRadius="1rem"
            >
              <Text fontSize="xl" color="white" textAlign="center">
                {subject.title}
              </Text>
            </Flex>
          ),
        )}
      </Grid>
    </Flex>
  );
};

export default SelectSubject;
