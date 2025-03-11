import { Field } from '@/components/ui/field';
import { Checkbox, Flex, Input, Text, Button } from '@chakra-ui/react';

const MAX_OPTIONS = 5;
const MIN_OPTIONS = 2;

const OPTION_LABELS = ['А', 'Б', 'В', 'Г', 'Д'];

type SingleAnswerTypeTestProps = {
  options: string[];
  handleOptionChange: (index: number, value: string) => void;
  handleRemoveOption: (index: number) => void;
  handleAddOption: () => void;
};

const SingleAnswerTypeTest: React.FC<SingleAnswerTypeTestProps> = ({
  options,
  handleOptionChange,
  handleRemoveOption,
  handleAddOption,
}) => (
  <>
    <Field label="Запитання" required mb={3} color="orange">
      <Input width="30.5rem" placeholder="Введіть запитання" />
    </Field>
    <Text color="orange" fontSize="sm" mb={2}>
      Варіанти відповіді
    </Text>
    {options.map((option, index) => (
      <Flex key={index} flexDir="row" alignItems="center" gap={3} mb={3}>
        <Text fontWeight="bold" color="orange" w="0.75rem">
          {OPTION_LABELS[index]}
        </Text>
        <Input
          width="30.5rem"
          placeholder={`Варіант ${OPTION_LABELS[index]}`}
          value={option}
          onChange={(e) => handleOptionChange(index, e.target.value)}
        />
        <Checkbox.Root colorPalette="orange">
          <Checkbox.HiddenInput />
          <Checkbox.Control>
            <Checkbox.Indicator />
          </Checkbox.Control>
        </Checkbox.Root>
        {options.length > MIN_OPTIONS && (
          <Button size="sm" colorPalette="red" onClick={() => handleRemoveOption(index)}>
            Видалити
          </Button>
        )}
      </Flex>
    ))}
    {options.length < MAX_OPTIONS && (
      <Button size="sm" colorPalette="orange" onClick={handleAddOption}>
        Додати варіант
      </Button>
    )}

    <Button colorPalette="orange" size="sm" ml={3}>
      Зберегти
    </Button>
  </>
);

export default SingleAnswerTypeTest;
