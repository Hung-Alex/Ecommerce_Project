import React from 'react';
import img from "../../../assets/Home/Banner/Image.png";
import Button from "../../../components/UI/Buttons/Button";
import { BsArrowRightShort } from "react-icons/bs";
import useIntersectionObserver from '../../../hooks/useIntersectionObserver'; // Đường dẫn đến hook của bạn

const HomeBanner = () => {
  const [ref, isVisible] = useIntersectionObserver();

  return (
    <div
      ref={ref}
      className={` bg-[#f1eff0] h-[520px] flex flex-col justify-center bg-center bg-cover pl-20 ${isVisible ? 'slide-up visible' : 'slide-up'}`}
      style={{ backgroundImage: `url(${img})` }}
    >
      <div className={`${isVisible ? 'fade-in visible' : 'fade-in'}`}>
        <p className="font-[Yellowtail] text-[#68A47F] text-4xl">
          100% Natural Food
        </p>
        <h2 className="text-7xl font-extrabold text-white">
          Choose the best <br />
          healthier way <br /> of life
        </h2>
      </div>
      <Button className={"w-[150px] mt-8 bg-[#EFD372] text-[#274c5b]"}>
        Explore Now{" "}
        <BsArrowRightShort className="bg-[#274c5b] text-white rounded-full ml-1" />
      </Button>
    </div>
  );
};

export default HomeBanner;
