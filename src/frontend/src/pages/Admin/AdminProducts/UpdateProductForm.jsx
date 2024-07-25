import React, { useState, useEffect, useRef } from 'react';
import axios from '../../../utils/axios';
import { useCategoryContext } from '../../../context/CategoryContext';
import { useBrandContext } from '../../../context/BrandContext';

const UpdateProductForm = ({ productId, onClose }) => {
  const { categories } = useCategoryContext();
  const { brands } = useBrandContext();

  const [productData, setProductData] = useState(null);
  const [name, setName] = useState('');
  const [urlSlug, setUrlSlug] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [discount, setDiscount] = useState('');
  const [brandId, setBrandId] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [images, setImages] = useState([]);
  const [imagePreviews, setImagePreviews] = useState([]);
  const [variants, setVariants] = useState([]);

  const fileInputRef = useRef(null);

  useEffect(() => {
    const fetchProductData = async () => {
      try {
        const response = await axios.get(`/products/${productId}`);
        const data = response.data.data;

        setProductData(data);
        setName(data.name);
        setUrlSlug(data.urlSlug);
        setDescription(data.description);
        setPrice(data.price);
        setDiscount(data.discount);
        setBrandId(data.brandId);
        setCategoryId(data.categoryId);
        setImages(data.images);
        setVariants(data.variants || []);
        setImagePreviews(data.images.map((img) => img.imageUrl)); // Assuming images are URLs
      } catch (error) {
        console.error('Error fetching product data:', error);
      }
    };

    fetchProductData();
  }, [productId]);

  const handleProductSave = async () => {
    try {
      await axios.put(`/products/${productId}`, {
        id: productId,
        name,
        urlSlug,
        description,
        price,
        discount,
        brandId,
        categoryId
      });
      console.log('Product details updated successfully');
    } catch (error) {
      console.error('Error updating product details:', error);
    }
  };

  const handleImageSave = async () => {
    const formData = new FormData();
    images.forEach((file) => formData.append('images', file));

    try {
      await axios.put(`/products/${productId}/images`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      console.log('Images updated successfully');
    } catch (error) {
      console.error('Error updating images:', error);
    }
  };

  const handleVariantSave = async () => {
    try {
      await Promise.all(variants.map(async (variant) => {
        if (variant.id) {
            await axios.put(`/products/${productId}/${variant.id}`, {
                productId,
                variantsId: variant.id,
                name: variant.name,
                description: variant.description,
              });
        } else {
          await axios.post(`/products/add-variant`, variant);
          await fetchProductData();
        }
      }));
      console.log('Variants updated successfully');
    } catch (error) {
      console.error('Error updating variants:', error);
    }
  };

  const handleImageChange = (event) => {
    const files = Array.from(event.target.files);
    const newImages = [...images, ...files];
    const imageUrls = newImages.map((file) => URL.createObjectURL(file));
    setImages(newImages);
    setImagePreviews(imageUrls);
    event.target.value = null;
  };

  const removeImage = (index) => {
    const updatedImages = images.filter((_, i) => i !== index);
    const updatedPreviews = imagePreviews.filter((_, i) => i !== index);

    setImages(updatedImages);
    setImagePreviews(updatedPreviews);

    if (fileInputRef.current) {
      fileInputRef.current.value = null;
    }
  };

  const addVariant = () => {
    setVariants([...variants, {productId, name: '', description: '' }]);
  };

  const removeVariant = async (index) => {
    const variant = variants[index];
    if (variant.id) {
      try {
        await axios.delete(`/products/${productId}/${variant.id}`);
      } catch (error) {
        console.error('Error deleting variant:', error);
      }
    }
    setVariants(variants.filter((_, i) => i !== index));
  };

  const updateVariant = (index, key, value) => {
    setVariants(variants.map((variant, i) =>
      i === index ? { ...variant, [key]: value } : variant
    ));
  };

  if (!productData) {
    return <div>Loading...</div>;
  }

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-4xl w-full">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* Image Section */}
          <div className="space-y-4">
            <div className="w-full bg-gray-100 rounded-md h-64 flex items-center justify-center">
              {imagePreviews.length > 0 ? (
                <div className="grid grid-cols-4 sm:grid-cols-3 md:grid-cols-8">
                  {imagePreviews.map((preview, index) => (
                    <div key={index} className="relative w-full h-64 bg-gray-200 rounded-md flex items-center justify-center">
                      <img src={preview} alt={`Preview ${index}`} className="w-full h-auto object-cover rounded-md" />
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
            <button
              type="button"
              onClick={handleImageSave}
              className="bg-blue-500 text-white rounded-md px-4 py-2"
            >
              Save Images
            </button>
          </div>

          {/* Product Section */}
          <div className="space-y-4">
            <h2 className="text-2xl mb-4">Edit Product</h2>
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
            <button
              type="button"
              onClick={handleProductSave}
              className="bg-blue-500 text-white rounded-md px-4 py-2"
            >
              Save Product
            </button>
          </div>

          {/* Variant Section */}
          <div className="space-y-4">
            <h2 className="text-2xl mb-4">Edit Variants</h2>
            {variants.map((variant, index) => (
              <div key={index} className="flex items-center space-x-4">
                <div className="flex-1">
                  <input
                    type="text"
                    value={variant.name}
                    onChange={(event) => updateVariant(index, 'name', event.target.value)}
                    placeholder="Variant Name"
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
                  <input
                    type="text"
                    value={variant.description}
                    onChange={(event) => updateVariant(index, 'description', event.target.value)}
                    placeholder="Description"
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
                </div>
                <button
                  type="button"
                  onClick={() => removeVariant(index)}
                  className="bg-red-500 text-white rounded-md px-4 py-2"
                >
                  Remove
                </button>
              </div>
            ))}
            <button
              type="button"
              onClick={addVariant}
              className="bg-blue-500 text-white rounded-md px-4 py-2"
            >
              Add Variant
            </button>
            <button
              type="button"
              onClick={handleVariantSave}
              className="bg-blue-500 text-white rounded-md px-4 py-2"
            >
              Save Variants
            </button>
          </div>
        </div>

        <div className="mt-6 flex justify-end">
          <button
            type="button"
            onClick={onClose}
            className="bg-gray-500 text-white rounded-md px-4 py-2 mr-2"
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default UpdateProductForm;
