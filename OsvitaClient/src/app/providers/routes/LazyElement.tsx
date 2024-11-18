import Loader from '@/shared/ui/Loader/Loader';
import { Suspense } from 'react';

const LazyElement = (Component: React.LazyExoticComponent<React.FC>) => (
  <Suspense fallback={<Loader />}>
    <Component />
  </Suspense>
);

export default LazyElement;
