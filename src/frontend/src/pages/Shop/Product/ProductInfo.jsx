import React, { useContext, useState, useEffect } from "react";
import { toast } from "react-hot-toast";
import { CartContext } from "../../../context/CartContext";
import { Rating } from "@mui/material";
import { BsArrowRightShort } from "react-icons/bs";
import { UserContext } from "../../../context/UserContext"; // Corrected import path

const ProductInfo = ({ productData }) => {
  const { addToCart, cart } = useContext(CartContext);
  const { user } = useContext(UserContext); // Use UserContext
  const [quantity, setQuantity] = useState(1);
  const [isAddedToCart, setIsAddedToCart] = useState(false);
  const product = productData;

  const handleDecrement = () => {
    if (quantity > 1) {
      setQuantity(prevQuantity => prevQuantity - 1);
    }
  };

  const handleIncrement = () => {
    if (quantity < 10) {
      setQuantity(prevQuantity => prevQuantity + 1);
    }
  };

  const handleClick = async () => {
    if (!user) {
      toast.error("You need to log in to add items to the cart.");
      return;
    }

    if (!isAddedToCart) {
      try {
        await addToCart({ productId: product.id, quantity });
        toast.success("Added to cart");
        setIsAddedToCart(false);
      } catch (error) {
        toast.error("Error adding product to cart");
      }
    } else {
      toast.success("Product already added to cart", {
        style: {
          padding: "16px",
          color: "#713200",
        },
        iconTheme: {
          primary: "#713200",
          secondary: "#FFFAEE",
        },
      });
    }
  };

  return (
    <>
      <div className="relative">
        <p className="bg-[#274c5b] absolute top-0 left-0 text-white text-center rounded-md w-fit px-2 py-1 mt-4 ml-6">
          {product?.category?.name}
        </p>
        <figure>
          <img
            className="rounded-2xl h-52 lg:h-96"
            src={product?.images?.[0]}
            alt="product"
          />
        </figure>
      </div>
      <div className="w-[90%] md:w-[50%] lg:w-[40%]">
        <div className="flex flex-col gap-2 mb-3">
          <h2 className="text-xl md:text-3xl font-bold">{product?.name}</h2>
          <Rating
            size="small"
            name="half-rating-read"
            defaultValue={product?.rating}
            precision={0.5}
            readOnly
          />
          <p className="text-xl font-bold">
            <span>
              <strike className="text-[#B8B8B8] text-base font-normal">
                $20.00
              </strike>
            </span>{" "}
            ${product?.price}
          </p>
        </div>
        <p className="2xl:w-[80%]">{product?.description}</p>

        <div className="flex items-center gap-2 lg:gap-4 mt-8">
          <p className="text-xl font-semibold">Quantity: </p>
          <div className="flex items-center gap-2">
            <button
              onClick={handleDecrement}
              className="text-2xl font-semibold bg-white p-1 w-8 h-8 flex justify-center items-center rounded-md"
            >
              -
            </button>
            <input
              className="outline-none border border-[#274C5B] rounded-md w-12 h-8 text-center"
              type="number"
              name="quantity"
              value={quantity}
              readOnly
            />
            <button
              onClick={handleIncrement}
              className="text-2xl font-semibold bg-white p-1 w-8 h-8 flex justify-center items-center rounded-md"
            >
              +
            </button>
          </div>
          <button
            onClick={handleClick}
            className="w-[150px] bg-[#274c5b] text-white px-2 py-1 md:px-4 md:py-3 text-sm md:text-base rounded-md md:rounded-xl flex justify-center items-center md:font-semibold"
          >
            {isAddedToCart ? "Added To Cart" : "Add To Cart"}
            <BsArrowRightShort className="bg-[#335B6B] text-white rounded-full ml-1" />
          </button>
        </div>
      </div>
    </>
  );
};

export default ProductInfo;
