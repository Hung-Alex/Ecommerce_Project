import React, { useState, useRef } from 'react';
import axios from '../../../utils/axios';
import { useCategoryContext } from '../../../context/CategoryContext';
import { useBrandContext } from '../../../context/BrandContext';

const AddProductForm = ({ product, onClose, onSuccess }) => {
  const { categories, loading: categoriesLoading, error: categoriesError } = useCategoryContext();
  const { brands, loading: brandsLoading, error: brandsError } = useBrandContext();

  const [name, setName] = useState(product ? product.name : '');
  const [urlSlug, setUrlSlug] = useState(product ? product.urlSlug : '');
  const [description, setDescription] = useState(product ? product.description : '');
  const [price, setPrice] = useState(product ? product.price : '');
  const [discount, setDiscount] = useState(product ? product.discount : '');
  const [brandId, setBrandId] = useState(product ? product.brandId : '');
  const [categoryId, setCategoryId] = useState(product ? product.categoryId : '');
  const [images, setImages] = useState(product ? product.images : []);
  const [imagePreviews, setImagePreviews] = useState([]);
  const [variants, setVariants] = useState(product ? product.variants : [{ variantName: '', description: '' }]);

  const fileInputRef = useRef(null);

  const handleSubmit = async () => {
    const formData = new FormData();
    formData.append('name', name);
    formData.append('urlSlug', urlSlug);
    formData.append('description', description);
    formData.append('price', price);
    formData.append('discount', discount);
    formData.append('brandId', brandId);
    formData.append('categoryId', categoryId);

    // Append images directly
    images.forEach((file) => {
      formData.append('images', file);
    });

    // Conditionally append variants if they are not empty
    if (variants.length > 0) {
      formData.append('variant', JSON.stringify(variants));
    }

    try {
      const response = await axios.post('/products', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      console.log('Product added:', response.data);
      onSuccess();
      onClose();
    } catch (error) {
      console.error('Error submitting product:', error);
    }
  };

  const handleImageChange = (event) => {
    const files = Array.from(event.target.files);
    const newImages = [...images, ...files];
    const imageUrls = newImages.map((file) => URL.createObjectURL(file));
    setImages(newImages);
    setImagePreviews(imageUrls);
    event.target.value = null; // Clear the input field value
  };

  const removeImage = (index) => {
    // Remove the image from the list
    const updatedImages = images.filter((_, i) => i !== index);
    const updatedPreviews = imagePreviews.filter((_, i) => i !== index);

    setImages(updatedImages);
    setImagePreviews(updatedPreviews);

    // If needed, trigger file input to allow re-selection
    if (fileInputRef.current) {
      fileInputRef.current.value = null;
    }
  };

  const addVariant = () => {
    setVariants((prevVariants) => [...prevVariants, { variantName: '', description: '' }]);
  };

  const removeVariant = (index) => {
    setVariants((prevVariants) => prevVariants.filter((_, i) => i !== index));
  };

  const updateVariant = (index, key, value) => {
    setVariants((prevVariants) =>
      prevVariants.map((variant, i) =>
        i === index ? { ...variant, [key]: value } : variant
      )
    );
  };

  if (categoriesLoading || brandsLoading) {
    return <div>Loading...</div>;
  }

  if (categoriesError || brandsError) {
    return <div>Error loading data</div>;
  }

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-4xl w-full">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* Image Section */}
          <div className="space-y-4">
            <div className="w-full bg-gray-100 rounded-md h-64 flex items-center justify-center">
              {imagePreviews.length > 0 ? (
                <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
                  {imagePreviews.map((preview, index) => (
                    <div key={index} className="relative w-full h-64 bg-gray-200 rounded-md flex items-center justify-center">
                      <img src={preview} alt={`Preview ${index}`} className="w-full h-full object-cover rounded-md" />
                      <button
                        type="button"
                        onClick={() => removeImage(index)}
                        className="absolute top-2 right-2 bg-red-500 text-white rounded-full p-1 text-sm"
                      >
                        ×
                      </button>
                    </div>
                  ))}
                </div>
              ) : (
                <p>No Images</p>
              )}
            </div>
            <input
              type="file"
              multiple
              onChange={handleImageChange}
              ref={fileInputRef}
              className="mt-1 block w-full px-3 py-2 border rounded-md"
            />
          </div>

          {/* Form Data Section */}
          <div className="space-y-4">
            <h2 className="text-2xl mb-4">{product ? 'Edit Product' : 'Add Product'}</h2>
            <form className="space-y-4">
              <div>
                <label className="block text-sm font-medium">Name:</label>
                <input
                  type="text"
                  value={name}
                  onChange={(event) => setName(event.target.value)}
                  className="mt-1 block w-full px-3 py-2 border rounded-md"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">URL Slug:</label>
                <input
                  type="text"
                  value={urlSlug}
                  onChange={(event) => setUrlSlug(event.target.value)}
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
                <label className="block text-sm font-medium">Price:</label>
                <input
                  type="number"
                  step="0.01"
                  value={price}
                  onChange={(event) => setPrice(event.target.value)}
                  className="mt-1 block w-full px-3 py-2 border rounded-md"
                />
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
                <label className="block text-sm font-medium">Brand:</label>
                <select
                  value={brandId}
                  onChange={(event) => setBrandId(event.target.value)}
                  className="mt-1 block w-full px-3 py-2 border rounded-md"
                >
                  <option value="">Select a Brand</option>
                  {brands.map((brand) => (
                    <option key={brand.id} value={brand.id}>
                      {brand.name}
                    </option>
                  ))}
                </select>
              </div>
              <div>
                <label className="block text-sm font-medium">Category:</label>
                <select
                  value={categoryId}
                  onChange={(event) => setCategoryId(event.target.value)}
                  className="mt-1 block w-full px-3 py-2 border rounded-md"
                >
                  <option value="">Select a Category</option>
                  {categories.map((category) => (
                    <option key={category.id} value={category.id}>
                      {category.name}
                    </option>
                  ))}
                </select>
              </div>
            </form>
          </div>

          {/* Variants Section */}
          <div className="space-y-4">
            <h4 className="text-xl mb-2">Variants</h4>
            {variants.map((variant, index) => (
              <div key={index} className="border p-4 rounded-md mb-4">
                <div className="flex justify-between items-center mb-2">
                  <h4 className="text-lg">Variant {index + 1}</h4>
                  <button
                    type="button"
                    onClick={() => removeVariant(index)}
                    className="bg-red-500 text-white rounded-full px-2 py-1 text-sm"
                  >
                    Remove
                  </button>
                </div>
                <div>
                  <label className="block text-sm font-medium">Name:</label>
                  <input
                    type="text"
                    value={variant.variantName}
                    onChange={(event) => updateVariant(index, 'variantName', event.target.value)}
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
                </div>
                <div className="mt-2">
                  <label className="block text-sm font-medium">Description:</label>
                  <textarea
                    value={variant.description}
                    onChange={(event) => updateVariant(index, 'description', event.target.value)}
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
                </div>
              </div>
            ))}
            <button
              type="button"
              onClick={addVariant}
              className="bg-blue-500 text-white rounded-full px-4 py-2 mt-4"
            >
              Add Variant
            </button>
          </div>
        </div>

        <div className="flex justify-end mt-6">
          <button
            type="button"
            onClick={handleSubmit}
            className="bg-blue-500 text-white rounded-full px-4 py-2"
          >
            Submit
          </button>
          <button
            type="button"
            onClick={onClose}
            className="ml-4 bg-gray-500 text-white rounded-full px-4 py-2"
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default AddProductForm;
