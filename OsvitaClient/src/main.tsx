import { ChakraProvider } from '@chakra-ui/react';
import { ThemeProvider } from 'next-themes';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import theme from '@/app/theme/theme';
import { RouterProvider } from 'react-router-dom';
import router from './app/providers/routes/AppRoutes';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ChakraProvider value={theme}>
      <ThemeProvider attribute="class" disableTransitionOnChange>
        <RouterProvider router={router} />
      </ThemeProvider>
    </ChakraProvider>
  </StrictMode>,
);
