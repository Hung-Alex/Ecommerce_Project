// ReusableSwiper.js
import React from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';
import 'swiper/css/effect-cube';

// Import required modules
import { Navigation, Pagination, Scrollbar, A11y, Zoom, EffectCube } from 'swiper/modules';

const ReusableSwiper = ({ children, options }) => {
    return (
        <Swiper
            modules={[Navigation, Pagination, Scrollbar, A11y, Zoom, EffectCube]}
            {...options}
        >
            {React.Children.map(children, (child, index) => (
                <SwiperSlide key={index}>
                    {child}
                </SwiperSlide>
            ))}
        </Swiper>
    );
};

export default ReusableSwiper;
