// useIntersectionObserver.js
import { useState, useEffect, useRef } from 'react';

const useIntersectionObserver = (options) => {
  const [isVisible, setIsVisible] = useState(false);
  const ref = useRef(null);

  useEffect(() => {
    const observer = new IntersectionObserver(
      ([entry]) => setIsVisible(entry.isIntersecting),
      {
        root: null, // Theo dõi trong viewport
        // rootMargin: '0px 0px -50px 0px', // Mở rộng khu vực quan sát phía dưới
        threshold: 0.2 // 10% phần tử phải nằm trong viewport để kích hoạt
      }
    );

    if (ref.current) {
      observer.observe(ref.current);
    }

    return () => {
      if (ref.current) {
        observer.unobserve(ref.current);
      }
    };
  }, [options]);

  return [ref, isVisible];
};

export default useIntersectionObserver;
