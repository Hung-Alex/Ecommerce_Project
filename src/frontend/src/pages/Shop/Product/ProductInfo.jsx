import React, { useContext, useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import useFetch from "../../../hooks/useFetch";
import { toast } from "react-hot-toast";
import { CartContext } from "../../../context/CartContext";
import { UserContext } from "../../../context/UserContext";
import { Rating } from "@mui/material";

const ProductInfo = () => {
  const { slug } = useParams();
  const { data: productData, loading, error } = useFetch(`/products/${slug}`);
  const { addToCart } = useContext(CartContext);
  const { user } = useContext(UserContext);
  const [quantity, setQuantity] = useState(1);
  const [mainImage, setMainImage] = useState("");
  const [selectedVariant, setSelectedVariant] = useState(null);
  const [tooltip, setTooltip] = useState({ visible: false, text: "" });

  useEffect(() => {
    if (productData && productData.images?.length) {
      setMainImage(productData.images[0]);
    }
  }, [productData]);

  const handleAddToCart = async () => {
    if (!user) {
      toast.error("You need to log in to add items to the cart.");
      return;
    }
    try {
      await addToCart({ productId: productData.id, quantity });
      toast.success("Added to cart");
    } catch (error) {
      toast.error("Error adding product to cart");
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading product data</p>;

  return (
    <div className="bg-gray-100">
      <div className="container mx-auto px-4 py-8">
        <div className="flex flex-wrap -mx-4">
          {/* Product Images */}
          <ProductImages
            images={productData?.images || []}
            mainImage={mainImage}
            setMainImage={setMainImage}
          />

          {/* Product Details */}
          <ProductDetails
            product={productData}
            quantity={quantity}
            setQuantity={setQuantity}
            selectedVariant={selectedVariant}
            setSelectedVariant={setSelectedVariant}
            tooltip={tooltip}
            setTooltip={setTooltip}
            onAddToCart={handleAddToCart}
          />
        </div>
      </div>
    </div>
  );
};

const ProductImages = ({ images, mainImage, setMainImage }) => (
  <div className="w-full md:w-1/2 px-4 mb-8">
    {images.length ? (
      <>
        <div className="w-full flex justify-center">
          <img src={mainImage} alt="Product" className="w-auto h-96 rounded-lg shadow-md mb-4" />
        </div>

        <div className="flex gap-4 py-4 justify-center overflow-x-auto">
          {images.map((image, index) => (
            <img
              key={index}
              src={image}
              alt={`Thumbnail ${index + 1}`}
              className="w-16 h-16 object-cover rounded-md cursor-pointer opacity-60 hover:opacity-100 transition duration-300"
              onClick={() => setMainImage(image)}
            />
          ))}
        </div>
      </>
    ) : (
      <p>No images available</p>
    )}
  </div>
);

const ProductDetails = ({ product, quantity, setQuantity, selectedVariant, setSelectedVariant, tooltip, setTooltip, onAddToCart }) => (
  <div className="w-full md:w-1/2 px-4">
    <div className="flex">
    <h2 className="text-3xl font-bold mr-4 mb-2">{product?.name || "Product Name"}</h2>
    {/* <p className="text-gray-600 mb-4">SKU: {product?.sku || "N/A"}</p> */}
    <p className="bg-gray-600 inline p-1 rounded-lg text-white mb-4">
      {product?.isStock ? 'In Stock' : 'Out of Stock'}
    </p>
    </div>

    <div className="mb-4">
      <span className="text-2xl font-bold mr-2">${product?.price || "0.00"}</span>
      <span className="text-gray-500 line-through">${product?.oldPrice || "0.00"}</span>
    </div>
    <div className="flex items-center mb-4">
      <Rating name="read-only" value={product?.rating || 0} readOnly size="small" />
      <span className="ml-2 text-gray-600">{product?.rating || "N/A"} ({product?.reviews || 0} reviews)</span>
    </div>
    <p className="text-gray-700 mb-6">{product?.description || "No description available"}</p>

    {/* <div className="mb-6">
      <h3 className="text-lg font-semibold mb-2">Select Option:</h3>
      <div className="relative">
        <div className="flex flex-wrap gap-2 mt-2">
          {product?.variants?.map(variant => (
            <div key={variant.id} className="relative group">
              <button
                className={`bg-gray-300 text-gray-700 py-2 px-4 rounded-full font-bold hover:bg-gray-400 ${selectedVariant?.id === variant.id ? 'bg-blue-500 text-white' : ''}`}
                onClick={() => setSelectedVariant(variant)}
                onMouseEnter={() => setTooltip({ visible: true, text: variant.description })}
                onMouseLeave={() => setTooltip({ visible: false, text: "" })}
              >
                {variant.name}
              </button>
              {tooltip.visible && (
                <div className="absolute left-0 mt-2 bg-black text-white text-xs rounded-md py-1 px-2 whitespace-nowrap opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                  {tooltip.text}
                </div>
              )}
            </div>
          ))}
        </div>
      </div>
    </div> */}

    <QuantitySelector quantity={quantity} onDecrement={() => setQuantity(q => q - 1)} onIncrement={() => setQuantity(q => q + 1)} />

    <div className="flex space-x-4 mb-6">
      <ActionButtons onAddToCart={onAddToCart} isInStock={product?.isStock}  />
    </div>
  </div>
);

const QuantitySelector = ({ quantity, onDecrement, onIncrement }) => (
  <div className="mb-6">
    <label htmlFor="quantity" className="block text-sm font-medium text-gray-700 mb-1">Quantity:</label>
    <div className="flex items-center">
      <button
        type="button"
        className="w-8 h-8 flex items-center justify-center bg-gray-300 rounded-md text-gray-700 hover:bg-gray-400"
        onClick={onDecrement}
        disabled={quantity <= 1}
      >
        -
      </button>
      <input
        type="number"
        id="quantity"
        name="quantity"
        min="1"
        value={quantity}
        className="w-12 text-center rounded-md border-gray-300 shadow-sm mx-2"
        readOnly
      />
      <button
        type="button"
        className="w-8 h-8 flex items-center justify-center bg-gray-300 rounded-md text-gray-700 hover:bg-gray-400"
        onClick={onIncrement}
      >
        +
      </button>
    </div>
  </div>
);

const ActionButtons = ({ onAddToCart, isInStock }) => (
  <>
  <button
    onClick={isInStock ? onAddToCart : null}
    disabled={!isInStock}
    className={`flex gap-2 items-center px-6 py-2 rounded-md text-white focus:outline-none focus:ring-2 focus:ring-offset-2 
      ${isInStock ? 'bg-[#4fb373] hover:bg-[#257a44] focus:bg-[#4fb373]' : 'bg-gray-500 cursor-not-allowed'}`}
  >
    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6">
      <path strokeLinecap="round" strokeLinejoin="round" d="M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0zm15.75-9.75a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0zm-5.625-6h-5.25" />
    </svg>
    Add to Cart
  </button>
    {/* <button
      className="flex gap-2 items-center text-[#4fb373] border border-[#4fb373] px-6 py-2 rounded-md hover:bg-[#f0fff4] focus:outline-none focus:ring-2 focus:ring-offset-2"
    >
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6">
        <path strokeLinecap="round" strokeLinejoin="round" d="M12 21.75c4.287 0 7.75-3.463 7.75-7.75S16.287 6.25 12 6.25 4.25 9.713 4.25 14s3.463 7.75 7.75 7.75zm0-15a5.25 5.25 0 1 1 0 10.5A5.25 5.25 0 0 1 12 6.75zm-1.25 5.25a1.25 1.25 0 1 1 2.5 0 1.25 1.25 0 0 1-2.5 0z" />
      </svg>
      View Details
    </button> */}
  </>
);

export default ProductInfo;
