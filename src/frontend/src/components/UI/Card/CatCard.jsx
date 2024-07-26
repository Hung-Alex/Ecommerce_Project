import React from 'react';
import useIntersectionObserver from '../../../hooks/useIntersectionObserver'; // Đường dẫn đến hook của bạn

const CatCard = ({ item, effect }) => {
  const { image, name, description } = item;
  const [ref, isVisible] = useIntersectionObserver({ threshold: 0.8 });

  const effectClass = `${effect} ${isVisible ? 'visible' : ''}`;

  return (
    <div
      ref={ref}
      className={`relative overflow-hidden cursor-pointer h-96 w-full group ${isVisible ? 'opacity-100' : 'opacity-0'}`}
    >
      <a href={`/category/${name}`} className="block h-full w-full">
        <div
          className={`relative h-full w-full bg-cover bg-center transition-transform duration-300 group-hover:scale-105 ${effectClass}`}
          style={{ backgroundImage: `url(${image})` }}
        >
          <div
            className={`pl-20 absolute inset-0 bg-black bg-opacity-0 transition-opacity duration-300 group-hover:bg-opacity-50 flex flex-col justify-center p-4 text-white ${effectClass}`}
          >
            <h3 className={`text-5xl font-[Yellowtail] transition-transform duration-300 group-hover:text-yellow-400 ${effectClass}`}>
              {name}
            </h3>
            <p className={`text-sm font-extrabold ${effectClass}`}>
              {description}
            </p>
          </div>
        </div>
      </a>
    </div>
  );
};

export default CatCard;
