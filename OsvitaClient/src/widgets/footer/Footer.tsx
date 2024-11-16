import FooterPicture from '@/shared/assets/FooterPicture';
import { Flex, Container } from '@chakra-ui/react';
import { ContactForm } from './ContactForm';
import { ContactInfo } from './ContactInfo';

const Footer = () => (
  <Flex bgColor="orange.100" mt="130px">
    <Container px="120px">
      <Flex flexDir="row" justifyContent="space-between" alignItems="center">
        <Flex flexDir="column" justifyContent="center" alignItems="flex-start">
          <ContactInfo />
          <ContactForm />
        </Flex>
        <FooterPicture />
      </Flex>
    </Container>
  </Flex>
);

export default Footer;
