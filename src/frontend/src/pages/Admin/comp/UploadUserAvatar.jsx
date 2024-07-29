import React, { useRef, useState } from 'react';
import { UploadUserAvatar as UploadUserAvatarapi } from '../../../api';

const UploadUserAvatar = ({ userId }) => {
    const [previewUrl, setPreviewUrl] = useState(null);
    const fileInputRef = useRef(null);

    const handleFileChange = () => {
        const file = fileInputRef.current.files[0];
        if (file) {
            const fileUrl = URL.createObjectURL(file);
            setPreviewUrl(fileUrl);
        }
    };

    const handleUpload = async (event) => {
        event.preventDefault();
        const file = fileInputRef.current.files[0];

        if (file) {
            try {
                await UploadUserAvatarapi(userId, file);
                // Handle successful upload, e.g., show a success message or update the UI
            } catch (error) {
                console.error('Error uploading avatar:', error);
                // Handle error, e.g., show an error message
            }
        } else {
            // Handle case where no file is selected
        }
    };

    return (
        <div>
            <div className="p-3 border-t mt-3">
                <form onSubmit={handleUpload}>
                    <label htmlFor="photo">Your Profile Photo</label>
                    <img
                        className="mt-2 rounded-full bg-slate-200 w-32 h-32 object-cover"
                        src={previewUrl || '/default-avatar.png'} // Default avatar if none selected
                        alt="User Avatar"
                    />
                    <div className="mt-4">
                        <input
                            type="file"
                            id="photo"
                            ref={fileInputRef}
                            // className="bg-[#7EB693] ml-4 px-4 py-1 rounded text-white cursor-pointer"
                            onChange={handleFileChange}
                        />
                        <input
                            className="bg-[#7EB693] ml-4 px-4 py-1 rounded text-white cursor-pointer"
                            type="submit"
                            value="Save"
                        />
                    </div>
                </form>
            </div>
        </div>
    );
};

export default UploadUserAvatar;
