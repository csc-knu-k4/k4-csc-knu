import { Button, ButtonProps, Flex } from '@chakra-ui/react';
import { ReactElement } from 'react';

interface AddButtonProps extends ButtonProps {
  text: string;
  icon: ReactElement;
  onClick: () => void;
}

export function AddButton({ text, icon, onClick, ...rest }: AddButtonProps) {
  return (
    <Button
      onClick={onClick}
      p={2}
      fontSize="xl"
      variant="outline"
      borderRadius="0.5rem"
      border="1px solid black"
      {...rest}
    >
      <Flex alignItems="center" gap={2}>
        {icon}
        {text}
      </Flex>
    </Button>
  );
}
