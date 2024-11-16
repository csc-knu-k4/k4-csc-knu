import { Button } from '@/components/ui/button';
import { createListCollection, Flex, Input, Text } from '@chakra-ui/react';
import { Field } from '@/components/ui/field';
import {
  SelectContent,
  SelectItem,
  SelectRoot,
  SelectTrigger,
  SelectValueText,
} from '@/components/ui/select';

const AddTopic = () => {
  return (
    <Flex maxWidth="30.25rem" flexDir="column">
      <Text mb="2rem" fontSize="2xl" fontWeight="medium">
        Додати тему
      </Text>
      <Field label="Назва" required mb={3} color="orange">
        <Input placeholder="Вкажіть назву" />
      </Field>
      <Field label="Розділ" required mb={3} color="orange">
        <SelectRoot collection={topics}>
          <SelectTrigger>
            <SelectValueText placeholder="Вкажіть розділ" />
          </SelectTrigger>
          <SelectContent>
            {topics.items.map((topic) => (
              <SelectItem item={topic} key={topic.value}>
                {topic.label}
              </SelectItem>
            ))}
          </SelectContent>
        </SelectRoot>
      </Field>
      <Button bgColor="orange">Додати</Button>
    </Flex>
  );
};

export default AddTopic;

const topics = createListCollection({
  items: [
    { label: 'Розділ 1', value: '1' },
    { label: 'Розділ 2', value: '2' },
    { label: 'Розділ 3', value: '3' },
  ],
});
