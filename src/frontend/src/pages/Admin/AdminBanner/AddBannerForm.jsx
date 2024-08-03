import React, { useState, useRef, useEffect } from "react";
import { updateBanner, createBanner, fetchBannerDataById } from '../../../api';

const AddBannerForm = ({ bannerId, onClose }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [visible, setVisible] = useState(false);
  const [image, setImage] = useState("");
  const [imagePreview, setImagePreview] = useState(null);

  const fileInputRef = useRef(null);

  useEffect(() => {
    const fetchData = async () => {
      if (bannerId) {
        const fetchedBanner = await fetchBannerDataById(bannerId);
        if (fetchedBanner) {
          setTitle(fetchedBanner.title);
          setDescription(fetchedBanner.description);
          setVisible(fetchedBanner.isVisible);
          setImagePreview(fetchedBanner.logoImageUrl);
        }
      }
    };

    fetchData();
  }, [bannerId]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const formData = new FormData();
    formData.append("title", title);
    formData.append("description", description);
    formData.append("Visible", visible);
    if (image) formData.append("FormFile", image);

    if (bannerId) {
      formData.append("id", bannerId);
      await updateBanner(bannerId, formData).then(res => {
        if (res?.isSuccess) {
          onClose();
        }
      });
    } else {
      await createBanner(formData).then(res => {
        if (res?.isSuccess) {
          onClose();
        }
      });
    }
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

        <div className="w-3/4">
          <h2 className="text-2xl mb-4">{bannerId ? "Edit Banner" : "Add Banner"}</h2>
          <form onSubmit={handleSubmit} className="space-y-4">
            <div>
              <label className="block text-sm font-medium">Title:</label>
              <input
                type="text"
                value={title}
                onChange={(event) => setTitle(event.target.value)}
                className="mt-1 block w-full px-3 py-2 border rounded-md"
              />
            </div>
            <div>
              <label className="block text-sm font-medium">Description:</label>
              <textarea
                value={description}
                onChange={(event) => setDescription(event.target.value)}
                className="mt-1 block max-h-56 min-h-44 w-full px-3 py-2 border rounded-md"
              />
            </div>
            <div>
              <label className="block text-sm font-medium">Visible:</label>
              <input
                type="checkbox"
                checked={visible}
                onChange={(event) => setVisible(event.target.checked)}
                className="mt-1"
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

export default AddBannerForm;
