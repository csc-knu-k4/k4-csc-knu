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
