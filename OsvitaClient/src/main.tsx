import { ChakraProvider, createSystem, defaultConfig } from '@chakra-ui/react';
import { ThemeProvider } from 'next-themes';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import router from './app/providers/routes/AppRoutes';
import config from './app/theme/theme';
import { ReactQueryProvider } from './app/providers/ReactQueryProvider';
import { Provider } from 'react-redux';
import store from './processes/store';
import { Toaster } from './components/ui/toaster';

const system = createSystem(defaultConfig, config);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
      <ChakraProvider value={system}>
        <ThemeProvider attribute="class" disableTransitionOnChange>
            <ReactQueryProvider>
              <RouterProvider router={router} />
              <Toaster />
            </ReactQueryProvider>
        </ThemeProvider>
      </ChakraProvider>
    </Provider>
  </StrictMode>,
);
