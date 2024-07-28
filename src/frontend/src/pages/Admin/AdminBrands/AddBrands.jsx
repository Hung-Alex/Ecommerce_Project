import React, { useState, useRef, useEffect } from "react";
import { titleToSlug } from '../../../utils/slugify';

const AddBrandForm = ({ brand, onClose, addBrand, updateBrand }) => {
  const [name, setName] = useState(brand ? brand.name : "");
  const [urlSlug, setUrlSlug] = useState(brand ? brand.urlSlug : "");
  const [description, setDescription] = useState(brand ? brand.description : "");
  const [image, setImage] = useState("");
  const [imagePreview, setImagePreview] = useState(brand ? brand.image : null);

  const fileInputRef = useRef(null);

  useEffect(() => {
    setUrlSlug(titleToSlug(name));
  }, [name]);

  useEffect(() => {
    if (brand && brand.image) {
      setImagePreview(brand.image);
    }
  }, [brand]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (brand) {
      await updateBrand(brand.id, { name, urlSlug, description, image });
    } else {
      await addBrand({ name, urlSlug, description, image });
    }
    onClose();
  };

  const handleImageChange = (event) => {
    const file = event.target.files[0];
    setImage(file);
    setImagePreview(URL.createObjectURL(file));
  };

  const handleImageClick = () => {
    fileInputRef.current.click();
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-3xl w-full flex">
        {/* Image Section */}
        <div className="flex-shrink-0 w-1/4 mr-6">
          <div
            className="relative w-full h-64 bg-gray-100 rounded-lg overflow-hidden cursor-pointer"
            onClick={handleImageClick}
          >
            {imagePreview ? (
              <img
                src={imagePreview}
                alt="Image preview"
                className="object-contain w-full h-full"
                style={{ maxWidth: "100%", maxHeight: "100%" }}
              />
            ) : (
              <div className="flex items-center justify-center h-full text-gray-500">
                No Image
              </div>
            )}
            <input
              type="file"
              ref={fileInputRef}
              onChange={handleImageChange}
              className="hidden"
            />
          </div>
        </div>

        {/* Form Section */}
        <div className="w-3/4">
          <h2 className="text-2xl mb-4">{brand ? "Edit Brand" : "Add Brand"}</h2>
          <form onSubmit={handleSubmit} className="space-y-4">
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
                readOnly
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
            <div className="flex justify-end space-x-4">
              <button
                type="button"
                onClick={onClose}
                className="px-4 py-2 bg-gray-300 text-gray-700 rounded-md hover:bg-gray-400"
              >
                Cancel
              </button>
              <button
                type="submit"
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
              >
                Save
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default AddBrandForm;
