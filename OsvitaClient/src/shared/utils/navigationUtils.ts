import { NavigateFunction } from 'react-router-dom';

export const goToNextMaterialOrTopic = (
  topicId: number,
  materialId: number,
  navigate: NavigateFunction,
) => {
  const subjectsData = JSON.parse(localStorage.getItem('subjectsData') || '[]');
  if (!Array.isArray(subjectsData) || !topicId || !materialId) return;

  let currentTopic: any = null;
  const allTopics: any[] = [];

  subjectsData.forEach((subject: any) => {
    subject.chapters.forEach((chapter: any) => {
      chapter.topics.forEach((topic: any) => {
        allTopics.push(topic);
        if (topic.id === topicId) currentTopic = topic;
      });
    });
  });

  if (!currentTopic || !currentTopic.materials) return;

  const materialIndex = currentTopic.materials.findIndex((m: any) => m.id === Number(materialId));

  const nextMaterial = currentTopic.materials[materialIndex + 1];

  if (nextMaterial) {
    navigate(`/course/subject-material/${nextMaterial.id}`, {
      state: {
        topicId: currentTopic.id,
        materialId: nextMaterial.id,
      },
    });
  } else {
    const currentTopicIndex = allTopics.findIndex((t: any) => t.id === topicId);
    const nextTopic = allTopics[currentTopicIndex + 1];

    if (nextTopic && nextTopic.materials?.length > 0) {
      navigate(`/course/subject-material/${nextTopic.materials[0].id}`, {
        state: {
          topicId: nextTopic.id,
          materialId: nextTopic.materials[0].id,
        },
      });
    } else {
      alert('Це був останній матеріал у курсі.');
    }
  }
};
