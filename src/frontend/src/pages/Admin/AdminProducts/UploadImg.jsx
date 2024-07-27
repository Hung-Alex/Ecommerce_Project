import React, { useState, useRef } from 'react';

const ImageUpload = ({ objId, dataimg, onAdd, onUpload }) => {
  const [files, setFiles] = useState([]);
  const [uploading, setUploading] = useState(false);
  const [error, setError] = useState(null);
  const fileInputRef = useRef(null);
  const dropzoneRef = useRef(null);

  const handleFileChange = (e) => {
    const selectedFiles = Array.from(e.target.files);
    setFiles(prevFiles => [...prevFiles, ...selectedFiles]);

    // Reset file input value
    e.target.value = null;

    if (!objId) {
      onAdd(selectedFiles); // Notify parent component with selected files if no objId
    }
  };

  const handleRemoveFile = async (index) => {
    const file = files[index];
    setFiles(prevFiles => prevFiles.filter((_, i) => i !== index));

    if (objId) {
      try {
        await axios.delete(`/images/${file.name}`, {
          headers: {
            'Content-Type': 'application/json',
          },
        });
      } catch (err) {
        setError('Failed to delete image.');
      }
    }
  };

  const handleDrop = (e) => {
    e.preventDefault();
    e.stopPropagation();
    const droppedFiles = Array.from(e.dataTransfer.files);
    setFiles(prevFiles => [...prevFiles, ...droppedFiles]);

    if (!objId) {
      onAdd(droppedFiles); // Notify parent component with dropped files if no objId
    }
  };

  const handleDragOver = (e) => {
    e.preventDefault();
    e.stopPropagation();
    dropzoneRef.current.classList.add('border-blue-500');
  };

  const handleDragLeave = () => {
    dropzoneRef.current.classList.remove('border-blue-500');
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (files.length === 0) {
      setError('Please select at least one file to upload.');
      return;
    }

    if (objId && onUpload) {
      setUploading(true);
      try {
        await onUpload(files); // Use onUpload to handle the file upload
        setFiles([]);
        setError(null);
      } catch (err) {
        setError('Failed to upload images.');
      } finally {
        setUploading(false);
      }
    } else {
      onAdd(files); // Notify parent component with files if no objId
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-4">
      <form onSubmit={handleSubmit} className="bg-white p-6 rounded-lg shadow-md">
        <div
          ref={dropzoneRef}
          className="border-2 border-dashed border-gray-300 rounded-lg p-6 text-center cursor-pointer transition-colors duration-200"
          onDrop={handleDrop}
          onDragOver={handleDragOver}
          onDragLeave={handleDragLeave}
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

        {error && <p className="text-red-500 mt-4">{error}</p>}

        {objId && (
          <button
            type="submit"
            disabled={uploading}
            className={`mt-6 w-full py-2 px-4 rounded-md text-white ${uploading ? 'bg-gray-400 cursor-not-allowed' : 'bg-blue-500 hover:bg-blue-600'} transition-colors duration-200`}
          >
            {uploading ? 'Uploading...' : 'Upload Images'}
          </button>
        )}
      </form>
    </div>
  );
};

export default ImageUpload;
