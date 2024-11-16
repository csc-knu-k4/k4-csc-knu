import { defineConfig } from '@chakra-ui/react';

const config = defineConfig({
  globalCss: {
    body: {
      margin: 0,
      bgColor: 'gray.100',
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
          placeholder: { value: '#B1B8FF' },
        },
        orange: {
          DEFAULT: { value: '#FF6D18' },
          100: { value: '#FCEDE6' },
          200: { value: '#FFCEB1' },
          300: { value: '#FF6D18' },
          hover: { value: '#fed7aa' },
          placeholder: { value: '#fb923c' },
        },
      },
      fonts: {
        body: { value: 'Inter, sans-serif' },
        heading: { value: 'Inter, sans-serif' },
      },
    },
  },
});

export default config;
