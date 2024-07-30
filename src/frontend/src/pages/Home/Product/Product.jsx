import Card from "../../../components/UI/Card/Card";
import { Link } from "react-router-dom";
import useFetch from "../../../hooks/useFetch";
import { useState, useEffect } from "react";
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/grid'; // Import grid styles
import { Pagination, Grid } from "swiper/modules";

const Product = () => {
  const { data, loading, error } = useFetch(`/sections?TakeCategories=4&TakeItems=20`);
  const [selectedCategory, setSelectedCategory] = useState(null);

  useEffect(() => {
    if (data.length > 0) {
      setSelectedCategory(data[0].category.name); // Set the default selected category to the first one
    }
  }, [data]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error!</p>;

  const handleCategoryClick = (category) => {
    setSelectedCategory(category);
  };

  const filteredProducts = selectedCategory
    ? data.find(categoryData => categoryData.category.name === selectedCategory)?.products || []
    : [];

  // return (
  //   <div className="my-12 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
  //     <div className="category-list flex justify-center mb-8">
  //       {data.map((categoryData, index) => (
  //         <div key={index} className="mx-4">
  //           <Link
  //             to="#"
  //             onClick={() => handleCategoryClick(categoryData.category.name)}
  //             className={`text-center text-lg font-semibold ${selectedCategory === categoryData.category.name ? 'text-[#7EB693]' : 'text-gray-500'}`}
  //           >
  //             {categoryData.category.name}
  //           </Link>
  //         </div>
  //       ))}
  //     </div>
  //     {filteredProducts.length > 0 && (
  //       <div className="category-section">
  //         <Swiper
  //           spaceBetween={20}
  //           slidesPerView={4} // Display 4 columns
  //           grid={{
  //             rows: 2, // Display 2 rows
  //             fill: 'row',
  //           }}
  //           // pagination={{ clickable: true }}
  //           loop={false} // Enable looping
  //           modules={[Pagination, Grid]} // Import necessary modules
  //         >
  //           {filteredProducts.map((product) => (
  //             <SwiperSlide key={product.id}>
  //               <Card item={product} />
  //             </SwiperSlide>
  //           ))}
  //         </Swiper>
  //         <div className="flex justify-center mt-8">
  //           {/* Optional: Additional navigation or actions can be added here */}
  //         </div>
  //       </div>
  //     )}
  //   </div>
  // );
};

export default Product;
