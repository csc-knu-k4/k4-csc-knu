import { AddButton } from '@/shared/ui/AddButton';
import { GoPlusCircle } from 'react-icons/go';
import { useNavigate } from 'react-router-dom';

export function AddTopicButton() {
  const navigate = useNavigate();

  const handleAddTopic = () => {
    navigate('/add-topic');
  };

  return (
    <AddButton
      text="Додати тему"
      icon={<GoPlusCircle size="1.5rem" color="blue" />}
      onClick={handleAddTopic}
    />
  );
}
