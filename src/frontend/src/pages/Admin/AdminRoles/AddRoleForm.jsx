import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import {
  fetchPermissionsData,
  fetchRoleDataById,
  createRole,
  updateRole,
} from '../../../api';

const AddRoleForm = ({ role, onClose }) => {
  const { register, handleSubmit, setValue } = useForm();
  const [permissions, setPermissions] = useState([]);
  const [loading, setLoading] = useState(false);

  // Fetch permissions data and role details if editing
  useEffect(() => {
    // Fetch all available permissions
    fetchPermissionsData()
      .then(response => {
        setPermissions(response);
      })
      .catch(error => {
        console.error("Error fetching permissions:", error);
      });

    // If editing a role, fetch its details
    if (role) {
      setLoading(true);
      fetchRoleDataById(role)
        .then(response => {
          const { name, permissions } = response;
          // Set form values with fetched role data
          setValue("roleName", name);
          setValue("permissions", permissions.map(p => p.id));
          setLoading(false);
        })
        .catch(error => {
          console.error("Error fetching role details:", error);
          setLoading(false);
        });
    }
  }, [role, setValue]);

  // Handle form submission
  const onSubmit = (data) => {
    const selectedPermissions = permissions
      .filter(permission => data.permissions.includes(permission.id))
      .map(permission => permission.id);

    const roleData = {
      roleId: role,
      roleName: data.roleName,
      permissions: selectedPermissions,
    };


    if (role) {
      updateRole(role, roleData).then(res => {
        if (res?.isSuccess) {
            onClose();
        }
    })
    } else {
      createRole(roleData)
      .then(res => {
          if (res?.isSuccess) {
              onClose();
          }
      })
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="fixed inset-0 bg-gray-900 bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-md">
        <h2 className="text-xl font-bold mb-4">{role ? "Edit Role" : "Add Role"}</h2>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700">Role Name</label>
            <input
              type="text"
              {...register("roleName", { required: true })}
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
            />
          </div>

          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700">Permissions</label>
            <div className="grid grid-cols-2 gap-2 mt-2">
              {permissions.map(permission => (
                <div key={permission.id} className="flex items-center">
                  <input
                    type="checkbox"
                    value={permission.id}
                    {...register("permissions")}
                    className="form-checkbox h-4 w-4 text-blue-600 transition duration-150 ease-in-out"
                  />
                  <label className="ml-2 text-sm text-gray-700">{permission.name}</label>
                </div>
              ))}
            </div>
          </div>

          <div className="flex justify-end">
            <button
              type="button"
              onClick={onClose}
              className="bg-gray-200 hover:bg-gray-300 text-gray-800 py-2 px-4 rounded mr-2"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="bg-blue-600 hover:bg-blue-700 text-white py-2 px-4 rounded"
            >
              {role ? "Update Role" : "Add Role"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AddRoleForm;
