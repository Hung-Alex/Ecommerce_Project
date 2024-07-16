import Card from "../../../components/UI/Card/Card";
import { BsArrowRightShort } from "react-icons/bs";
import { Link } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import axios from "../../../utils/axios";

const Product = () => {
  const [section, setSections] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        const res = await axios.get(`/sections?TakeCategories=4&TakeItems=4`);
        setSections(res.data.data);
        setLoading(false);
      } catch (error) {
        setLoading(false);
        setError(true);
      }
    })();
  }, []);
  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading categories</p>;

  if (loading) {
    return <h1 className="text-black">Loading...</h1>;
  }

  return (
    <div className="my-12 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
      {section.map((categoryData, index) => (
        <div key={index} className="my-5 mx-auto max-w-2xl px-4 sm:px-6 sm:py-12 lg:max-w-7xl lg:px-8">
          <div className="category-section">
            <div className="text-center">
              <p className="text-[#7EB693] font-[Yellowtail] text-3xl ">{categoryData.category.name}</p>
              <h3 className="text-[#274C5B] text-4xl font-bold my-3 mb-8">
                Our New Products
              </h3>
            </div>
            <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
              {categoryData.products &&
                categoryData.products.map((product) => (
                  <Card key={product.id} img={product.images[0]} item={product} />
                ))}
            </div>
            <div className="flex justify-center mt-8">
              <Link
                to={`/category/${categoryData.category.name.toLowerCase()}`}
                className="bg-[#274c5b] text-white flex justify-center items-center w-[150px] mt-8 py-2 rounded-md"
              >
                Explore Now{" "}
                <BsArrowRightShort className="bg-[#335B6B] text-white rounded-full ml-1" />
              </Link>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Product;
