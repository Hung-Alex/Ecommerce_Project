import HomeBanner from "./Banner/HomeBanner";
import Offer from "./Offer/Offer";
import About from "./AboutUs/About";
import Product from "./Product/Product";
import ProductOffer from "./Offer/ProductOffer";
import Our from "./Our/Our";
import Category from "./Category/Category";
import News from "./News/News";
import Subscribe from "./Subscribe/Subscribe";
import Brands from "./Brands/Brands";
import TokenChecker from "../../Test";


const HomePage = () => {
  return (
    <>
    <TokenChecker/>
      <HomeBanner />
      <Category />
      <div className="max-w-screen-xl mx-auto">
        <Product />
        {/* <Offer /> */}
        <Brands />
        {/* <About /> */}
        {/* <ProductOffer /> */}
        {/* <Our /> */}
        <News />
        {/* <Subscribe /> */}
      </div>
    </>
  );
};

export default HomePage;
