import React from "react";
import Card from "../../../../components/UI/Card/Card";
import useFetch from "../../../../hooks/useFetch";
import ReusableSwiper from "../../../../components/ReusableSwiper/ReusableSwiper";

const RelatedProducts = ({ slug }) => {
  const fetchUrl = slug ? `searchs?UrlSlugCategory=${slug}` : 'searchs?UrlSlugCategory=spinach';
  const { data: product } = useFetch(fetchUrl);

  const swiperOptions = {
    spaceBetween: 20,
    loop: true,
    autoplay: {
      delay: 1000, // 1 second delay
      disableOnInteraction: false,
    },
    className: "pb-100",
    breakpoints: {
      // when window width is >= 640px
      250: {
        slidesPerView: 1, // Show 1 slide at a time
      },
      // when window width is >= 768px
      768: {
        slidesPerView: 2, // Show 2 slides at a time
      },
      // when window width is >= 1024px
      1024: {
        slidesPerView: 3, // Show 3 slides at a time
      },
      // when window width is >= 1280px
      1280: {
        slidesPerView: 4, // Show 4 slides at a time
      },
    },
  };

  if (!product) return null;

  return (
    <div className="my-24 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
      <h3 className="text-2xl text-gray-600">Related Products</h3>
      <ReusableSwiper className="w-full max-w-7xl pb-[110px]" options={swiperOptions}>
        {product.map((item) => (
          <Card key={item?.id} item={item} />
        ))}
      </ReusableSwiper>
    </div>
  );
};

export default RelatedProducts;
