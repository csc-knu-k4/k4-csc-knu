import { AddButton } from '@/shared/ui/AddButton';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddMaterialButton() {
  const navigate = useNavigate();

  const handleAddMaterial = () => {
    navigate('/admin/add-material');
  };

  return (
    <AddButton
      text="Додати матеріал"
      icon={<GoPlusCircle size="1.5rem" color="orange" />}
      onClick={handleAddMaterial}
    />
  );
}
