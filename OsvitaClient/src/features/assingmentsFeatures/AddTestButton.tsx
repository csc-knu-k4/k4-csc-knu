import { AddButton } from '@/shared/ui/AddButton';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddTestButton() {
  const navigate = useNavigate();

  const handleAddTest = () => {
    navigate('/admin/add-tests');
  };

  return (
    <AddButton
      text="Додати тест"
      icon={<GoPlusCircle size="1.5rem" color="orange" />}
      onClick={handleAddTest}
    />
  );
}
