import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getTopicById } from '@/shared/api/topicsApi';
import { getMaterialContentById } from '@/shared/api/materialsApi';
import { Flex, Stack, Text, Image, Spinner, Box, IconButton } from '@chakra-ui/react';
import { IoArrowBack } from 'react-icons/io5';

type ContentBlock = {
  id: number;
  title: string;
  contentBlockModelType: number; // 0 - текст, 1 - зображення
  orderPosition: number;
  materialId: number;
  value: string;
};

const SubjectTaskTopic = () => {
  const { topicId } = useParams<{ topicId: string }>();
  const [loading, setLoading] = useState(true);
  const [topicTitle, setTopicTitle] = useState('');
  const [materials, setMaterials] = useState<
    { id: number; title: string; content: ContentBlock[] }[]
  >([]);
  const navigate = useNavigate();

  useEffect(() => {
    if (!topicId) return;

    const fetchData = async () => {
      try {
        const topic = await getTopicById(Number(topicId));
        setTopicTitle(topic.title);

        const materialPromises = topic.materials.map(async (material: any) => {
          const content = await getMaterialContentById(material.id);
          return {
            id: material.id,
            title: material.title,
            content: content.sort(
              (a: ContentBlock, b: ContentBlock) => a.orderPosition - b.orderPosition,
            ),
          };
        });

        const allMaterials = await Promise.all(materialPromises);
        setMaterials(allMaterials);
      } catch (err) {
        console.error('❌ Помилка при завантаженні матеріалів:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [topicId]);

  if (loading) {
    return (
      <Flex justify="center" align="center" h="100%">
        <Spinner size="lg" />
      </Flex>
    );
  }

  return (
    <Flex direction="column" h="calc(100vh - 180px)">
      <Box position="sticky" top={0} zIndex={10} bg="white" px={{ base: 4, md: 6 }} py={3}>
        <Flex align="center" gap={4}>
          <IconButton
            aria-label="Назад"
            onClick={() => navigate(-1)}
            size="sm"
            variant="outline"
            colorScheme="orange"
          >
            <IoArrowBack />
          </IconButton>
          <Text fontSize={{ base: 'xl', md: '2xl' }} fontWeight="bold" color="orange">
            {topicTitle}
          </Text>
        </Flex>
      </Box>

      <Box overflowY="auto" px={{ base: 4, md: 6 }} py={4} flex="1">
        <Flex direction="column" w="full" maxW="container.lg" mx="auto">
          <Stack gap={8}>
            {materials.map((material) => (
              <Flex
                key={material.id}
                direction="column"
                bg="gray.50"
                borderRadius="1rem"
                p={{ base: 3, md: 5 }}
                w="full"
              >
                <Text fontSize={{ base: 'lg', md: 'xl' }} fontWeight="semibold" mb={2}>
                  📖 {material.title}
                </Text>

                <Stack gap={4}>
                  {material.content.map((block) =>
                    block.contentBlockModelType === 0 ? (
                      <Box
                        key={block.id}
                        mb={4}
                        className="quill-content"
                        dangerouslySetInnerHTML={{ __html: block.value }}
                      />
                    ) : (
                      <Image
                        key={block.id}
                        src={`${'http://localhost:5134/'}${block.value}`}
                        alt={block.title}
                        borderRadius="lg"
                        maxW="100%"
                        maxH="300px"
                        objectFit="contain"
                        mb={4}
                      />
                    ),
                  )}
                </Stack>
              </Flex>
            ))}
          </Stack>
        </Flex>
      </Box>
    </Flex>
  );
};

export default SubjectTaskTopic;
