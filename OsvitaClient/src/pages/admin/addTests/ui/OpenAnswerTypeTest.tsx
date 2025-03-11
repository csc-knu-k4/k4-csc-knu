import { Field } from '@/components/ui/field';
import { Button, Input } from '@chakra-ui/react';

const OpenAnswerTypeTest = () => {
  return (
    <>
      <Field label="Запитання" required mb={3} color="orange">
        <Input width="30.5rem" placeholder="Введіть запитання" />
      </Field>
      <Field label="Відповідь" required mb={3} color="orange">
        <Input width="30.5rem" placeholder="Введіть відповідь" />
      </Field>
      <Button colorPalette="orange" size="sm">
        Зберегти
      </Button>
    </>
  );
};

export default OpenAnswerTypeTest;
