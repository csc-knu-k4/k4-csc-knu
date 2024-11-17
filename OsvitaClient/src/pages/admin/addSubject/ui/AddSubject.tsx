import { useState } from 'react';
import { useMutation, useQueryClient } from 'react-query';
import { Button } from '@/components/ui/button';
import { Flex, Input, Text } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import { addSubject } from '@/shared/api/subjectsApi';

const AddSubject = () => {
  const [title, setTitle] = useState('');
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: addSubject,
    onSuccess: () => {
      queryClient.invalidateQueries(['subjects']);
      setTitle('');
    },
    onError: (error) => {
      console.error('Error adding subject:', error);
    },
  });

  const handleSubmit = () => {
    if (!title.trim()) {
      alert('Будь ласка, вкажіть назву предмету!');
      return;
    }

    mutation.mutate({
      title: title.trim(),
      chaptersIds: [],
    });
  };

  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати предмет
      </Text>
      <Field label="Назва" required mb={3} color="orange">
        <Input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          _placeholder={{ color: 'inherit' }}
          placeholder="Вкажіть назву"
          color="orange.placeholder"
          borderColor="orange"
        />
      </Field>
      <Button bgColor="orange" onClick={handleSubmit} loading={mutation.isLoading}>
        Додати
      </Button>
    </Flex>
  );
};

export default AddSubject;
