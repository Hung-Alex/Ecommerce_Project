import React, { useEffect } from 'react';
import SwiperCore, { Navigation, Thumbs, FreeMode, Autoplay } from 'swiper';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/swiper-bundle.min.css';

// Install Swiper modules
SwiperCore.use([Navigation, Thumbs, FreeMode, Autoplay]);

const SwiperComponent = () => {
  useEffect(() => {
    // Additional initialization if needed
  }, []);

  return (
    <div>
      <Swiper
        className="home-slider2"
        spaceBetween={10}
        slidesPerView={5}
        freeMode={true}
        watchSlidesProgress={true}
        breakpoints={{
          280: {
            slidesPerView: 3,
            spaceBetween: 10,
          },
          640: {
            slidesPerView: 3,
            spaceBetween: 10,
          },
          768: {
            slidesPerView: 3,
            spaceBetween: 10,
          },
          992: {
            slidesPerView: 3,
            spaceBetween: 10,
          },
          1024: {
            slidesPerView: 5,
            spaceBetween: 10,
          },
        }}
      >
        {/* Add SwiperSlides here */}
        {/* Example:
        <SwiperSlide>Slide 1</SwiperSlide>
        <SwiperSlide>Slide 2</SwiperSlide>
        */}
      </Swiper>

      <Swiper
        className="home-slider3"
        navigation={{
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        }}
        thumbs={{ swiper: SwiperCore }}
        autoplay={{
          delay: 4000,
          disableOnInteraction: true,
        }}
      >
        {/* Add SwiperSlides here */}
        {/* Example:
        <SwiperSlide>Slide 1</SwiperSlide>
        <SwiperSlide>Slide 2</SwiperSlide>
        */}
      </Swiper>

      <div className="sub_banner sub_banner--bottom d-none d-lg-flex">
        <a className="sub_banner__item banner" href="https://go.mmz.vn/o-cung-ssd-di-dong-web" title="Ổ cứng chính hãng">
          <picture>
            <source media="(max-width: 480px)" srcSet="//bizweb.dktcdn.net/thumb/medium/100/329/122/themes/951253/assets/bottom_banner_1.jpg?1719463557044" />
            <img
              className="img-fluid"
              src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/bottom_banner_1.jpg?1719463557044"
              alt="Ổ cứng chính hãng"
              width="355"
              height="172"
            />
          </picture>
        </a>

        <a className="sub_banner__item banner" href="https://go.mmz.vn/the-nho-luu-tru-web" title="Thẻ nhớ chính hãng">
          <picture>
            <source media="(max-width: 480px)" srcSet="//bizweb.dktcdn.net/thumb/medium/100/329/122/themes/951253/assets/bottom_banner_2.jpg?1719463557044" />
            <img
              className="img-fluid"
              src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/bottom_banner_2.jpg?1719463557044"
              alt="Thẻ nhớ chính hãng"
              width="355"
              height="172"
            />
          </picture>
        </a>

        <a className="sub_banner__item banner" href="https://go.mmz.vn/Cate-Gear" title="Chuột - Bàn phím - Tai nghe">
          <picture>
            <source media="(max-width: 480px)" srcSet="//bizweb.dktcdn.net/thumb/medium/100/329/122/themes/951253/assets/bottom_banner_3.jpg?1719463557044" />
            <img
              className="img-fluid"
              src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/bottom_banner_3.jpg?1719463557044"
              alt="Chuột - Bàn phím - Tai nghe"
              width="355"
              height="172"
            />
          </picture>
        </a>
      </div>
    </div>
  );
};

export default SwiperComponent;