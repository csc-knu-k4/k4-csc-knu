import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { Topic } from './types';
import { ActionButtons } from '@/shared/ui/ActionButtons';
import { useEffect, useState } from 'react';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { getChapterById } from '@/shared/api/chaptersApi';
import { deleteTopic } from '@/shared/api/topicsApi';
import { EditTopicModal } from '@/features/topics/EditTopicModal';
import { useNavigate } from 'react-router-dom';

interface TopicsTableRowProps {
  item: Topic;
}

export function TopicsTableRow({ item }: TopicsTableRowProps) {
  const [isEditOpen, setEditOpen] = useState(false);
  const [chapterTitle, setChapterTitle] = useState<string | null>(null);
  const queryClient = useQueryClient();

  const navigate = useNavigate();

  const handleViewMaterials = () => {
    navigate(`/admin/materials`, { state: { topicId: item.id } });
  };

  const { data: subject, isLoading } = useQuery(
    ['chapters', item.chapterId],
    () => getChapterById(item.chapterId),
    {
      enabled: !!item.chapterId,
    },
  );

  useEffect(() => {
    if (subject && subject.title) {
      setChapterTitle(subject.title);
    }
  }, [subject]);

  const deleteMutation = useMutation({
    mutationFn: deleteTopic,
    onSuccess: () => {
      queryClient.invalidateQueries(['topics']);
    },
  });

  const handleDelete = () => {
    deleteMutation.mutate(item.id);
  };

  return (
    <>
      <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
        <Table.Cell textAlign="start" whiteSpace="nowrap">
          {item.title}
        </Table.Cell>
        <Table.Cell w="full" textAlign="center">
          {isLoading ? 'Завантаження...' : chapterTitle || 'Не знайдено'}
        </Table.Cell>
        <Table.Cell>
          <ActionButtons
            actions={[
              { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: handleViewMaterials },
              { icon: <TbEdit />, ariaLabel: 'Edit', onClick: () => setEditOpen(true) },
              { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
            ]}
          />
        </Table.Cell>
      </Table.Row>

      <EditTopicModal
        isOpen={isEditOpen}
        onClose={() => setEditOpen(false)}
        initialTitle={item.title}
        topicId={item.id}
        chapterId={item.chapterId}
        orderPosition={item.orderPosition}
        materialIds={item.materialsIds}
      />
    </>
  );
}
