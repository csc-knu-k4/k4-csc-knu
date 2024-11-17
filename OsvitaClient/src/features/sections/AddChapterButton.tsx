import { AddButton } from '@/shared/ui/AddButton';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddChapterButton() {
  const navigate = useNavigate();

  const handleAddChapter = () => {
    navigate('/admin/add-chapter');
  };

  return (
    <AddButton
      text="Додати розділ"
      icon={<GoPlusCircle size="1.5rem" color="orange" />}
      onClick={handleAddChapter}
    />
  );
}
