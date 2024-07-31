import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddRoleForm from "./AddRoleForm";
import toast from "react-hot-toast"; // Import toast from react-hot-toast
import {
  fetchRolesData,
 deleteRole
} from '../../../api/index';

const AdminRoles = () => {
  const [roles, setRoles] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingRole, setEditingRole] = useState(null);

  const fetchRoles = useCallback(async () => {
      const res = await fetchRolesData();
      setRoles(res.data);
  }, [showForm]);

  useEffect(() => {
    fetchRoles();
  }, [fetchRoles]);

  const deleteRoles = async (id) => {
    const response = await deleteRole(id);
    setRoles((prevList) => prevList.filter((role) => role.id !== id));
  };

  const handleEdit = useCallback((row) => {
    setEditingRole(row.id);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteRoles(row.id);
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
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminRoles;
