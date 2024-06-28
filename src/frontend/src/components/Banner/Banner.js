import React, { useState, useRef } from 'react';
// Import Swiper React components
import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/navigation';

// import required modules
import { Navigation } from 'swiper/modules';
import "./Banner.css"

const images = [
    { id: 1, topic: "Topic 1", image: "https://bizweb.dktcdn.net/100/329/122/themes/951253/assets/slider1_1.jpg?1719541902656" },
    { id: 2, topic: "Topic 2", image: "https://bizweb.dktcdn.net/100/329/122/themes/951253/assets/slider1_2.jpg?1719494496488" },
    { id: 3, topic: "Topic 3", image: "https://bizweb.dktcdn.net/100/329/122/themes/951253/assets/slider1_3.jpg?1719494496488" },
    { id: 4, topic: "Topic 4", image: "https://bizweb.dktcdn.net/100/329/122/themes/951253/assets/slider1_4.jpg?1719494496488" },
    { id: 5, topic: "Topic 5", image: "https://bizweb.dktcdn.net/100/329/122/themes/951253/assets/slider1_5.jpg?1719494496488" },
];

export default function Banner() {
    const [currentTopic, setCurrentTopic] = useState(images[0].topic);
    const swiperRef = useRef(null);

    const handleSlideChange = () => {
        if (swiperRef.current) {
            const activeIndex = swiperRef.current.activeIndex;
            const currentImage = images[activeIndex].image;
            setCurrentTopic(images[activeIndex].topic);
        }
    };

    const handleButtonClick = (index, topic) => {
        setCurrentTopic(topic);
        if (swiperRef.current) {
            swiperRef.current.slideTo(index);
        }
    };

    return (
        <div className="col-lg-9">
            <Swiper
                style={{
                    '--swiper-navigation-color': '#fff',
                }}
                loop={false}
                navigation={true}
                modules={[Navigation]}
                className="banner"
                onSlideChange={handleSlideChange}
                onSwiper={(swiper) => (swiperRef.current = swiper)}
            >
                {images.map((image, index) => (
                    <SwiperSlide key={image.id}>
                        <img className='banner-img' src={image.image} alt={image.topic} />
                    </SwiperSlide>
                ))}
            </Swiper>
            <div className="button-container">
                {images.map((image, index) => (
                    <div key={image.id} className="button-wrapper" >
                        {currentTopic === image.topic && <div className="highlight-line"></div>}
                        <button onClick={() => handleButtonClick(index, image.topic)}
                            className={currentTopic === image.topic ? "gray-background" : ""}
                        >
                            {image.topic}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}
