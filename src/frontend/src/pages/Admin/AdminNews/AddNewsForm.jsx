import React, { useState, useRef, useEffect } from "react";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css"; // Import Quill styles
import { titleToSlug } from "../../../utils/slugify"; // Import hàm titleToSlug từ file riêng

const AddNewsForm = ({ post, onClose, addPost, updatePost }) => {
  const [title, setTitle] = useState(post ? post.title : "");
  const [urlSlug, setUrlSlug] = useState(post ? post.urlSlug : "");
  const [shortDescription, setShortDescription] = useState(post ? post.shortDescription : "");
  const [description, setDescription] = useState(post ? post.description : "");
  const [published, setPublished] = useState(post ? post.published : false); // Fixed typo
  const [image, setImage] = useState(null);
  const [imagePreview, setImagePreview] = useState(post ? post.imageUrl : null);

  const fileInputRef = useRef(null);

  useEffect(() => {
    if (post && post.imageUrl) {
      setImagePreview(post.imageUrl);
    }
  }, [post]);

  // Cập nhật URL slug khi tiêu đề thay đổi
  useEffect(() => {
    setUrlSlug(titleToSlug(title));
  }, [title]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const postData = {
      title,
      urlSlug,
      shortDescription,
      description,
      published,
      image
    };
    if (post) {
      await updatePost(post.id, postData);
    } else {
      await addPost(postData);
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
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-4xl w-full">
        <h2 className="text-2xl mb-6">{post ? "Edit Post" : "Add New Post"}</h2>
        <div className="flex flex-col lg:flex-row">
          <div className="flex-shrink-0 w-full lg:w-1/4 mb-4 lg:mb-0 lg:mr-6">
            <div
              className="relative w-full h-64 bg-gray-100 rounded-lg overflow-hidden cursor-pointer"
              onClick={handleImageClick}
            >
              {imagePreview ? (
                <img
                  src={imagePreview}
                  alt="Image preview"
                  className="object-contain w-full h-full"
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
          <div className="w-full lg:w-3/4">
            <form onSubmit={handleSubmit} className="space-y-4">
              <div className="flex items-center mt-4">
                <label className="inline-flex items-center cursor-pointer">
                  <input
                    type="checkbox"
                    checked={published}
                    onChange={(event) => setPublished(event.target.checked)}
                    className="sr-only"
                  />
                  <span className="relative">
                    <span className="block w-10 h-6 bg-gray-300 rounded-full shadow-inner"></span>
                    <span
                      className={`absolute block w-4 h-4 mt-1 ml-1 rounded-full shadow inset-y-0 left-0 transform transition-transform ${published ? 'bg-green-600 translate-x-full' : 'bg-white'
                        }`}
                    ></span>
                  </span>
                  <span className="ml-3 text-sm font-medium text-gray-700">Published</span>
                </label>
              </div>
              <div>
                <label className="block text-sm font-medium">Title:</label>
                <input
                  type="text"
                  value={title}
                  onChange={(event) => setTitle(event.target.value)}
                  className="mt-1 block w-full px-3 py-2 border rounded-md"
                  placeholder="Enter post title"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">URL Slug:</label>
                <input
                  type="text"
                  value={urlSlug}
                  onChange={(event) => setUrlSlug(event.target.value)}
                  className="mt-1 block w-full px-3 py-2 border rounded-md"
                  placeholder="Enter URL slug"
                  readOnly // Optional: Prevent user from editing the slug manually
                />
              </div>
              <div>
                <label className="block text-sm font-medium">Short Description:</label>
                <textarea
                  value={shortDescription}
                  onChange={(e) => setShortDescription(e.target.value)}
                  className="mt-1 block w-full border rounded-md p-2"
                  rows="4"
                  placeholder="Enter a brief summary of the post"
                />
              </div>
              <div>
                <label className="block text-sm font-medium">Description:</label>
                <ReactQuill
                  value={description}
                  onChange={setDescription}
                  className="mt-1 block w-full border rounded-md"
                />
              </div>
              <div className="flex justify-end space-x-4 mt-6">
                <button
                  type="button"
                  onClick={onClose}
                  className="px-4 py-2 bg-gray-300 text-gray-700 rounded-md hover:bg-gray-400"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="px-4 py-2 bg-green-600 text-white rounded-md hover:bg-green-700"
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

export default AddNewsForm;
