import React, { useEffect, useState } from 'react';
import { fetchBannerData } from '../../../api';
import ReusableSwiper from '../../../components/ReusableSwiper/ReusableSwiper';
import OfferCard from './OfferCard';

/**
 * Offer Component
 * 
 * This component fetches banner data from the API and displays it in a swiper (carousel) format using the ReusableSwiper component.
 * The swiper configuration is set to show different numbers of slides based on the screen width.
 * 
 * @returns {JSX.Element} The rendered Offer component.
 */
const Offer = () => {
  // State to hold the banner data fetched from API
  const [bannerData, setBannerData] = useState(null);

  useEffect(() => {
    // Function to load banner data from API
    const loadBannerData = async () => {
      // Fetch data from API
      const data = await fetchBannerData();
      // Set fetched data to state
      setBannerData(data);
    };
    // Call the function to fetch data when component mounts
    loadBannerData();
  }, []); // Empty dependency array ensures this effect runs only on mount

  const swiperOptions = {
    slidesPerView: 1, // Show 1 slide at a time
    spaceBetween: 20,
    loop: true,
    autoplay: {
      delay: 1000, // 1 second delay
      disableOnInteraction: false,
    },
  };

  return (
      <ReusableSwiper options={swiperOptions}>
        {bannerData?.map((bannerItem, index) => (
          <OfferCard key={index} bannerData={bannerItem} />
        ))}
      </ReusableSwiper>
  );
};

export default Offer;
