import { Link, useParams } from "react-router-dom";
import ShopBanner from "./Banner/ShopBanner";
import CategoryList from "./CategoryList/CategoryList";
import Products from "./Products/Products";
import useFetch from "../../hooks/useFetch";
import { useState } from "react";
import Header from "../../components/Shared/Header/Header";
import Footer from "../../components/Shared/Footer/Footer";

const ShopPage = () => {
  const { name } = useParams();
  const [type, setType] = useState("default");
  const [sort, setSort] = useState("default");
  const { data: products, loading, error } = useFetch(`/searchs?UrlSlugCategory=${name}&SortColoumn=${type}&SortBy=${sort}`);

  const handleSortChange = (e) => {
    const [type, sort] = e.target.value.split(':');
    setType(type);
    setSort(sort);
  };

  return (
    <>
      <div className="sm:px-12">
        <ShopBanner name={name} />
        <div className="grid grid-cols-12 items-start">
          <div className="col-span-3 lg:col-span-2 hidden lg:block">
            <CategoryList />
          </div>
          <div className="col-span-12 lg:col-span-10">
            <div className="flex justify-between items-center flex-wrap my-8">
              <div className="text-sm breadcrumbs mb-1 md:mb-0">
                <ul>
                  <li>
                    <Link to="/">Home</Link>
                  </li>
                  <li>Category</li>
                </ul>
              </div>
              <div className="flex gap-6">
                <div>
                  <select
                    className="outline-none border-b-2 pb-1"
                    name="sort"
                    id="sort"
                    onChange={handleSortChange}
                  >
                    <option value="default:default">Default sorting</option>
                    <option value="totalRate:DESC">Sort by popularity</option>
                    <option value="latest:latest">Sort by latest</option>
                    <option value="price:ASC">Sort by price: low to high</option>
                    <option value="price:DESC">Sort by price: high to low</option>
                  </select>
                </div>
              </div>
            </div>
            <Products products={products} />
          </div>
        </div>
      </div>
    </>
  );
};

export default ShopPage;
