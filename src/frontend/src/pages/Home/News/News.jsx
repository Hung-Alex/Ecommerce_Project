import { useState, useEffect } from "react";
import { BsArrowRightShort } from "react-icons/bs";
import Button from "../../../components/UI/Buttons/Button";
import NewsCard from "./NewsCard";
import { fetchNewsPublished } from '../../../api';
import ReusableSwiper from "../../../components/ReusableSwiper/ReusableSwiper";

const News = () => {
  const [newsData, setNewsData] = useState([]);

  const payload = {
    Title: '',
    PageSize: 10,
    PageNumber: 1,
    SortColumn: "createdAt",
    SortBy: "ASC",
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetchNewsPublished(payload);
      setNewsData(response.data);
    };

    fetchData();
  }, []);

  const swiperOptions = {
    slidesPerView: 2, // Show 1 slide at a time
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
        slidesPerView: 1, // Show 2 slides at a time
      },
      // when window width is >= 1024px
      1024: {
        slidesPerView: 2, // Show 3 slides at a time
      },
      // when window width is >= 1280px
      1280: {
        slidesPerView: 2, // Show 4 slides at a time
      },
    },
  };

  return (
    <div className="my-24 flex flex-col">
      <div className="flex justify-between items-center mx-[150px]">
        <div>
          <p className="font-[Yellowtail] text-[#68A47F] text-2xl">News</p>
          <h2 className="text-[#274c5b] text-4xl font-bold my-1">
            Discover weekly content about <br /> organic food, & more
          </h2>
        </div>
        <a href="/news" className="px-4 py-2 rounded-lg  w-[150px] border border-[#274c5b] text-[#274c5b] flex items-center justify-center">
          More News{" "}
          <BsArrowRightShort className="bg-[#335B6B] text-white rounded-full ml-1" />
        </a>

      </div>
      <div className="flex justify-center items-center gap-12 mt-8">
        <ReusableSwiper options={swiperOptions}>
          {newsData.map((newsItem, index) => (
            <NewsCard key={index} newsData={newsItem} />
          ))}
        </ReusableSwiper>
      </div>
    </div>
  );
};

export default News;
