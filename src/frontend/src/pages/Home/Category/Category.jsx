// components/CategorySlider/CategorySlider.js
import React from 'react';
import ReusableSwiper from '../../../components/ReusableSwiper/ReusableSwiper';
import { useCategoryContext } from '../../../context/CategoryContext';
import CatCard from '../../../components/UI/Card/CatCard';

const Category = () => {
    const { categories, loading, error } = useCategoryContext();

    const swiperOptions = {
        loop: true,
        autoplay: {
            delay: 1000, // 1 second delay
            disableOnInteraction: false,
        },
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

    return (
        <ReusableSwiper options={swiperOptions}>
            {categories.map((item) => (
                <CatCard key={item.id} item={item} />
            ))}
        </ReusableSwiper>
    );
};

export default Category;
