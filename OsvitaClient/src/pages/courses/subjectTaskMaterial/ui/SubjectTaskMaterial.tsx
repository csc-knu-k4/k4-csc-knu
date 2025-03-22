import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getMaterialContentById } from '@/shared/api/materialsApi';
import { Text, Spinner } from '@chakra-ui/react';
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
  const [content, setContent] = useState<MaterialContent | null>(null);
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
        .then((data) => setContent(data[0]))
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
      title={content?.title || ''}
      showTest={isTest}
      onToggleMode={isTest ? () => setIsTest(false) : handleStartTest}
      onNextTopic={() => goToNextMaterialOrTopic(topicId, Number(materialId), navigate)}
      isNextDisabled={!nextTopic}
      nextTopicTitle={nextTopic?.title || ''}
    >
      {loading ? <Spinner size="xl" /> : <Text>{content?.value || 'Немає контенту'}</Text>}
    </SubjectTaskLayout>
  );
};

export default SubjectTaskMaterial;
