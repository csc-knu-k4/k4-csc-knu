import {
  AccordionItem,
  AccordionItemContent,
  AccordionItemTrigger,
  AccordionRoot,
} from '@/components/ui/accordion';

interface FAQItem {
  value: string;
  title: string;
  text: string;
}

interface AccordionProps {
  items: FAQItem[];
}

export const Accordion = ({ items }: AccordionProps) => (
  <AccordionRoot multiple mt={8} maxW="52.5rem">
    {items.map((item) => (
      <AccordionItem borderColor="blue" key={item.value} value={item.value}>
        <AccordionItemTrigger>{item.title}</AccordionItemTrigger>
        <AccordionItemContent fontSize="md">{item.text}</AccordionItemContent>
      </AccordionItem>
    ))}
  </AccordionRoot>
);
