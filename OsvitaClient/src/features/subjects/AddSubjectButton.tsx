import { AddButton } from '@/shared/ui/AddButton';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddSubjectButton() {
  const navigate = useNavigate();

  const handleAddSubject = () => {
    navigate('/admin/add-subject');
  };

  return (
    <AddButton
      text="Додати предмет"
      icon={<GoPlusCircle size="1.5rem" color="#5C6CFF" />}
      onClick={handleAddSubject}
    />
  );
}
