import { IconButton, Flex } from '@chakra-ui/react';
import { ReactElement } from 'react';

interface ActionButtonProps {
  icon: ReactElement;
  ariaLabel: string;
  onClick?: () => void;
  bgColor?: string;
  iconColor?: string;
}

interface ActionButtonsProps {
  actions: ActionButtonProps[];
}

export function ActionButtons({ actions }: ActionButtonsProps) {
  return (
    <Flex gap={5} justifyContent="flex-end">
      {actions.map((action, index) => (
        <IconButton
          key={index}
          aria-label={action.ariaLabel}
          onClick={action.onClick}
          bgColor={action.bgColor || 'orange.200'}
          _hover={{ bg: action.bgColor || 'orange.hover' }}
          color={action.iconColor || 'orange'}
        >
          {action.icon}
        </IconButton>
      ))}
    </Flex>
  );
}
