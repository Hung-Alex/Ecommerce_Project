import React from 'react';
import useIntersectionObserver from '../../../hooks/useIntersectionObserver';
import Button from "../../../components/UI/Buttons/Button";
import { BsArrowRightShort } from "react-icons/bs";

const BannerCard = ({ slide }) => {
  const [ref, isVisible] = useIntersectionObserver({ threshold: 0.8 });
  const { image, title, description } = slide;

  return (
    <div
      ref={ref}
      className={`bg-[#f1eff0] h-[100vh] flex flex-col justify-center items-center  bg-cover`}
      style={{
        backgroundImage: `url(${image})`,
        backgroundAttachment: 'fixed'
      }}
    >
      <div className={`text-center p-8 ${isVisible ? 'slide-left visible' : 'slide-left'}`}>
        <p className={`font-[Yellowtail] text-[#68A47F] text-5xl mb-4 leading-tight ${isVisible ? 'slide-left visible' : 'slide-left'}`}>
          {title}
        </p>
        <h2 className="text-2xl font-extrabold text-white max-w-3xl">
          {description}
        </h2>
        <Button className={"w-[150px] mt-8 mx-auto bg-[#EFD372] text-[#274c5b]"}>
        Explore Now{" "}
        <BsArrowRightShort className="bg-[#274c5b] text-white rounded-full ml-1" />
      </Button>
      </div>
    </div>
  );
};

export default BannerCard;
