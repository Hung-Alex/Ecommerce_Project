import Card from "../../../components/UI/Card/Card";
import { BsArrowRightShort } from "react-icons/bs";
import { Link } from "react-router-dom";
import useFetch from "../../../hooks/useFetch";

const Product = () => {
  const { data, loading, error } = useFetch(`/sections?TakeCategories=4&TakeItems=4`);
  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error!</p>;

  return (
    <div className="my-12 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
      {data.map((categoryData, index) => (
        <div key={index} className="my-5 mx-auto max-w-2xl px-4 sm:px-6 sm:py-12 lg:max-w-7xl lg:px-8">
          <div className="category-section">
            <div className="text-center">
              <Link
                to={`/category/${categoryData.category.name}`}
                onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })}
              >
                <h3 className="text-[#7EB693] font-[Yellowtail] text-5xl">
                  {categoryData.category.name}
                </h3>
              </Link>
            </div>
            <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
              {categoryData.products &&
                categoryData.products.map((product) => (
                  <Card key={product.id} item={product} />
                ))}
            </div>
            <div className="flex justify-center mt-8">
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Product;