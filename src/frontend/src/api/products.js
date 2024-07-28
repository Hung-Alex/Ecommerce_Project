import axiosInstance from "../utils/axios.js";// Import your axios instance

// Fetch product data
export const fetchProductsData = async (SortColoumn, SortBy) => {
  const response = await axiosInstance.get(`/products?SortColoumn=${SortColoumn}&SortBy=${SortBy}`);
  return response.data;
};

// Delete an Product by ID
export const deleteProductId = async (id) => {
  await axiosInstance.delete(`/products/${id}`);
};

/**
 * Create a new product.
 * @param {Object} formData - The product data including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const createProductData = async (formData) => {
  try {
    const response = await axiosInstance.post('/products', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error creating product:', error);
    throw error;
  }
};

/**
 * Update an existing product.
 * @param {number} productId - The ID of the product to update.
 * @param {Object} formData - The updated product data including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const updateProductData = async (productId, formData) => {
  try {
    const response = await axiosInstance.put(`/products/${productId}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error updating product:', error);
    throw error;
  }
};

// Fetch product data by ID
export const fetchProductData = async (productId) => {
  const response = await axiosInstance.get(`/products/${productId}`);
  return response.data.data;
};

// Update product details
export const updateProduct = async (productId, productData) => {
  await axiosInstance.put(`/products/${productId}`, productData);
};

// Add or update product variants
export const saveVariants = async (productId, variants) => {
  return Promise.all(variants.map(async (variant) => {
    if (variant.id) {
      await axiosInstance.put(`/products/${productId}/${variant.id}`, variant);
    } else {
      await axiosInstance.post(`/products/add-variant`, variant);
    }
  }));
};

// Upload product images
export const uploadImages = async (productId, files) => {
  const formData = new FormData();
  files.forEach(file => formData.append('File', file));
  formData.append('productId', productId);

  await axiosInstance.post('/products/add-image', formData, {
    headers: { 'Content-Type': 'multipart/form-data' },
  });
};


// Delete an image by ID
export const deleteImage = async (imageId) => {
  await axiosInstance.delete(`/images/${imageId}`);
};

// Fetch updated images after uploading or deleting
export const fetchUpdatedImages = async (productId) => {
  const response = await axiosInstance.get(`/products/${productId}`);
  return response.data.data.images;
};

// Delete a variant by ID
export const deleteVariant = async (productId, variantId) => {
  await axiosInstance.delete(`/products/${productId}/${variantId}`);
};
