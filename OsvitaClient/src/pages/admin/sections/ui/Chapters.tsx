import { Text, Flex } from '@chakra-ui/react';
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
    <Flex justifyContent="space-between" alignItems="center" mb={2}>
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
    <>
      {header}
      {chapters && chapters.length > 0 ? (
        <ChaptersTable items={chapters} />
      ) : (
        <Text>Дані відсутні.</Text>
      )}
    </>
  );
};

export default Chapters;
