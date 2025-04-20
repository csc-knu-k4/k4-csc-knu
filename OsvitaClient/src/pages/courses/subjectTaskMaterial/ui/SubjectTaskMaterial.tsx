import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getMaterialContentById } from '@/shared/api/materialsApi';
import { Text, Spinner, Box, Image } from '@chakra-ui/react';
import { addAssignmentsSets, getAssignmentsSets } from '@/shared/api/assingnmentsSets';
import { useLocation } from 'react-router-dom';
import SubjectTaskLayout from '@/app/layouts/SubjectTaskLayout/SubjectTaskLayout';
import { goToNextMaterialOrTopic } from '@/shared/utils/navigationUtils';
import { toaster } from '@/components/ui/toaster';

interface MaterialContent {
  id: number;
  title: string;
  contentBlockModelType: number;
  orderPosition: number;
  materialId: number;
  value: string;
}

const SubjectTaskMaterial = () => {
  const location = useLocation();
  const topicId = location.state?.topicId;
  const { materialId } = useParams<{ materialId: string }>();
  const [content, setContent] = useState<MaterialContent[]>([]);

  const [loading, setLoading] = useState(true);
  const [isTest, setIsTest] = useState(false);
  const [nextTopic, setNextTopic] = useState<any | null>(null);

  const navigate = useNavigate();

  const handleStartTest = async () => {
    if (!topicId) return;
    setLoading(true);
    try {
      const testId = await addAssignmentsSets({
        id: 0,
        objectModelType: 1,
        objectId: topicId,
        assignments: [],
      });

      const testData = await getAssignmentsSets(testId);

      if (!testData.assignments || testData.assignments.length === 0) {
        toaster.create({
          title: `До цієї теми ще немає тесту 😔`,
          type: 'warning',
        });
        return;
      }

      navigate(`/course/subject-test/${testId}`);
    } catch (err) {
      toaster.create({
        title: `Не вдалося створити тест: ${err}`,
        type: 'error',
      });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const subjectsData = JSON.parse(localStorage.getItem('subjectsData') || '[]');
    if (!Array.isArray(subjectsData) || !topicId) return;

    const allTopics: any[] = [];

    subjectsData.forEach((subject: any) => {
      subject.chapters.forEach((chapter: any) => {
        chapter.topics.forEach((topic: any) => {
          allTopics.push({
            ...topic,
            subjectId: subject.id,
            chapterId: chapter.id,
          });
        });
      });
    });

    const currentIndex = allTopics.findIndex((t) => t.id === topicId);
    const nt = allTopics[currentIndex + 1];
    if (nt) setNextTopic(nt);
  }, [topicId]);

  useEffect(() => {
    if (materialId) {
      getMaterialContentById(Number(materialId))
        .then((data) => setContent(data))
        .catch((error) => {
          toaster.create({
            title: `Помилка завантаження матеріалу: ${error}`,
            type: 'error',
          });
        })
        .finally(() => setLoading(false));
    }
  }, [materialId]);

  return (
    <SubjectTaskLayout
      title={content[0]?.title || ''}
      showTest={isTest}
      onToggleMode={isTest ? () => setIsTest(false) : handleStartTest}
      onNextTopic={() => goToNextMaterialOrTopic(topicId, Number(materialId), navigate)}
      isNextDisabled={!nextTopic}
      nextTopicTitle={nextTopic?.title || ''}
    >
      <Box height="calc(100vh - 400px)" overflowY="auto" px={4} py={2}>
        {loading ? (
          <Spinner size="xl" />
        ) : content.length > 0 ? (
          content
            .sort((a: MaterialContent, b: MaterialContent) => a.orderPosition - b.orderPosition)
            .map((block: MaterialContent) =>
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
                  src={`${import.meta.env.VITE_IMG_URL}${block.value}`}
                  alt={block.title}
                  borderRadius="lg"
                  maxW="100%"
                  maxH="300px"
                  objectFit="contain"
                  mb={4}
                  boxShadow="md"
                />
              ),
            )
        ) : (
          <Text>Немає контенту</Text>
        )}
      </Box>
    </SubjectTaskLayout>
  );
};

export default SubjectTaskMaterial;
