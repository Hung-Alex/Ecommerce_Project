import React, { useState } from "react";

const AddBrandForm = ({ brand, onClose, addBrand, updateBrand }) => {
  const [name, setName] = useState(brand ? brand.name : "");
  const [urlSlug, setUrlSlug] = useState(brand ? brand.urlSlug : "");
  const [description, setDescription] = useState(brand ? brand.description : "");
  const [image, setImage] = useState(brand ? brand.image : null);
  const [imagePreview, setImagePreview] = useState(null);

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
    setImage(event.target.files[0]);
    setImagePreview(URL.createObjectURL(event.target.files[0]));
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
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
            <label className="block text-sm font-medium">Image:</label>
            <input type="file" onChange={handleImageChange} className="mt-1 block w-full" />
            {imagePreview && (
              <img src={imagePreview} alt="Image preview" className="mt-2 w-24 h-24 object-cover" />
            )}
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
  );
};

export default AddBrandForm;