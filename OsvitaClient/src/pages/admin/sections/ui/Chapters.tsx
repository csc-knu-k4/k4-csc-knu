import { Text, Flex, Box } from '@chakra-ui/react';
import { AddChapterButton } from '@/features/sections';
import { ChaptersTable } from '@/entities/sections';
import { useQuery } from 'react-query';
import { getChapters, getChaptersBySubject } from '@/shared/api/chaptersApi';
import { useLocation } from 'react-router-dom';

const Chapters = () => {
  const location = useLocation();
  const subjectId = location.state?.subjectId;

  const {
    data: chapters,
    isLoading,
    isError,
  } = useQuery(
    ['chapters', subjectId],
    () => (subjectId ? getChaptersBySubject(subjectId) : getChapters()),
    { keepPreviousData: true },
  );

  const header = (
    <Flex
      position="sticky"
      top="0"
      bg="white"
      zIndex="3"
      p={4}
      justifyContent="space-between"
      alignItems="center"
      borderBottom="1px solid #ddd"
      mb={2}
    >
      <Text fontSize="2xl" fontWeight="medium">
        {subjectId ? `Розділи для предмету #${subjectId}` : 'Розділи'}
      </Text>
      <AddChapterButton />
    </Flex>
  );

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  if (isError) {
    return (
      <>
        {header}
        <Text color="red.500">Помилка завантаження даних.</Text>
      </>
    );
  }

  return (
    <Box height="calc(100vh - 180px)" display="flex" flexDirection="column">
      {header}
      <Box flex="1" overflowY="auto" p={4}>
        {chapters && chapters.length > 0 ? (
          <ChaptersTable items={chapters} />
        ) : (
          <Text>Дані відсутні.</Text>
        )}
      </Box>
    </Box>
  );
};

export default Chapters;
