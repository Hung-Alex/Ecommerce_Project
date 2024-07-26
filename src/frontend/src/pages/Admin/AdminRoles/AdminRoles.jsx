import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddRoleForm from "./AddRoleForm";
import axios from "../../../utils/axios";
import toast from "react-hot-toast"; // Import toast from react-hot-toast

const AdminRoles = () => {
  const [roles, setRoles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [editingRole, setEditingRole] = useState(null);

  const fetchRoles = useCallback(async () => {
    try {
      const response = await axios.get("/roles");
      if (response.data.isSuccess) { // Assuming the API response has an isSuccess field
        setRoles(response.data.data);
        setLoading(false);
      } else {
        throw new Error("Failed to fetch roles");
      }
    } catch (error) {
      setError(error);
      setLoading(false);
      toast.error(`Error loading roles: ${error.message}`);
    }
  }, []);

  useEffect(() => {
    fetchRoles();
  }, [fetchRoles]);

  const addRole = async (role) => {
    try {
      const response = await axios.post("/roles", role);
      if (response.data.isSuccess) {
        fetchRoles();
        handleCloseForm();
        toast.success("Role added successfully");
      } else {
        throw new Error("Failed to add role");
      }
    } catch (error) {
      console.error("Error adding role:", error);
      toast.error(`Error adding role: ${error.message}`);
    }
  };

  const updateRole = async (id, updatedRole) => {
    try {
      const response = await axios.put(`/roles/${id}`, updatedRole);
      if (response.data.isSuccess) {
        fetchRoles();
        toast.success("Role updated successfully");
      } else {
        throw new Error("Failed to update role");
      }
    } catch (error) {
      console.error("Error updating role:", error);
      toast.error(`Error updating role: ${error.message}`);
    }
  };

  const deleteRole = async (id) => {
    try {
      const response = await axios.delete(`/roles/${id}`);
      if (response.data.isSuccess) {
        setRoles((prevList) => prevList.filter((role) => role.id !== id));
        toast.success("Role deleted successfully");
      } else {
        throw new Error("Failed to delete role");
      }
    } catch (error) {
      console.error("Error deleting role:", error);
      toast.error(`Error deleting role: ${error.message}`);
    }
  };

  const handleEdit = useCallback((row) => {
    setEditingRole(row.id);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteRole(row.id);
    } catch (error) {
      console.error("Error deleting role:", error);
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

  if (loading) return <p>Loading...</p>;
  if (error){console.log(error.message);}

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
