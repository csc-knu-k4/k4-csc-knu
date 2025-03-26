import { useParams, useLocation, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';

import TestRenderer from './TestRenderer';
import SubjectTaskLayout from '@/app/layouts/SubjectTaskLayout/SubjectTaskLayout';
import { getAssignmentsSets } from '@/shared/api/assingnmentsSets';
import { goToNextMaterialOrTopic } from '@/shared/utils/navigationUtils';

const SubjectTaskTest = () => {
  const { testId } = useParams<{ testId: string }>();
  const navigate = useNavigate();
  const location = useLocation();
  const topicId = location.state?.topicId;
  const materialId = location.state?.materialId;
  const [nextTopic, setNextTopic] = useState<any | null>(null);
  const [title, setTitle] = useState('Завантаження...');

  useEffect(() => {
    if (testId) {
      getAssignmentsSets(+testId).then((data) => {
        setTitle(`Тест до теми №${data.objectId}`);
      });
    }
  }, [testId]);

  const handleBackToMaterial = () => {
    if (materialId) {
      navigate(`/course/subject-material/${materialId}`, {
        state: { topicId },
      });
    } else {
      navigate(-1);
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

  if (!testId) return <div>Тест не знайдено</div>;

  return (
    <SubjectTaskLayout
      title={title}
      showTest={true}
      onToggleMode={handleBackToMaterial}
      onNextTopic={() => goToNextMaterialOrTopic(topicId, Number(materialId), navigate)}
      isNextDisabled={!nextTopic}
      nextTopicTitle={nextTopic?.title || ''}
    >
      <TestRenderer testId={+testId} />
    </SubjectTaskLayout>
  );
};

export default SubjectTaskTest;
