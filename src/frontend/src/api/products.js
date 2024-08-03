// api/products.js
import { get_api, delete_api, post_form, put_form, put_json } from './methods.js';

/**
 * Fetch product data.
 * @param {string} SortColoumn - Column to sort by.
 * @param {string} SortBy - Sorting direction.
 * @returns {Promise<Object>} - The response data.
 */
export const fetchProductsData = async (SortColoumn, SortBy) => {
  return get_api(`/products?SortColoumn=${SortColoumn}&SortBy=${SortBy}`);
};

/**
 * Delete a product by ID.
 * @param {number} id - The ID of the product to delete.
 * @returns {Promise<void>} - Resolves when the product is deleted.
 */
export const deleteProductId = async (id) => {
  return delete_api(`/products/${id}`);
};

/**
 * Create a new product.
 * @param {FormData} formData - The product data including images.
 * @returns {Promise<Object>} - The response data.
 */
export const createProductData = async (formData) => {
  return post_form('/products', formData);
};

/**
 * Update an existing product.
 * @param {number} productId - The ID of the product to update.
 * @param {FormData} formData - The updated product data including images.
 * @returns {Promise<Object>} - The response data.
 */
export const updateProductData = async (productId, formData) => {
  return put_form(`/products/${productId}`, formData);
};

/**
 * Fetch product data by ID.
 * @param {number} productId - The ID of the product.
 * @returns {Promise<Object>} - The response data.
 */
export const fetchProductData = async (productId) => {
  return get_api(`/products/${productId}`);
};

/**
 * Update product details.
 * @param {number} productId - The ID of the product to update.
 * @param {Object} productData - The product data to update.
 * @returns {Promise<void>} - Resolves when the product is updated.
 */
export const updateProduct = async (productId, productData) => {
  return put_json(`/products/${productId}`, productData);
};

/**
 * Add or update product variants.
 * @param {number} productId - The ID of the product.
 * @param {Array<Object>} variants - The variants to add or update.
 * @returns {Promise<void>} - Resolves when the variants are saved.
 */
export const saveVariants = async (productId, variants) => {
  return Promise.all(variants.map(async (variant) => {
    if (variant.id) {
      return put_json(`/products/${productId}/${variant.id}`, variant);
    } else {
      return post_json(`/products/add-variant`, variant);
    }
  }));
};

/**
 * Upload product images.
 * @param {number} productId - The ID of the product.
 * @param {Array<File>} files - The image files to upload.
 * @returns {Promise<void>} - Resolves when the images are uploaded.
 */
export const uploadImages = async (productId, files) => {
  const formData = new FormData();
  files.forEach(file => formData.append('File', file));
  formData.append('productId', productId);

  return post_form('/products/add-image', formData);
};

/**
 * Delete an image by ID.
 * @param {number} imageId - The ID of the image to delete.
 * @returns {Promise<void>} - Resolves when the image is deleted.
 */
export const deleteImage = async (imageId) => {
  return delete_api(`/images/${imageId}`);
};

/**
 * Fetch updated images after uploading or deleting.
 * @param {number} productId - The ID of the product.
 * @returns {Promise<Array>} - The updated images.
 */
export const fetchUpdatedImages = async (productId) => {
  const response = await get_api(`/products/${productId}`);
  return response.data.images;
};

/**
 * Delete a variant by ID.
 * @param {number} productId - The ID of the product.
 * @param {number} variantId - The ID of the variant to delete.
 * @returns {Promise<void>} - Resolves when the variant is deleted.
 */
export const deleteVariant = async (productId, variantId) => {
  return delete_api(`/products/${productId}/${variantId}`);
};
