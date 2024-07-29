import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddUserForm from "./AddUserForm";
import toast from "react-hot-toast";
import {
    fetchUsersData,
    createUser ,
    updateUser ,
    deleteUser
} from '../../../api';
import UpdateUserForm from "./UpdateUserForm.jsx";

const AdminUsers = () => {
    const [users, setUsers] = useState([]);
    const [showForm, setShowForm] = useState(false);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [editingUser, setEditingUser] = useState(null);

    const fetchUsers = useCallback(async () => {
            const data = await fetchUsersData();
            setUsers(data.data);
    }, []);

    useEffect(() => {
        fetchUsers();
    }, [fetchUsers, showUpdateForm, showForm]);

    const addUser = async (user) => {
        const res = await createUser(user);
        return res;
        await fetchUsers();
    };

    const handleUpdateUser = async (id, updatedUser) => {
            const res = await updateUser(id, updatedUser);
            return res;
            fetchUsers();
    };

    const handleDeleteUser = async (id) => {
        try {
            await deleteUser(id);
            setUsers((prevList) => prevList.filter((user) => user.id !== id));
        } catch (error) {
            toast.error(`Error deleting user: ${error.message}`);
        }
    };

    const handleEdit = useCallback((row) => {
        setEditingUser(row);
        setShowUpdateForm(true);
    }, []);

    const handleAddUser = useCallback(() => {
        setEditingUser(null);
        setShowForm(true);
    }, []);

    const handleCloseForm = useCallback(() => {
        setShowForm(false);
        setShowUpdateForm(false);
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
                        { header: 'City', accessor: 'city' },
                        { header: 'Region', accessor: 'region' },
                        { header: 'Country', accessor: 'country' },
                    ]}
                    data={users}
                    onEdit={handleEdit}
                    onAdd={handleAddUser}
                />
                {showForm && (
                    <AddUserForm
                        onClose={handleCloseForm}
                        addUser={addUser}
                    />
                )}
                {showUpdateForm && (
                    <UpdateUserForm
                        userId={editingUser.id}
                        onClose={handleCloseForm}
                        updateUserData={handleUpdateUser}
                    />
                )}
            </div>
        </DashboardLayout>
    );
};

export default AdminUsers;
