import { Input } from '@chakra-ui/react';
import { SearchIcon } from './SearchIcon';
import { InputGroup } from '@/components/ui/input-group';

export function SearchInput() {
  return (
    <InputGroup flex="1" endElement={<SearchIcon />}>
      <Input
        color="#5C6CFF"
        placeholder="Пошук"
        w="20rem"
        h="2.25rem"
        borderRadius="6.25rem"
        border="0.03rem solid #5C6CFF"
        _placeholder={{ color: 'inherit' }}
        fontSize="md"
      />
    </InputGroup>
  );
}
