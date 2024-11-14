import { AddButton } from '@/shared/ui/AddButton';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddSectionButton() {
  const navigate = useNavigate();

  const handleAddSection = () => {
    navigate('/admin/add-section');
  };

  return (
    <AddButton
      text="Додати розділ"
      icon={<GoPlusCircle size="1.5rem" color="blue" />}
      onClick={handleAddSection}
    />
  );
}
