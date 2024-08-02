import React, { useState, useRef, useEffect } from "react";
import { updateSlide, createSlide, fetchSlideDataById } from '../../../api';

const AddSlideForm = ({ slideId, onClose }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [isActive, setIsActive] = useState(false);
  const [image, setImage] = useState(null);
  const [imagePreview, setImagePreview] = useState(null);

  const fileInputRef = useRef(null);

  useEffect(() => {
    const fetchData = async () => {
      if (slideId) {
        const fetchedSlide = await fetchSlideDataById(slideId);
        if (fetchedSlide) {
          setTitle(fetchedSlide.title);
          setDescription(fetchedSlide.description);
          setIsActive(fetchedSlide.isActive);
          setImagePreview(fetchedSlide.image);
        }
      }
    };

    fetchData();
  }, [slideId]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const formData = new FormData();
    formData.append("title", title);
    formData.append("description", description);
    formData.append("isActive", isActive);
    if (image) formData.append("image", image);

    if (slideId) {
      formData.append("id", slideId);
      const res = await updateSlide(slideId, formData);
      if (res?.isSuccess) {
        onClose();
      }
    } else {
      const res = await createSlide(formData);
      if (res?.isSuccess) {
        onClose();
      }
    }
  };

  const handleImageChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      setImage(file);
      setImagePreview(URL.createObjectURL(file));
    }
  };
  
  const handleImageClick = () => {
    fileInputRef.current.click();
  };
  
  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-3xl w-full ">
      <h1 className="text-2xl mb-4">{slideId ? "Edit Slide" : "Add Slide"}</h1>
      <div className="md:flex">

        <div className="flex-shrink-0 md:w-1/4 mr-6 ">
          <div
            className="relative w-full h-64 bg-gray-100 rounded-lg overflow-hidden cursor-pointer "
            onClick={handleImageClick}
            >
            {imagePreview ? (
              <img
              src={imagePreview}
              alt="Image preview"
              className="object-contain w-full h-full "
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

        <div className="md:w-3/4">
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
              <label className="block text-sm font-medium">Active:</label>
              <input
                type="checkbox"
                checked={isActive}
                onChange={(event) => setIsActive(event.target.checked)}
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
    </div>
  );
};

export default AddSlideForm;
