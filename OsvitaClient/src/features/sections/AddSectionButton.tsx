import { Button } from '@/components/ui/button';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddSectionButton() {
  const navigate = useNavigate();

  const handleAddSection = () => {
    navigate('/add-section');
  };

  return (
    <Button
      onClick={handleAddSection}
      p={2}
      fontSize="xl"
      variant="outline"
      borderRadius="0.5rem"
      border="1px solid black"
    >
      <GoPlusCircle size="1.5rem" color="#5C6CFF" /> Додати розділ
    </Button>
  );
}
