import { defineConfig } from '@chakra-ui/react';

const config = defineConfig({
  globalCss: {
    body: {
      margin: 0,
      padding: '1.25rem 1.5rem',
      bgColor: 'gray.100',
      overflow: 'hidden',
    },
  },
  theme: {
    tokens: {
      colors: {
        blue: {
          DEFAULT: { value: '#5C6CFF' },
          100: { value: '#F4F5FF' },
          200: { value: '#E7E9FF' },
          300: { value: '#E4E7FF' },
          400: { value: '#5C6CFF' },
          hover: { value: '#D7D9FF' },
        },
      },
    },
  },
});

export default config;
