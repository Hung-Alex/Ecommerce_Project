import React, { useState, useEffect, useRef } from 'react';
import { useCategoryContext } from '../../../context/CategoryContext';
import { useBrandContext } from '../../../context/BrandContext';
import { titleToSlug } from '../../../utils/slugify';
import {
  fetchProductData,
  updateProduct,
  uploadImages,
  deleteImage,
  fetchUpdatedImages,
} from '../../../api/index'; // Adjust the import path as needed

const UpdateProductForm = ({ productId, onClose }) => {
  const { categories } = useCategoryContext();
  const { brands } = useBrandContext();
  const [isStock, setIsStock] = useState(false);
  const [productData, setProductData] = useState(null);
  const [name, setName] = useState('');
  const [urlSlug, setUrlSlug] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [oldPrice, setOldPrice] = useState('');
  const [discount, setDiscount] = useState('');
  const [brandId, setBrandId] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [images, setImages] = useState([]);
  const [files, setFiles] = useState([]);
  const fileInputRef = useRef(null);

  useEffect(() => {
    setUrlSlug(titleToSlug(name));
  }, [name]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const res = await fetchProductData(productId);
        const data = res.data;
        setProductData(data);
        setName(data.name);
        setUrlSlug(data.urlSlug);
        setDescription(data.description);
        setIsStock(data.isStock);
        setPrice(data.price);
        setOldPrice(data.oldPrice);
        setDiscount(data.discount);
        setBrandId(data.brandId);
        setCategoryId(data.categoryId);
        setImages(data.images);
        setVariants(data.variants || []);
      } catch (error) {
        console.error('Error fetching product data:', error);
      }
    };

    fetchData();
  }, [productId]);

  const handleProductSave = async () => {
    updateProduct(productId, { id: productId, name, urlSlug, description, price, oldPrice, isStock, discount, brandId, categoryId }).then(res => {
      if (res?.isSuccess) {
        onClose();
      }
    })
  };

  const handleFileChange = (e) => {
    const selectedFiles = Array.from(e.target.files);
    setFiles(prevFiles => [...prevFiles, ...selectedFiles]);
    e.target.value = null; // Reset file input value
  };

  const handleRemoveFile = (index) => {
    setFiles(prevFiles => prevFiles.filter((_, i) => i !== index));
  };

  const handleImageSave = async () => {
    try {
      await uploadImages(productId, files);
      const updatedImages = await fetchUpdatedImages(productId);
      setImages(updatedImages);
      setFiles([]);
    } catch (error) {
      console.error('Error uploading images:', error);
    }
  };

  const handleDeleteImage = async (imageId) => {
    try {
      await deleteImage(imageId);
      const updatedImages = await fetchUpdatedImages(productId);
      setImages(updatedImages);
    } catch (error) {
      console.error('Error deleting image:', error);
    }
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50 overflow-y-auto">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-4xl w-full max-h-full overflow-auto">
        {/* Image Upload Section */}
        <div
          className="border-2 border-dashed border-gray-300 p-6 text-center cursor-pointer transition-colors duration-200"
          onClick={() => fileInputRef.current.click()}
        >
          <p className="text-gray-500">Drag & drop images here, or click to select</p>
          <input
            type="file"
            id="fileInput"
            accept="image/*"
            multiple
            onChange={handleFileChange}
            ref={fileInputRef}
            className="hidden"
          />
        </div>
        {files.length > 0 && (
          <div className="mt-6">
            <h3 className="text-lg font-semibold mb-4">Selected Images:</h3>
            <div className="max-h-60 overflow-auto border border-gray-300 rounded-md p-2">
              <div className="flex flex-wrap gap-4">
                {files.map((file, index) => (
                  <div key={index} className="relative">
                    <img
                      src={URL.createObjectURL(file)}
                      alt={`preview-${index}`}
                      className="w-32 h-32 object-cover rounded-md"
                    />
                    <button
                      type="button"
                      className="absolute top-1 right-1 bg-white text-red-500 rounded-full p-1 text-lg"
                      onClick={() => handleRemoveFile(index)}
                    >
                      &times;
                    </button>
                  </div>
                ))}
              </div>
            </div>
          </div>
        )}
        <button
          type="button"
          onClick={handleImageSave}
          className={`mt-4 w-full block px-5 py-3 rounded-lg text-white transition-duration-300 ease-in-out shadow-lg hover:shadow-xl bg-blue-400 focus:outline-none focus:ring-2 focus:ring-blue-400 `}
        >
          Upload Images
        </button>

        {/* Existing Images */}
        <div className="mt-6">
          <h3 className="text-lg font-semibold mb-4">Existing Images:</h3>
          <div className="flex flex-wrap gap-4 max-h-60 overflow-auto">
            {images.map((image) => (
              <div key={image.id} className="relative">
                <img
                  src={image.imageUrl}
                  alt={`image-${image.id}`}
                  className="w-32 h-32 object-cover rounded-md"
                />
                <button
                  type="button"
                  className="absolute top-1 right-1 bg-white text-red-500 rounded-full p-1 text-lg"
                  onClick={() => handleDeleteImage(image.id)}
                >
                  &times;
                </button>
              </div>
            ))}
          </div>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
          {/* Form Data Section */}
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium">Name:</label>
              <input
                type="text"
                value={name}
                onChange={(event) => setName(event.target.value)}
                className="mt-1 block w-full px-3 py-2 border rounded-md"
              />
            </div>
          </div>
          <div>
            <label className="block text-sm font-medium">Brand:</label>
            <select
              value={brandId}
              onChange={(event) => setBrandId(event.target.value)}
              className="mt-1 block w-full px-3 py-2 border rounded-md"
            >
              <option value="">Select a brand</option>
              {brands.map((brand) => (
                <option key={brand.id} value={brand.id}>
                  {brand.name}
                </option>
              ))}
            </select>
          </div>
          <div>
            <label className="block text-sm font-medium">URL Slug:</label>
            <input
              type="text"
              value={urlSlug}
              onChange={(event) => setUrlSlug(event.target.value)}
              className="mt-1 block w-full px-3 py-2 border rounded-md"
              readOnly
            />
          </div>
          <div>
            <label className="block text-sm font-medium">Category:</label>
            <select
              value={categoryId}
              onChange={(event) => setCategoryId(event.target.value)}
              className="mt-1 block w-full px-3 py-2 border rounded-md"
            >
              <option value="">Select a category</option>
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>
          <div>
            <label className="block text-sm font-medium">Price:</label>
            <input
              type="number"
              value={price}
              onChange={(event) => setPrice(event.target.value)}
              className="mt-1 block w-full px-3 py-2 border rounded-md"
            />
          </div>

          <div>
            <label className="block text-sm font-medium">OldPrice:</label>
            <input
              type="number"
              value={oldPrice}
              onChange={(event) => setOldPrice(event.target.value)}
              className="mt-1 block w-full px-3 py-2 border rounded-md"
            />
          </div>
          <div>
            <label className="block text-sm font-medium">Description:</label>
            <textarea
              value={description}
              onChange={(event) => setDescription(event.target.value)}
              className="mt-1 block w-full px-3 py-2 border min-h-10 rounded-md"
            />
          </div>
        </div>
        <div className="flex justify-end mt-6 text-right">
          {/* isStock */}
          <div className="flex px-5 ">
            <label className="inline-flex items-center cursor-pointer">
              <input
                type="checkbox"
                checked={isStock}
                onChange={(event) => setIsStock(event.target.checked)}
                className="sr-only"
              />
              <span className="relative">
                <span className="block w-10 h-6 bg-gray-300 rounded-full shadow-inner"></span>
                <span
                  className={`absolute block w-4 h-4 mt-1 ml-1 rounded-full shadow inset-y-0 left-0 transform transition-transform ${isStock ? "bg-green-600 translate-x-full" : "bg-white"
                    }`}
                ></span>
              </span>
              <span className="ml-3 text-sm font-medium text-gray-700">
                IsStock
              </span>
            </label>
          </div>
          <button
            type="button"
            onClick={onClose}
            className="px-4 py-2 bg-gray-300 text-gray-800 rounded-md mr-4"
          >
            Close
          </button>
          <button
            onClick={handleProductSave}
            className="px-4 py-2 bg-blue-500 text-white rounded-md"
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default UpdateProductForm;
