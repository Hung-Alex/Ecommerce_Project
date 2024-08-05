import img1 from "../../../assets/About/Banner/banner1.png";
import img2 from "../../../assets/About/Banner/banner2.png";
import BannerComponent from "../../../components/Banner/Banner";

const OrderBanner = () => {
  return (
    <BannerComponent
      img1={img1}
      img2={img2}
      title={"Your Order"}
      className="about_banner"
    />
  );
};

export default OrderBanner;
