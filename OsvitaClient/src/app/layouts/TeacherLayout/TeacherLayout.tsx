import { Grid, GridItem, useBreakpointValue } from '@chakra-ui/react';
import { Toolbar } from '@/features/Toolbar';
import { ContentArea } from '../ContentArea';
import { useState } from 'react';
import { TeacherSidebar } from '@/shared/ui/TeacherSidebar';

export default function BaseLayout() {
  const [isSidebarOpen, setSidebarOpen] = useState(false);
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <Grid
      templateAreas={isMobile ? `"toolbar" "content"` : `"toolbar toolbar" "sidebar content"`}
      templateColumns={isMobile ? '1fr' : '16rem 1fr'}
      templateRows="auto 1fr"
      gap="1.5rem"
      h="100vh"
      padding="1.25rem 1.5rem"
      overflow="hidden"
    >
      {!isMobile || isSidebarOpen ? (
        <GridItem
          area="sidebar"
          zIndex={10}
          position={isMobile ? 'absolute' : 'relative'}
          w={isMobile ? '16rem' : 'auto'}
        >
          <TeacherSidebar onClose={() => setSidebarOpen(false)} />
        </GridItem>
      ) : null}

      <GridItem area="toolbar" colSpan={{ base: 1, md: 2 }}>
        <Toolbar onMenuToggle={() => setSidebarOpen(!isSidebarOpen)} />
      </GridItem>

      <GridItem area="content">
        <ContentArea />
      </GridItem>
    </Grid>
  );
}
