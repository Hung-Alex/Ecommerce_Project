import React from "react";
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css'; // Import core Swiper styles
import 'swiper/css/navigation'; // Import navigation styles (optional)
import 'swiper/css/pagination'; // Import pagination styles (optional)
import { useBrandContext } from "../../../context/BrandContext.jsx";
import BrandCard from "../../../components/UI/Card/BrandCard.jsx";


export default function Brands() {
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
      <div className="p-16">
        <Swiper
          spaceBetween={20} // Optional: Spacing between slides (in pixels)
          slidesPerView={1} // Default to 1 slide per view
          breakpoints={{
            640: {
              slidesPerView: 2, // Hiển thị 2 slide cho màn hình từ 640px trở lên
              spaceBetween: 15, // Khoảng cách giữa các slide
            },
            768: {
              slidesPerView: 3, // Hiển thị 3 slide cho màn hình từ 768px trở lên
              spaceBetween: 20,
            },
            1024: {
              slidesPerView: 4, // Hiển thị 4 slide cho màn hình từ 1024px trở lên
              spaceBetween: 25,
            },
            1280: {
              slidesPerView: 6, // Hiển thị 6 slide cho màn hình từ 1280px trở lên
              spaceBetween: 30,
            },
          }}
          pagination={{ clickable: true }} // Optional: Enable pagination dots
          loop={true}
          modules={[ ]} // Register modules
        >
          {brands.map((item, index) => (
            <SwiperSlide key={index} >
              <BrandCard  item={item} />
            </SwiperSlide>
          ))}
        </Swiper>
      </div>
    </div>
  );
};