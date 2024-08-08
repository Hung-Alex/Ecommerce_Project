import img1 from "../../assets/About/Banner/banner1.png";
import img2 from "../../assets/About/Banner/banner2.png";
import BannerComponent from "../../components/Banner/Banner";

const Banner = () => {
  return (
    <BannerComponent
      img1={img1}
      img2={img2}
      title={"News"}
      className="about_banner"
    />
  );
};

export default Banner;
