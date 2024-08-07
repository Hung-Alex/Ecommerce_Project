import React, { useState, useEffect } from 'react';
import axios from '../../../utils/axios';
import {
    fetchRolesData,
    fetchUsersId,
    updateUser,
    deleteUser
} from '../../../api';
import UploadUserAvatar from '../comp/UploadUserAvatar';

const UpdateUserForm = ({ userId, updateUserData, onClose }) => {
    const [userName, setUserName] = useState('');
    const [email, setEmail] = useState('');
    const [currentAvatar, setCurrentAvatar] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [region, setRegion] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [city, setCity] = useState('');
    const [country, setCountry] = useState('');
    const [isLocked, setIsLocked] = useState(false);
    const [roleOptions, setRoleOptions] = useState([]);
    const [roles, setRoles] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const response = await fetchUsersId(userId);
                const userData = response.data;
                setUserName(userData.userName);
                setEmail(userData.email);
                setCurrentAvatar(userData.avatarImage)
                setPhoneNumber(userData.phoneNumber);
                setRegion(userData.region);
                setFirstName(userData.firstName);
                setLastName(userData.lastName);
                setCity(userData.city);
                setCountry(userData.country);
                setIsLocked(userData.isLocked); // Assuming 'isLocked' is used for Locked status
                setRoles(userData.roles.map(role => role.id)); // Map to array of role IDs
            } catch (err) {
                console.error('Error fetching user data:', err);
                setError('Failed to fetch user data.');
            }
        };

        const fetchRoles = async () => {
            try {
                const response = await fetchRolesData();
                setRoleOptions(response.data); // Ensure this is an array
            } catch (err) {
                console.error('Error fetching roles:', err);
                setError('Failed to fetch roles.');
            }
        };

        fetchUserData();
        fetchRoles();
    }, [userId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();

        // Append each field to the FormData object
        formData.append('phoneNumber', phoneNumber);
        formData.append('region', region);
        formData.append('firstName', firstName);
        formData.append('lastName', lastName);
        formData.append('city', city);
        formData.append('country', country);
        formData.append('isLocked', isLocked ? true : false);
        formData.append('userId', userId);

        // Convert the roles array to a JSON string and append it
        // formData.append('roles', JSON.stringify(roles));
        roles.forEach((roleId, index) => {
            formData.append(`roles[${index}]`, roleId);
        });

        const res = await updateUser(userId, formData);
        if (res.isSuccess === true) {
            onClose();
        }

    };

    const addRole = () => {
        setRoles((prevRoles) => [...prevRoles, '']); // Add an empty string initially
    };

    const removeRole = (index) => {
        setRoles((prevRoles) => prevRoles.filter((_, i) => i !== index));
    };

    const updateRole = (index, roleId) => {
        setRoles((prevRoles) =>
            prevRoles.map((role, i) => (i === index ? roleId : role))
        );
    };

    return (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 flex items-center justify-center z-50">
            <div className="bg-white border-t-2 border-[#7eb693] shadow-md rounded max-w-3xl w-full max-h-[85vh] overflow-auto">
                <div className='flex'>                <h3 className="text-xl p-3">Personal Information of</h3>
                    <h3 className="text-xl py-3 font-semibold">{userName}</h3></div>
                <UploadUserAvatar userId={userId} currentAvatar={currentAvatar} />
                {/* User section */}
                <form onSubmit={handleSubmit}>
                    <div className="p-3 grid grid-cols-1 md:grid-cols-3 gap-4">
                        {/* User Information (2/3 width) */}
                        <div className="col-span-2">
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <span className="mb-6 block">
                                    <label className="block mb-2" htmlFor="firstName">First Name</label>
                                    <input
                                        className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                        type="text"
                                        id="firstName"
                                        value={firstName}
                                        onChange={(e) => setFirstName(e.target.value)}
                                    />
                                </span>
                                <span className="mb-6 block">
                                    <label className="block mb-2" htmlFor="lastName">Last Name</label>
                                    <input
                                        className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                        type="text"
                                        id="lastName"
                                        value={lastName}
                                        onChange={(e) => setLastName(e.target.value)}
                                    />
                                </span>
                            </div>
                            <span>
                                <label className="block mb-2 w-full text-xl border-b pb-2" htmlFor="address">Address</label>
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-4 pb-6 w-full">
                                    <span>
                                        <label htmlFor="city">City</label>
                                        <input
                                            className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            type="text"
                                            id="city"
                                            value={city}
                                            onChange={(e) => setCity(e.target.value)}
                                        />
                                    </span>
                                    <span>
                                        <label htmlFor="region">Region</label>
                                        <input
                                            className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            type="text"
                                            id="region"
                                            value={region}
                                            onChange={(e) => setRegion(e.target.value)}
                                        />
                                    </span>
                                </div>
                            </span>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 pb-6 w-full">
                                <span>
                                    <label htmlFor="country">Country</label>
                                    <input
                                        className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                        type="text"
                                        id="country"
                                        value={country}
                                        onChange={(e) => setCountry(e.target.value)}
                                    />
                                </span>
                                <span>
                                    <label htmlFor="phoneNumber">Mobile Number</label>
                                    <input
                                        className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                        type="text"
                                        id="phoneNumber"
                                        value={phoneNumber}
                                        onChange={(e) => setPhoneNumber(e.target.value)}
                                    />
                                </span>
                            </div>
                        </div>

                        {/* Roles Section (1/3 width) */}
                        <div className="col-span-1 border-l pl-4">
                            <label className="block mb-2">Roles:</label>
                            <div className='max-h-[30vh] overflow-auto'>
                                <div>
                                    {roles.map((roleId, index) => (
                                        <div key={index} className="mb-4">
                                            <label className="block mb-2">Role:</label>
                                            <select
                                                value={roleId}
                                                onChange={(e) => updateRole(index, e.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-2 rounded-md text-sm"
                                            >
                                                <option value="">Select Role</option>
                                                {roleOptions.length > 0 ? (
                                                    roleOptions.map((role) => (
                                                        <option key={role.id} value={role.id}>
                                                            {role.name}
                                                        </option>
                                                    ))
                                                ) : (
                                                    <option value="">No roles available</option>
                                                )}
                                            </select>
                                            <button
                                                type="button"
                                                onClick={() => removeRole(index)}
                                                className="bg-red-500 text-white px-3 py-2 rounded-md hover:bg-red-600 mt-2"
                                            >
                                                Remove
                                            </button>
                                        </div>
                                    ))}
                                    <button
                                        type="button"
                                        onClick={addRole}
                                        className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600"
                                    >
                                        Add Role
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="flex justify-end m-5">
                        {/* isLocked Checkbox */}
                        <div className="flex justify-end mx-5">
                            <label className="inline-flex items-center cursor-pointer">
                                <input
                                    type="checkbox"
                                    checked={isLocked}
                                    onChange={(event) => setIsLocked(event.target.checked)}
                                    className="form-checkbox hidden"
                                />
                            </label>
                        </div>
                        <button
                            type="submit"
                            className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
                        >
                            Update User
                        </button>
                        <button
                            type="button"
                            onClick={onClose}
                            className="bg-gray-300 text-black px-4 py-2 rounded-md hover:bg-gray-400 ml-2"
                        >
                            Cancel
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default UpdateUserForm;
