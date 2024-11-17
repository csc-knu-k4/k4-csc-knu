import { Image } from '@chakra-ui/react';

interface PreparationPictureProps {
  width?: string | number;
  height?: string | number;
}

const PreparationPicture = ({ width = '100%', height = 'auto' }: PreparationPictureProps) => (
  <Image
    src="./PreparationPicture.png"
    alt="PreparationPicture"
    w={width}
    h={height}
    objectFit="contain"
  />
);

export default PreparationPicture;
