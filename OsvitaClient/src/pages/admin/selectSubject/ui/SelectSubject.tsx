import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import IconBox from '@/shared/ui/IconBox/IconBox';
import { Flex, Grid, Spinner } from '@chakra-ui/react';
import { getSubjects, Subject } from '@/shared/api/subjectsApi';
import { VscBook } from 'react-icons/vsc';

const SelectSubject = () => {
  const navigate = useNavigate();
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getSubjects()
      .then((data) => setSubjects(data))
      .catch((error) => console.error('Помилка завантаження предметів:', error))
      .finally(() => setLoading(false));
  }, []);

  return (
    <Flex justifyContent="center" mt={5} px={4}>
      {loading ? (
        <Spinner size="xl" />
      ) : (
        <Grid
          templateColumns="repeat(auto-fit, minmax(17.6rem, 1fr))"
          gap={{ base: '1rem', md: '2rem', lg: '3.5rem' }}
          justifyContent="center"
          maxWidth="100%"
        >
          {subjects.map((subject) => (
            <Flex
              key={subject.id}
              justifyContent="center"
              alignItems="center"
              width="17.6rem"
              height="12.2rem"
              borderRadius="1rem"
              bg="white"
              _hover={{ bg: 'orange.200', cursor: 'pointer' }}
              onClick={() => navigate(`/course/select-course-list/${subject.id}`)}
            >
              <IconBox icon={VscBook} title={subject.title} />
            </Flex>
          ))}
        </Grid>
      )}
    </Flex>
  );
};

export default SelectSubject;
