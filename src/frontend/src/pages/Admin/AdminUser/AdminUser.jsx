import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddUserForm from "./AddUserForm"; // Assuming you have a form component for users
import toast from "react-hot-toast";
import {
    fetchUsersData,
    createUser,
    updateUser,
    deleteUser
} from '../../../api/index';

const AdminUsers = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [showForm, setShowForm] = useState(false);
    const [editingUser, setEditingUser] = useState(null);

    const fetchUsers = useCallback(async () => {
        try {
            const data = await fetchUsersData();
            setUsers(data);
            setLoading(false);
        } catch (error) {
            setError(error);
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        fetchUsers();
    }, [fetchUsers]);

    const addUser = async (user) => {
        await createUser(user);
        fetchUsers();
    };

    const updateUser = async (id, updatedUser) => {
        await updateUser(id, updatedUser);
        fetchUsers();
    };

    const deleteUser = async (id) => {
        try {
            await deleteUser(id);
            setUsers((prevList) => prevList.filter((user) => user.id !== id));
        } catch (error) {
            toast.error(`Error deleting user: ${error.message}`);
        }
    };

    const handleEdit = useCallback((row) => {
        setEditingUser(row.id);
        setShowForm(true);
    }, []);

    const handleDelete = useCallback(async (row) => {
        try {
            await deleteUser(row.id);
        } catch (error) {
            toast.error(`Error deleting user: ${error.message}`);
        }
    }, []);

    const handleAddUser = useCallback(() => {
        setEditingUser(null);
        setShowForm(true);
    }, []);

    const handleCloseForm = useCallback(() => {
        setShowForm(false);
        setEditingUser(null);
    }, []);

    return (
        <DashboardLayout>
            <div className='p-6'>
                <Table
                    columns={[
                        { header: 'ID', accessor: 'id' },
                        { header: 'Avatar', accessor: 'avatarImage' },
                        { header: 'First Name', accessor: 'firstName' },
                        { header: 'Last Name', accessor: 'lastName' },
                        { header: 'city', accessor: 'city' },
                        { header: 'region', accessor: 'region' },
                        { header: 'country', accessor: 'country' },
                    ]}
                    data={users}
                    onEdit={handleEdit}
                    onDelete={handleDelete}
                    onAdd={handleAddUser}
                />
                {showForm && (
                    <AddUserForm
                        user={editingUser}
                        onClose={handleCloseForm}
                        addUser={addUser}
                        updateUser={updateUser}
                    />
                )}
            </div>
        </DashboardLayout>
    );
};

export default AdminUsers;
