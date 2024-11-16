import { Button } from '@/components/ui/button';
import FooterPicture from '@/shared/assets/FooterPicture';
import { Flex, Text, Highlight, Input, chakra, useRecipe, Container } from '@chakra-ui/react';
import AutoResize from 'react-textarea-autosize';

const StyledAutoResize = chakra(AutoResize);
const Footer = () => {
  const recipe = useRecipe({ key: 'textarea' });
  const styles = recipe({ size: 'sm' });

  return (
    <Flex bgColor="orange.100" mt="130px">
      <Container px="120px">
        <Flex flexDir="row" justifyContent="space-between" alignItems="center">
          <Flex flexDir="column" justifyContent="flex-start">
            <Text fontSize="2xl" fontWeight="bold">
              <Highlight query="Зв’яжіться" styles={{ color: 'orange' }}>
                Зв’яжіться
              </Highlight>{' '}
              з нами
            </Text>
            <Text fontSize="xs">У вас є запитання чи пропозиції?</Text>
            <Text fontSize="xs">Напишіть нам, і ми з радістю відповімо на всі ваші запитання!</Text>
            <Flex flexDir="row" gap={6} mt={4}>
              <Input bgColor="white" placeholder="Ім’я" />
              <Input bgColor="white" placeholder="Email" />
            </Flex>
            <StyledAutoResize
              bgColor="white"
              placeholder="Повідомлення"
              mt={3}
              minH="100px"
              resize="none"
              overflow="hidden"
              lineHeight="inherit"
              css={styles}
            ></StyledAutoResize>
            <Button maxW="17.5rem" mt={4} bgColor="orange" borderRadius="1rem">
              Відправити повідомлення
            </Button>
          </Flex>
          <FooterPicture />
        </Flex>
      </Container>
    </Flex>
  );
};

export default Footer;
