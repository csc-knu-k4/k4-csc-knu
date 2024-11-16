import { Image } from '@chakra-ui/react';

interface IndexPictureHeroProps {
  width?: string | number;
  height?: string | number;
}

const IndexPictureHero = ({ width = '100%', height = 'auto' }: IndexPictureHeroProps) => (
  <Image src="./IndexPictureHero.png" alt="heroPicture" w={width} h={height} objectFit="contain" />
);

export default IndexPictureHero;
