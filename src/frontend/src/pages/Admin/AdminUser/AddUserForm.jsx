import React, { useState, useEffect } from 'react';
import { fetchRolesData, createUser } from '../../../api';

const AddUserForm = ({ user, addUser, onClose }) => {
    const [userName, setUserName] = useState(user ? user.userName : '');
    const [email, setEmail] = useState(user ? user.email : '');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [phoneNumber, setPhoneNumber] = useState(user ? user.phoneNumber : '');
    const [region, setRegion] = useState(user ? user.region : '');
    const [firstName, setFirstName] = useState(user ? user.firstName : '');
    const [lastName, setLastName] = useState(user ? user.lastName : '');
    const [city, setCity] = useState(user ? user.city : '');
    const [country, setCountry] = useState(user ? user.country : '');
    const [isActive, setIsActive] = useState(user ? user.isActive : false);
    const [roles, setRoles] = useState(user ? user.roles : []);
    const [error, setError] = useState(null);
    const [roleOptions, setRoleOptions] = useState([]);

    useEffect(() => {
        const fetchRoles = async () => {
            try {
                const fetchedRoles = await fetchRolesData();
                setRoleOptions(fetchedRoles.data);
            } catch (err) {
                setError('Failed to fetch roles.');
            }
        };

        fetchRoles();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append('userName', userName);
        formData.append('email', email);
        formData.append('password', password);
        formData.append('confirmPassword', confirmPassword);
        formData.append('phoneNumber', phoneNumber);
        formData.append('region', region);
        formData.append('firstName', firstName);
        formData.append('lastName', lastName);
        formData.append('city', city);
        formData.append('country', country);
        formData.append('isActive', isActive);
        // Append each role as a separate JSON object if roles is an array
        roles.forEach((role) => {
            formData.append('roles', (role.roleId));
        });

        const res = await createUser(formData);
        if (res?.isSuccess) {
            onClose();
        }
    };

    const addRole = () => {
        setRoles((prevRoles) => [...prevRoles, { roleId: '', roleName: '' }]);
    };

    const removeRole = (index) => {
        setRoles((prevRoles) => prevRoles.filter((_, i) => i !== index));
    };

    const updateRole = (index, roleId) => {
        const selectedRole = roleOptions.find(role => role.id === roleId);
        setRoles((prevRoles) =>
            prevRoles.map((role, i) =>
                i === index ? { ...role, roleId, roleName: selectedRole.name } : role
            )
        );
    };

    return (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
            <div className="max-h-[80vh] overflow-auto  border-t-2 border-[#7eb693] shadow-md rounded bg-white w-3/4">
                <div className="space-y-4">

                    <div className="max-w-4xl mx-auto p-4">
                        <h1 className="text-2xl font-bold text-center mb-6">Create User</h1>
                        <form onSubmit={handleSubmit} className="bg-white space-y-6">
                            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                                <div className='col-span-2'>
                                    <label
                                        className="block mb-5 text-xl border-b pb-2"
                                        htmlFor="address"
                                    >
                                        Account
                                    </label>
                                    <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                                        <div>
                                            <label className="block mb-2" htmlFor="userName">UserName:</label>
                                            <input
                                                type="text"
                                                id="userName"
                                                value={userName}
                                                onChange={(event) => setUserName(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="email">Email:</label>
                                            <input
                                                type="email"
                                                id="email"
                                                value={email}
                                                onChange={(event) => setEmail(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="password">Password:</label>
                                            <input
                                                type="password"
                                                id="password"
                                                value={password}
                                                onChange={(event) => setPassword(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="confirmPassword">Confirm Password:</label>
                                            <input
                                                type="password"
                                                id="confirmPassword"
                                                value={confirmPassword}
                                                onChange={(event) => setConfirmPassword(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                    </div>
                                    <label
                                        className="block my-5 text-xl border-b pb-2"
                                        htmlFor="address"
                                    >
                                        Infomation
                                    </label>
                                    <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                                        <div>
                                            <label className="block mb-2" htmlFor="firstName">First Name:</label>
                                            <input
                                                type="text"
                                                id="firstName"
                                                value={firstName}
                                                onChange={(event) => setFirstName(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="lastName">Last Name:</label>
                                            <input
                                                type="text"
                                                id="lastName"
                                                value={lastName}
                                                onChange={(event) => setLastName(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="phoneNumber">Phone Number:</label>
                                            <input
                                                type="text"
                                                id="phoneNumber"
                                                value={phoneNumber}
                                                onChange={(event) => setPhoneNumber(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="region">Region:</label>
                                            <input
                                                type="text"
                                                id="region"
                                                value={region}
                                                onChange={(event) => setRegion(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>

                                        <div>
                                            <label className="block mb-2" htmlFor="city">City:</label>
                                            <input
                                                type="text"
                                                id="city"
                                                value={city}
                                                onChange={(event) => setCity(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                        <div>
                                            <label className="block mb-2" htmlFor="country">Country:</label>
                                            <input
                                                type="text"
                                                id="country"
                                                value={country}
                                                onChange={(event) => setCountry(event.target.value)}
                                                className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                                            />
                                        </div>
                                    </div>

                                </div>

                                {/* Roles Section */}
                                <div className="space-y-4">
                                    <h2 className="text-2xl mb-4">Roles</h2>
                                    <div className="max-h-[45vh] overflow-auto">
                                        {roles.map((role, index) => (
                                            <div key={index} className="border p-4 rounded-md mb-4">
                                                <div>
                                                    <label className="block text-sm font-medium text-gray-700">Role:</label>
                                                    <select
                                                        value={role.roleId}
                                                        onChange={(event) => updateRole(index, event.target.value)}
                                                        className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500"
                                                    >
                                                        <option value="">Select a role</option>
                                                        {roleOptions.map((roleOption) => (
                                                            <option key={roleOption.id} value={roleOption.id}>
                                                                {roleOption.name}
                                                            </option>
                                                        ))}
                                                    </select>
                                                </div>
                                                <button
                                                    type="button"
                                                    onClick={() => removeRole(index)}
                                                    className="mt-4 text-red-500 hover:text-red-700"
                                                >
                                                    Remove Role
                                                </button>
                                            </div>
                                        ))}
                                    </div>
                                    <button
                                        type="button"
                                        onClick={addRole}
                                        className="mt-4 w-full py-2 px-4 bg-blue-500 text-white rounded-md hover:bg-blue-600"
                                    >
                                        Add Role
                                    </button>
                                </div>
                            </div>

                            <div className="mt-6 text-right">
                                <div className="">
                                    {/* isActive */}
                                    <label className="inline-flex px-4 justify-center items-center cursor-pointer">
                                        <input
                                            type="checkbox"
                                            id="isActive"
                                            checked={isActive}
                                            onChange={() => setIsActive(!isActive)}
                                            className="sr-only"
                                        />
                                        <span className="relative">
                                            <span className="block w-10 h-6 bg-gray-300 rounded-full shadow-inner"></span>
                                            <span
                                                className={`absolute block w-4 h-4 mt-1 ml-1 rounded-full shadow inset-y-0 left-0 transform transition-transform ${isActive ? "bg-green-600 translate-x-full" : "bg-white"
                                                    }`}
                                            ></span>
                                        </span>
                                        <span className="ml-3 text-sm font-medium text-gray-700">
                                            isActive
                                        </span>
                                    </label>
                                    <button
                                        type="button"
                                        onClick={onClose}
                                        className="px-4 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400"
                                    >
                                        Close
                                    </button>
                                    <button
                                        type="submit"
                                        className="ml-4 px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600"
                                    >
                                        Save
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>

    );
};

export default AddUserForm;
