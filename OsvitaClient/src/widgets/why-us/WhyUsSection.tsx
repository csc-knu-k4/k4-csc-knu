import IconBox from '@/shared/ui/IconBox/IconBox';
import { Flex, Text, Highlight } from '@chakra-ui/react';
import { VscBook } from 'react-icons/vsc';
import { boxData } from './why-usConfig';

const WhyUsSection = () => {
  return (
    <Flex flexDir="column" justifyContent="center" alignItems="center" mt="130px">
      <Text fontSize="2xl" fontWeight="bold">
        Чому ми та які наші{' '}
        <Highlight query="переваги?" styles={{ color: 'orange' }}>
          переваги?
        </Highlight>
      </Text>
      <Text maxW="42rem" mt={3} textAlign="center">
        Ми розуміємо, що підготовка до НМТ може бути складною. Наша платформа зробить цей процес
        захопливим та ефективним, щоб ти міг зосередитись на результаті!
      </Text>
      <Flex
        flexDir="row"
        flexWrap="wrap"
        justifyContent="center"
        alignItems="center"
        gap={6}
        mt={9}
      >
        {boxData.map((box, index) => (
          <IconBox key={index} icon={VscBook} title={box.title} description={box.description} />
        ))}
      </Flex>
    </Flex>
  );
};

export default WhyUsSection;
