import { Button } from '@/components/ui/button';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddTopicButton() {
  const navigate = useNavigate();

  const handleAddTopic = () => {
    navigate('/add-topic');
  };

  return (
    <Button
      onClick={handleAddTopic}
      p={2}
      fontSize="xl"
      variant="outline"
      borderRadius="0.5rem"
      border="1px solid black"
    >
      <GoPlusCircle size="1.5rem" color="#5C6CFF" /> Додати тему
    </Button>
  );
}
