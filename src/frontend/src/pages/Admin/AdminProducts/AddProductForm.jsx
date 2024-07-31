import React, { useState, useRef, useEffect } from 'react';
import { useCategoryContext } from '../../../context/CategoryContext';
import { useBrandContext } from '../../../context/BrandContext';
import { titleToSlug } from '../../../utils/slugify';
import { createProductData } from '../../../api';

const AddProductForm = ({ product, onClose }) => {
  const [files, setFiles] = useState([]);
  const [error, setError] = useState(null);
  const fileInputRef = useRef(null);

  const { categories, loading: categoriesLoading, error: categoriesError } = useCategoryContext();
  const { brands, loading: brandsLoading, error: brandsError } = useBrandContext();

  const [name, setName] = useState(product ? product.name : '');
  const [isStock, setIsStock] = useState(false);
  const [urlSlug, setUrlSlug] = useState(product ? product.urlSlug : '');
  const [description, setDescription] = useState(product ? product.description : '');
  const [price, setPrice] = useState(product ? product.price : '');
  const [oldPrice, setOldPrice] = useState(product ? product.oldPrice : '');
  const [discount, setDiscount] = useState(product ? product.discount : '');
  const [brandId, setBrandId] = useState(product ? product.brandId : '');
  const [categoryId, setCategoryId] = useState(product ? product.categoryId : '');

  useEffect(() => {
    setUrlSlug(titleToSlug(name));
  }, [name]);

  const handleFileChange = (e) => {
    const selectedFiles = Array.from(e.target.files);
    setFiles(prevFiles => [...prevFiles, ...selectedFiles]);

    // Reset file input value
    e.target.value = null;
  };

  const handleRemoveFile = (index) => {
    setFiles(prevFiles => prevFiles.filter((_, i) => i !== index));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append('name', name);
    formData.append('urlSlug', urlSlug);
    formData.append('description', description);
    formData.append('price', price);
    formData.append('oldPrice', oldPrice);
    formData.append('discount', discount);
    formData.append('brandId', brandId);
    formData.append('categoryId', categoryId);
    formData.append('isStock', isStock);

    // Append images directly
    files.forEach((file) => {
      formData.append('images', file);
    });

    createProductData(formData).then(res => {
      if (res?.isSuccess) {
        onClose();
      }
    })
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white p-6 shadow-lg block max-w-4xl w-full max-h-[90vh] overflow-auto">
        <h1 className='text-3xl'>Create Product</h1>
        {/* Image Upload Section */}
        <div className="space-y-4">
          <div className="max-w-4xl mx-auto p-4">
            <form onSubmit={handleSubmit} className="bg-white space-y-6">
              <div
                className="border-2 border-dashed border-gray-300 p-6 text-center cursor-pointer max-h-[85vh] transition-colors duration-200"
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

              {error && <p className="text-red-500 mt-4">{error}</p>}

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
                  <label className="block text-sm font-medium">Discount:</label>
                  <input
                    type="number"
                    value={discount}
                    onChange={(event) => setDiscount(event.target.value)}
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
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
                  <label className="block text-sm font-medium">Description:</label>
                  <textarea
                    value={description}
                    onChange={(event) => setDescription(event.target.value)}
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
                  type="submit"
                  className="px-4 py-2 bg-blue-500 text-white rounded-md"
                >
                  Save
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>

  );
};

export default AddProductForm;
