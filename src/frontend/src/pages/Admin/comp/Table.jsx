import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { AiOutlinePlus } from 'react-icons/ai';

const Table = ({ columns, data = [], onEdit, onDelete, onAdd }) => {
  const [search, setSearch] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const rowsPerPage = 10;

  const hiddenColumns = new Set(['id', 'image']);

  const filteredData = Array.isArray(data) ? data.filter(row =>
    columns.some(col =>
      row[col.accessor]
        .toString()
        .toLowerCase()
        .includes(search.toLowerCase())
    )
  ) : [];

  const totalPages = Math.ceil(filteredData.length / rowsPerPage);
  const paginatedData = filteredData.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const handleSearchChange = (e) => {
    setSearch(e.target.value);
    setCurrentPage(1);
  };

  const handlePageChange = (newPage) => {
    if (newPage >= 1 && newPage <= totalPages) {
      setCurrentPage(newPage);
    }
  };

  return (
    <div className="p-6">
      <div className="mb-4 flex items-center">
        <button
          onClick={onAdd}
          className="mr-4 p-2 bg-green-500 text-white rounded-3xl shadow-md hover:bg-green-600 transition-colors duration-200"
        >
          <AiOutlinePlus size={20} />
        </button>
        <input
          type="text"
          value={search}
          onChange={handleSearchChange}
          placeholder="Search..."
          className="shadow-md p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
        />
      </div>
      <div className="overflow-x-auto">
        <div className="max-w-screen-lg mx-auto">
          <table className="min-w-full divide-y divide-gray-200 bg-white shadow-md rounded-lg">
            <thead className="bg-gray-50">
              <tr>
                {columns.map((col, index) => {
                  if (hiddenColumns.has(col.accessor)) return null;
                  return (
                    <th
                      key={index}
                      className="px-2 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider min-w-[120px]"
                    >
                      {col.header}
                    </th>
                  );
                })}
                {(onEdit || onDelete) && (
                  <th className="px-2 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider min-w-[80px]">
                    Actions
                  </th>
                )}
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {paginatedData.map((row, rowIndex) => (
                <tr key={rowIndex} className="text-sm">
                  {columns.map((col, colIndex) => {
                    if (hiddenColumns.has(col.accessor)) return null;
                    return (
                      <td
                        key={colIndex}
                        className="px-2 py-2 whitespace-nowrap text-gray-900 truncate"
                      >
                        {row[col.accessor]}
                      </td>
                    );
                  })}
                  {(onEdit || onDelete) && (
                    <td className="px-2 py-2 whitespace-nowrap text-gray-500">
                      {onEdit && (
                        <button
                          onClick={() => onEdit(row)}
                          className="text-blue-500 hover:text-blue-700 mr-2 text-xs"
                        >
                          Edit
                        </button>
                      )}
                      {onDelete && (
                        <button
                          onClick={() => onDelete(row)}
                          className="text-red-500 hover:text-red-700 text-xs"
                        >
                          Delete
                        </button>
                      )}
                    </td>
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
      <div className="flex justify-end mt-4">
        <button
          onClick={() => handlePageChange(currentPage - 1)}
          className="px-4 py-2 bg-gray-300 text-gray-700 rounded-md shadow-sm hover:bg-gray-400 disabled:opacity-50"
          disabled={currentPage === 1}
        >
          {'<'}
        </button>
        <span className="mx-2 text-sm text-gray-600">
          Page {currentPage} of {totalPages}
        </span>
        <button
          onClick={() => handlePageChange(currentPage + 1)}
          className="px-4 py-2 bg-gray-300 text-gray-700 rounded-md shadow-sm hover:bg-gray-400 disabled:opacity-50"
          disabled={currentPage === totalPages}
        >
          {'>'}
        </button>
      </div>
    </div>
  );
};

Table.propTypes = {
  columns: PropTypes.arrayOf(
    PropTypes.shape({
      header: PropTypes.string.isRequired,
      accessor: PropTypes.string.isRequired
    })
  ).isRequired,
  data: PropTypes.arrayOf(PropTypes.object), // Validate data as an array
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
  onAdd: PropTypes.func.isRequired
};

export default Table;
