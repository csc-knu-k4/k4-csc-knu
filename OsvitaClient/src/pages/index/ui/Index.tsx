import { Container } from '@chakra-ui/react';
import { Header } from '@/entities/novigation';
import { Hero } from '@/widgets/hero';
import { WhyUsSection } from '@/widgets/why-us';
import { FAQSection } from '@/widgets/faq';
import { Footer } from '@/widgets/footer';
import { Preparation } from '@/widgets/preparation';

const Index = () => {
  return (
    <>
      <Container maxW="1232px" p="1rem 1rem 0px">
        <Header />
        <Hero />
        <WhyUsSection />
      </Container>
      <Preparation />
      <Container maxW="1232px" px="1rem">
        <FAQSection />
      </Container>
      <Footer />
    </>
  );
};

export default Index;
