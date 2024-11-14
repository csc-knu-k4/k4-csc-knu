import { Container } from '@chakra-ui/react';
import WhyUsSection from './WhyUsSection';
import Header from './Header';
import Hero from './Hero';
import Preparation from './Preparation';
import FAQSection from './FAQSection';
import Footer from './Footer';

const Index = () => {
  return (
    <>
      <Container p="1rem 120px 0px">
        <Header />
        <Hero />
        <WhyUsSection />
      </Container>
      <Preparation />
      <Container p="1rem 120px 0px">
        <FAQSection />
      </Container>
      <Footer />
    </>
  );
};

export default Index;
