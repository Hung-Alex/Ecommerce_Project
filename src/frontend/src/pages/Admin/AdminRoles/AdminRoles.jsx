import React, { useState, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddRoleForm from "./AddRoleForm";
import { deleteRole } from '../../../api/index'; // Import the API function to delete a role

const AdminRoles = () => {
  const [showForm, setShowForm] = useState(false);
  const [showEditForm, setShowEditForm] = useState(false);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh
  const [editingRole, setEditingRole] = useState(null);

  const handleDelete = useCallback(async (row) => {
      await deleteRole(row.id);
      setRefresh((prev) => !prev); // Trigger refresh
  }, []);

  const handleAddRole = useCallback(() => {
    setEditingRole(null);
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setEditingRole(row.id);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingRole(null);
    setRefresh((prev) => !prev); // Trigger refresh
  }, []);

  return (
    <DashboardLayout>
      <div className=''>
        <Table
          apiUrl="/roles" // Assuming you have an API endpoint for roles
          columns={[
            { header: 'id', accessor: 'id' },
            { header: 'Role Name', accessor: 'name' }
          ]}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddRole}
          searchParam="Name"
          refresh={refresh} // Pass refresh state to Table component
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
