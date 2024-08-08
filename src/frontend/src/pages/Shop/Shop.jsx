import { Link, useParams } from "react-router-dom";
import ShopBanner from "./Banner/ShopBanner";
import CategoryList from "./CategoryList/CategoryList";
import { useState, useEffect } from "react";
import Card from "../../components/UI/Card/Card";
import { fetchData } from "../../api";

const ShopPage = () => {
  const { slug , name } = useParams();
  const [type, setType] = useState("default");
  const [sort, setSort] = useState("default");
  const [page, setPage] = useState(1);
  const [pageSize] = useState(8); // You can set this to a different value if needed
  const [products, setProducts] = useState([]);
  const [searchQuery, setSearchQuery] = useState(''); // New state for search query
  const [totalProducts, setTotalProducts] = useState(0);
  const [totalPages, setTotalPages] = useState(1);

  useEffect(() => {
    const fetchProducts = async () => {
      const params = {
        ProductName: name || "", // Include search query
        UrlSlugCategory: slug === 'category' ? '' : name, // Check if slug is 'category'
        UrlSlugBrand: '',
        PageSize: pageSize,
        PageNumber: page,
        SortColoumn: type,
        SortBy: sort,
      };

      try {
        const data = await fetchData(params);
        setProducts(data.data); // Assuming data.data.products contains the product list
        setTotalProducts(data.data.total); // Assuming data.data.total contains the total number of products
        setTotalPages(Math.ceil(data.data.total / pageSize)); // Calculate total pages
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    fetchProducts();
  }, [name, slug, type, sort, page, pageSize, searchQuery]); // Include searchQuery in dependencies

  const handleSortChange = (e) => {
    const [newType, newSort] = e.target.value.split(':');
    setType(newType);
    setSort(newSort);
    setPage(1); // Reset to the first page when sorting changes
  };

  const handlePageChange = (newPage) => {
    setPage(newPage);
  };

  const handleSearchChange = (e) => {
    setSearchQuery(e.target.value);
    setPage(1); // Reset to the first page when search query changes
  };

  return (
    <>
      <div className="sm:px-12">
        <ShopBanner name={slug} />
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
              <div className="flex gap-6 items-center">
                <select
                  className="outline-none border-b-2 pb-1"
                  name="sort"
                  id="sort"
                  onChange={handleSortChange}
                >
                  <option value="default:default">Default sorting</option>
                  <option value="createdAt:ASC">Sort by latest</option>
                  <option value="price:ASC">Sort by price: low to high</option>
                  <option value="price:DESC">Sort by price: high to low</option>
                </select>
              </div>
            </div>
            <div className="mb-8 px-4 lg:px-8">
              {products.length > 0 ? (
                <>
                  <div className="mt-6 grid grid-cols-1 gap-3 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 xl:gap-x-8">
                    {products.map((item) => (
                      <Card key={item.id} item={item} />
                    ))}
                  </div>
                  <div className="flex justify-center mt-8">
                    <button
                      className="btn"
                      onClick={() => handlePageChange(page - 1)}
                      disabled={page === 1}
                    >
                      Previous
                    </button>
                    <span className="mx-4">Page {page} of {totalPages}</span>
                    <button
                      className="btn"
                      onClick={() => handlePageChange(page + 1)}
                      disabled={page === totalPages}
                    >
                      Next
                    </button>
                  </div>
                </>
              ) : (
                <h1 className="text-black text-xl text-center mt-32">
                  No Products Found
                </h1>
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ShopPage;
