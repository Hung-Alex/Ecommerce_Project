import HomeBanner from "./Banner/HomeBanner";
import Offer from "./Offer/Offer";
import About from "./AboutUs/About";
import Product from "./Product/Product";
import Testimonial from "./Testimonial/Testimonial";
import ProductOffer from "./Offer/ProductOffer";
import Our from "./Our/Our";
import Category from "./Category/Category";
import News from "./News/News";
import Subscribe from "./Subscribe/Subscribe";
import Brands from "./Brands/Brands";


const HomePage = () => {
  return (
    <div className="max-w-screen-xl mx-auto">
      <HomeBanner />
      <Category />
      <Product />
      <Brands />
      {/* <Offer /> */}
      {/* <About /> */}
      {/* <Testimonial /> */}
      {/* <ProductOffer /> */}
      {/* <Our /> */}
      {/* <News /> */}
      <Subscribe />
    </div>
  );
};

export default HomePage;
