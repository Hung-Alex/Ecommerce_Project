import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddRoleForm from "./AddRoleForm";
import toast from "react-hot-toast"; // Import toast from react-hot-toast
import {
  fetchRolesData,
  createRoles,
  updateRoles,
  deleteRoles
} from '../../../api/index';

const AdminRoles = () => {
  const [roles, setRoles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [editingRole, setEditingRole] = useState(null);

  const fetchRoles = useCallback(async () => {
    try {
      const data = await fetchRolesData();
      setRoles(data);
      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchRoles();
  }, [fetchRoles]);

  const addRole = async (role) => {
    await createRoles(role);
    fetchRoles();
  };

  const updateRole = async (id, updatedRole) => {
    await updateRoles(id, updatedRole);
    fetchRoles();
  };

  const deleteRole = async (id) => {
    const response = await deleteRoles(id);
    setRoles((prevList) => prevList.filter((role) => role.id !== id));
  };

  const handleEdit = useCallback((row) => {
    setEditingRole(row.id);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteRole(row.id);
    } catch (error) {
      toast.error(`Error deleting role: ${error.message}`);
    }
  }, []);

  const handleAddRole = useCallback(() => {
    setEditingRole(null);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingRole(null);
  }, []);

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Role Name', accessor: 'name' }
          ]}
          data={roles}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddRole}
        />
        {showForm && (
          <AddRoleForm
            role={editingRole}
            onClose={handleCloseForm}
            addRole={addRole}
            updateRole={updateRole}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminRoles;
