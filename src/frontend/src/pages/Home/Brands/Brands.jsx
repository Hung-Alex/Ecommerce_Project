import React from "react";
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css'; // Import core Swiper styles
import 'swiper/css/navigation'; // Import navigation styles (optional)
import 'swiper/css/pagination'; // Import pagination styles (optional)
import CatCard from "../../../components/UI/Card/CatCard.jsx";
import { useBrandContext } from "../../../context/BrandContext.jsx";

const Brands = () => {
  const { brands, loading, error } = useBrandContext();

  const handleError = (error) => {
    console.error("Error loading categories:", error); // Log the error for debugging
    return <p>Error loading categories</p>;
  };

  if (loading) return <p>Loading...</p>;
  if (error) return handleError(error);

  return (
    <div className="mt-12">
      <div className="text-center">
        <h3 className="text-[#7EB693] font-[Yellowtail] text-5xl">Brands</h3>
      </div>
      <div className="mt-16">
        <Swiper
          spaceBetween={20} // Optional: Spacing between slides (in pixels)
          slidesPerView={1} // Default to 1 slide per view
          breakpoints={{
            768: {
              slidesPerView: 4, // Adjust for medium screens
            },
            992: {
              slidesPerView: 6, // Adjust for larger screens
            },
          }}
          pagination={{ clickable: true }} // Optional: Enable pagination dots
          loop={true}
          modules={[ ]} // Register modules
        >
          {brands.map((item, index) => (
            <SwiperSlide key={index} >
              <CatCard  item={item} />
            </SwiperSlide>
          ))}
        </Swiper>
      </div>
    </div>
  );
};

export default Brands;
