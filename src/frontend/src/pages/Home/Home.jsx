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

/**
 * HomePage Component
 * 
 * This component serves as the main page of the application, assembling various sections into a single view.
 * 
 * @returns {JSX.Element} The rendered HomePage component.
 */
const HomePage = () => {
  return (
    <>
      {/* Banner section at the top of the homepage */}
      <HomeBanner />

      {/* Category section showcasing different product categories */}
      <Category />

      {/* Container for the main content, centered and with max width */}
      <div className="max-w-screen-xl mx-auto">
        {/* Product section displaying product listings */}
        <Product />

        {/* Offer section displaying special offers or promotions */}
        <Offer />

        {/* Brands section showcasing various brands */}
        <Brands />

        {/* Uncomment the following lines to include additional sections */}
        {/* About section providing information about the company */}
        {/* <About /> */}

        {/* ProductOffer section for additional product offers */}
        {/* <ProductOffer /> */}

        {/* Our section detailing company or team information */}
        {/* <Our /> */}

        {/* News section displaying the latest news or updates */}
        <News />

        {/* Subscribe section for newsletter or updates subscription */}
        {/* <Subscribe /> */}
      </div>
    </>
  );
};

export default HomePage;
