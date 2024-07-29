import React, { useRef, useState, useEffect } from 'react';
import { UploadUserAvatar as UploadUserAvatarapi } from '../../../api';

const UploadUserAvatar = ({ userId, currentAvatar }) => {
    const [previewUrl, setPreviewUrl] = useState(currentAvatar);
    const fileInputRef = useRef(null);

    useEffect(() => {
        setPreviewUrl(currentAvatar);
    }, [currentAvatar]);

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
                // Assuming the API returns the new avatar URL
                setPreviewUrl(URL.createObjectURL(file)); // Update with new avatar URL if needed
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
        <div className="p-3 border-t mt-3">
    <form onSubmit={handleUpload} className="relative">
        <label htmlFor="photo" className="flex justify-center text-lg font-semibold">Your Profile Photo</label>
        <div className="flex justify-center relative mt-2">
            <img
                className="rounded-full bg-slate-200 w-32 h-32 object-cover"
                src={previewUrl || '/default-avatar.png'} // Default avatar if none selected
                alt="User Avatar"
            />
            <input
                type="file"
                id="photo"
                ref={fileInputRef}
                onChange={handleFileChange}
                className="absolute inset-0 opacity-0 cursor-pointer"
            />
        </div>
        <div className="flex justify-center mt-4">
            <input
                className="bg-[#7EB693] w-[50%] px-4 py-1 rounded text-white cursor-pointer"
                type="submit"
                value="Upload"
            />
        </div>
    </form>
</div>

    );
};

export default UploadUserAvatar;
