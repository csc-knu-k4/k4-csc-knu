import { useEffect, useState } from 'react';
import { Table } from '@chakra-ui/react';
import { IoEyeOutline } from 'react-icons/io5';
import { TbEdit } from 'react-icons/tb';
import { MdDeleteOutline } from 'react-icons/md';
import { useMutation, useQuery } from 'react-query';
import { deleteChapter } from '@/shared/api/chaptersApi';
import { getSubjectById } from '@/shared/api/subjectsApi';
import { ActionButtons } from '@/shared/ui/ActionButtons';

interface Section {
  id: number;
  title: string;
  subjectId: number;
  orderPosition: number;
  topicsIds: number[];
}

interface SectionsTableRowProps {
  item: Section;
}

export function SectionsTableRow({ item }: SectionsTableRowProps) {
  const [subjectTitle, setSubjectTitle] = useState<string | null>(null);

  const { data: subject, isLoading } = useQuery(
    ['subject', item.subjectId],
    () => getSubjectById(item.subjectId),
    {
      enabled: !!item.subjectId,
    },
  );

  useEffect(() => {
    if (subject && subject.title) {
      setSubjectTitle(subject.title);
    }
  }, [subject]);

  const deleteMutation = useMutation({
    mutationFn: deleteChapter,
    onSuccess: () => {
      console.log(`Chapter with ID ${item.id} deleted.`);
    },
  });

  const handleDelete = () => {
    deleteMutation.mutate(item.id);
  };

  return (
    <Table.Row bgColor={item.id % 2 === 0 ? 'white' : 'orange.100'}>
      <Table.Cell textAlign="start" whiteSpace="nowrap">
        {item.title}
      </Table.Cell>
      <Table.Cell w="full" textAlign="center">
        {isLoading ? 'Завантаження...' : subjectTitle || 'Не знайдено'}
      </Table.Cell>
      <Table.Cell>
        <ActionButtons
          actions={[
            { icon: <IoEyeOutline />, ariaLabel: 'Watch', onClick: () => console.log('View') },
            { icon: <TbEdit />, ariaLabel: 'Edit', onClick: () => console.log('Edit') },
            { icon: <MdDeleteOutline />, ariaLabel: 'Delete', onClick: handleDelete },
          ]}
        />
      </Table.Cell>
    </Table.Row>
  );
}
