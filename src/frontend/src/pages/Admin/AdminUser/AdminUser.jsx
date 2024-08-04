import React, { useState, useCallback } from "react";
import Table from "../comp/Table.jsx";
import AddUserForm from "./AddUserForm";
import UpdateUserForm from "./UpdateUserForm";
import toast from "react-hot-toast";
import {
    fetchUsersData,
    createUser,
    updateUser,
    deleteUser
} from '../../../api';

const AdminUsers = () => {
    const [showForm, setShowForm] = useState(false);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [refresh, setRefresh] = useState(false); // State to trigger refresh
    const [editingUser, setEditingUser] = useState(null);

    const handleAddUser = useCallback(() => {
        setEditingUser(null);
        setShowForm(true);
    }, []);

    const handleEdit = useCallback((row) => {
        setEditingUser(row);
        setShowUpdateForm(true);
    }, []);

    const handleDeleteUser = useCallback(async (row) => {
        try {
            await deleteUser(row.id);
            setRefresh(prev => !prev); // Trigger refresh
        } catch (error) {
            toast.error(`Error deleting user: ${error.message}`);
        }
    }, []);

    const handleCloseForm = useCallback(() => {
        setShowForm(false);
        setShowUpdateForm(false);
        setEditingUser(null);
        setRefresh(prev => !prev); // Trigger refresh
    }, []);

    return (
        <div className='md:p-6'>
            <Table
                apiUrl="/users"
                columns={[
                    { header: 'ID', accessor: 'id' },
                    { header: 'Avatar', accessor: 'avatarImage' },
                    { header: 'First Name', accessor: 'firstName' },
                    { header: 'Last Name', accessor: 'lastName' },
                    { header: 'City', accessor: 'city' },
                    { header: 'Region', accessor: 'region' },
                    { header: 'Country', accessor: 'country' },
                ]}
                onEdit={handleEdit}
                onAdd={handleAddUser}
                searchParam="Name"
                refresh={refresh} // Pass refresh state to Table component
            />
            {showForm && (
                <AddUserForm onClose={handleCloseForm} />
            )}
            {showUpdateForm && (
                <UpdateUserForm
                    userId={editingUser.id}
                    onClose={handleCloseForm}
                    updateUserData={async (updatedUser) => {
                        try {
                            await updateUser(editingUser.id, updatedUser);
                            handleCloseForm();
                        } catch (error) {
                            toast.error(`Error updating user: ${error.message}`);
                        }
                    }}
                />
            )}
        </div>
    );
};

export default AdminUsers;
