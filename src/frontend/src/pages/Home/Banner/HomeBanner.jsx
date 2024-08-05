import React, { useEffect, useState } from 'react';
import { fetchActiveSlideData } from '../../../api/slider';
import ReusableSwiper from '../../../components/ReusableSwiper/ReusableSwiper';
import BannerCard from './BannerCard';

const HomeBanner = () => {
  const [slides, setSlides] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetchActiveSlideData();
        if (response.isSuccess && response.data.length > 0) {
          setSlides(response.data);
        }
      } catch (error) {
        console.error('Error fetching slide data:', error);
      }
    };

    fetchData();
  }, []);

  const swiperOptions = {
    loop: true,
    slidesPerView: 1,
    autoplay: {
      delay: 3000, // 3 seconds delay
      disableOnInteraction: false,
    },
  };

  return (
<ReusableSwiper options={swiperOptions}>
      {slides.map((slide) => (
        <BannerCard key={slide.id} slide={slide} />
      ))}
    </ReusableSwiper>
  );
};

export default HomeBanner;
