import Card from "../../../components/UI/Card/Card";
import { BsArrowRightShort } from "react-icons/bs";
import { Link } from "react-router-dom";
import useFetch from "../../../hooks/useFetch";

import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css'; // Import core Swiper styles
import 'swiper/css/navigation'; // Import navigation styles (optional)
import 'swiper/css/pagination'; // Import pagination styles (optional)
import { Pagination } from "swiper/modules";

const Product = () => {
  const { data, loading, error } = useFetch(`/sections?TakeCategories=4&TakeItems=8`);
  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error!</p>;

  return (
    <div className="my-12 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
      {data.map((categoryData, index) => (
        <div key={index} className="my-5 mx-auto max-w-2xl px-4 sm:px-6 sm:py-12 lg:max-w-7xl lg:px-8">
          <div className="category-section">
            <div className="text-center">
              <Link
                to={`/category/${categoryData.category.name}`}
                onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })}
              >
                <h3 className="my-12 text-[#7EB693] font-[Yellowtail] text-5xl">
                  {categoryData.category.name}
                </h3>
              </Link>
            </div>
            <Swiper
              spaceBetween={20} // Optional: Spacing between slides (in pixels)
              slidesPerView={1} // Default to 1 slide per view
              breakpoints={{
                768: {
                  slidesPerView: 3, // Adjust for medium screens
                },
                992: {
                  slidesPerView: 4, // Adjust for larger screens
                },
              }}
              pagination={{ clickable: true }} // Optional: Enable pagination dots
              loop={true}
            >
              {categoryData.products &&
                categoryData.products.map((product) => (
                  <SwiperSlide key={product.id} >
                      <Card item={product} />
                  </SwiperSlide>
                ))}
            </Swiper>
            <div className="flex justify-center mt-8">
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Product;
