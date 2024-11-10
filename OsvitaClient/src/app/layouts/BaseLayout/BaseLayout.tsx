import { Grid, GridItem } from '@chakra-ui/react';
import { Sidebar } from '@/shared/ui/Sidebar';
import { Toolbar } from '@/features/Toolbar';
import { ContentArea } from './ContentArea';

export default function BaseLayout() {
  return (
    <Grid
      templateAreas={`"sidebar toolbar" "sidebar content"`}
      templateColumns="16rem 1fr"
      templateRows="auto 1fr"
      gap="1.5rem"
      h="100vh"
    >
      <GridItem area="sidebar">
        <Sidebar />
      </GridItem>

      <GridItem area="toolbar">
        <Toolbar />
      </GridItem>

      <GridItem area="content">
        <ContentArea />
      </GridItem>
    </Grid>
  );
}
