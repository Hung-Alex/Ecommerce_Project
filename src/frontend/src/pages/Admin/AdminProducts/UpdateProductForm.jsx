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
  const [files, setFiles] = useState([]);
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
        }
      }));
      console.log('Variants updated successfully');
    } catch (error) {
      console.error('Error updating variants:', error);
    }
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
      const formData = new FormData();
      files.forEach(file => formData.append('File', file));
      formData.append('productId', productId);

      await axios.post('/products/add-image', formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      });

      // Refresh images after upload
      const response = await axios.get(`/products/${productId}`);
      setImages(response.data.data.images);
      setFiles([]);
    } catch (error) {
      console.error('Error uploading images:', error);
    }
  };

  const handleDeleteImage = async (imageId) => {
    try {
      await axios.delete(`/images/${imageId}`);
      // Refresh images after deletion
      const response = await axios.get(`/products/${productId}`);
      setImages(response.data.data.images);
    } catch (error) {
      console.error('Error deleting image:', error);
    }
  };

  const addVariant = () => {
    setVariants([...variants, { productId, name: '', description: '' }]);
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
                  <option value="">Select Brand</option>
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
                  <option value="">Select Category</option>
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
          <div className="mt-6 md:mt-0 max-h-[60vh] overflow-auto">
            <h3 className="text-lg font-semibold mb-4">Variants:</h3>
            {variants.map((variant, index) => (
              <div key={index} className="border border-gray-300 p-4 rounded-md mb-4">
                <div>
                  <label className="block text-sm font-medium">Name:</label>
                  <input
                    type="text"
                    value={variant.name}
                    onChange={(e) => updateVariant(index, 'name', e.target.value)}
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium">Description:</label>
                  <textarea
                    value={variant.description}
                    onChange={(e) => updateVariant(index, 'description', e.target.value)}
                    className="mt-1 block w-full px-3 py-2 border rounded-md"
                  />
                </div>
                <button
                  type="button"
                  onClick={() => removeVariant(index)}
                  className="mt-2 bg-red-500 text-white px-4 py-2 rounded-md"
                >
                  Remove Variant
                </button>
              </div>
            ))}
            <button
              type="button"
              onClick={addVariant}
              className="bg-blue-500 text-white px-4 py-2 rounded-md"
            >
              Add Variant
            </button>
          </div>
        </div>

        {/* Actions */}
        <div className="mt-6 flex justify-end gap-4">
          <button
            type="button"
            onClick={() => onClose()}
            className="bg-gray-300 text-gray-800 px-4 py-2 rounded-md"
          >
            Cancel
          </button>
          <button
            type="button"
            onClick={async () => {
              await handleProductSave();
              await handleVariantSave();
              onClose();
            }}
            className="bg-blue-500 text-white px-4 py-2 rounded-md"
          >
            Save Changes
          </button>
        </div>
      </div>
    </div>

  );
};

export default UpdateProductForm;
