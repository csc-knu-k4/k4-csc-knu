import { Grid, GridItem } from '@chakra-ui/react';
import { Toolbar } from '@/features/Toolbar';
import { ContentArea } from '../ContentArea';

export default function BaseLayout() {
  return (
    <Grid
      templateAreas={`
        "toolbar"
        "content"
      `}
      templateColumns="1fr"
      templateRows="auto 1fr"
      gap="1.5rem"
      h="100vh"
      padding={{ base: '0.5rem 0.75rem', md: '1.25rem 1.5rem' }}
      overflow="hidden"
    >
      <GridItem area="toolbar" w="100%">
        <Toolbar />
      </GridItem>

      <GridItem area="content" w="100%">
        <ContentArea />
      </GridItem>
    </Grid>
  );
}
