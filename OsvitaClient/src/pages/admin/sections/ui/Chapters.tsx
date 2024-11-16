import { Text, Flex } from '@chakra-ui/react';
import { AddChapterButton } from '@/features/sections';
import { ChaptersTable } from '@/entities/sections';
import { useQuery } from 'react-query';
import { getChapters } from '@/shared/api/chaptersApi';

const Chapters = () => {
  const { data: chapters, isLoading } = useQuery(['chapters'], getChapters);

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <>
      <Flex justifyContent="space-between" alignItems="center" mb={2}>
        <Text fontSize="2xl" fontWeight="medium">
          Розділи
        </Text>
        <AddChapterButton />
      </Flex>
      <ChaptersTable items={chapters} />
    </>
  );
};

export default Chapters;
