import React, { useState, useEffect, useCallback } from 'react';
import axios from '../../../utils/axios';
import PropTypes from 'prop-types';
import debounce from 'lodash.debounce';
import { AiOutlinePlus } from 'react-icons/ai';

const PaginationTable = ({ apiUrl, columns, onEdit, onDelete, onAdd, searchParam, refresh }) => {
  const [data, setData] = useState([]);
  const [bool, setbool] = useState(false);
  const [pageIndex, setPageIndex] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [totalItems, setTotalItems] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const [pageSize, setPageSize] = useState(8);
  const [sortColumn, setSortColumn] = useState('createdAt');
  const [sortBy, setSortBy] = useState('ASC');
  const [loading, setLoading] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [error, setError] = useState(null);

  const hiddenColumns = new Set(['id']);
  const imageAccessorsSet = new Set(['image', 'avatarImage', 'images', 'logoImageUrl', 'imageUrl']);
  const booleanAccessorsSet = new Set(["isVisible", "published", "isActive"]);

  const fetchData = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await axios.get(apiUrl, {
        params: {
          [searchParam]: searchTerm,
          PageSize: pageSize,
          PageNumber: pageIndex + 1,
          SortColumn: sortColumn,
          SortBy: sortBy,
        },
      });

      const { data } = response.data;
      setData(data || []);
      setTotalPages(response.data.totalPages);
      setTotalItems(response.data.totalItems);
    } catch (error) {
      setError('Error fetching data');
      console.error('Error fetching data:', error);
    } finally {
      setLoading(false);
    }
  };

  const debouncedSearch = useCallback(
    debounce((term) => {
      setSearchTerm(term);
      setPageIndex(0);
    }, 500),
    []
  );

  useEffect(() => {
    fetchData();
  }, [searchTerm, pageIndex, pageSize, sortColumn, sortBy, refresh]);

  const getPageNumbers = () => {
    if (totalPages <= 7) {
      return Array.from({ length: totalPages }, (_, i) => i);
    }

    const left = Math.max(0, pageIndex - 3);
    const right = Math.min(totalPages - 1, pageIndex + 3);
    const pages = [];

    for (let i = left; i <= right; i++) {
      pages.push(i);
    }

    return pages;
  };

  const handlePageChange = (index) => {
    if (index === '...') return;
    setPageIndex(index);
    setDropdownOpen(false);
  };

  const handleDelete = useCallback(async (row) => {
    await onDelete(row);
  }, [onDelete]);

  const handleAdd = useCallback(async () => {
    await onAdd();
  }, [onAdd]);

  const handleEdit = useCallback(async (row) => {
    await onEdit(row);
  }, [onEdit]);


  const renderTableHeaders = () => (
    <tr className="border-b border-gray-200">
      {columns.map(({ header, accessor }, index) => {
        if (hiddenColumns.has(accessor)) return null;

        let headerClass = '';
        if (index === 0) headerClass = 'col-first';
        else if (index === 1) headerClass = 'col-second';
        else if (index === 2) headerClass = 'col-second';
        else if (index === columns.length - 1) headerClass = 'col-last';
        else headerClass = 'col-hidden';

        return (
          <th key={accessor} className={`p-2 text-left ${headerClass}`}>
            {header}
          </th>
        );
      })}

      {(onEdit || onDelete) && <th className="p-4 text-left">Actions</th>}
    </tr>
  );

  const renderTableRows = () => (
    data.map((row) => (
      <tr key={row.id} className="border-b border-gray-200">
        {columns.map((col, index) => {
          if (hiddenColumns.has(col.accessor)) return null;

          let colClass = '';
          if (index === 0) colClass = 'col-first';
          else if (index === 1) colClass = 'col-second';
          else if (index === 2) colClass = 'col-second';
          else if (index === columns.length - 1) colClass = 'col-last';
          else colClass = 'col-hidden';

          let content = row[col.accessor];

          if (imageAccessorsSet.has(col.accessor)) {
            if (Array.isArray(content) && content.length > 0) {
              content = content[0];
            }
            content = <img src={content} alt="" className="w-14 h-14 object-cover rounded-full" />;
          } else if (booleanAccessorsSet.has(col.accessor)) {
            content = content ? 'Yes' : 'No';
          }

          return (
            <td key={col.accessor} className={`p-2 ${colClass}`}>
              {content}
            </td>
          );
        })}

        {(onEdit || onDelete) && (
          <td className="px-2 py-2 whitespace-nowrap text-gray-500 col-last">
            {onEdit && (
              <button
                onClick={() => handleEdit(row)}
                className="text-blue-500 hover:text-blue-700 text-xs"
              >
                Edit
              </button>
            )}
            {onDelete && (
              <button
                onClick={() => handleDelete(row)}
                className="text-red-500 hover:text-red-700 ml-2 text-xs"
              >
                Delete
              </button>
            )}
          </td>
        )}
      </tr>
    ))
  );

  const pageNumbers = getPageNumbers();

  // Generate options for the page size
  const generatePageSizeOptions = () => {
    const options = [1, 5, 10, 20, 50];
    return options.filter(option => option <= totalItems);
  };

  const pageSizeOptions = generatePageSizeOptions();

  return (
    <div className="container mx-auto m-4 w-full">
      <p className="text-xs text-gray-500">({totalItems} <n />Item)</p>

      <div className="mb-4 m-2 md:flex flex-col md:flex-row md:items-center">
        <button
          onClick={handleAdd}
          className="mr-4 p-2 bg-green-500 text-white w-9 rounded-3xl shadow-md hover:bg-green-600 transition-colors duration-200"
        >
          <AiOutlinePlus size={20} />
        </button>
        <input
          type="text"
          onChange={(e) => debouncedSearch(e.target.value)}
          placeholder="Search"
          className="p-2 border border-gray-300 rounded-md mb-2 md:mb-0 md:mr-4"
        />

        <div className="ml-auto flex">
          <div>

            <label className="mr-2">Sort By:</label>
            <select
              value={sortColumn}
              onChange={(e) => setSortColumn(e.target.value)}
              className="p-2 border border-gray-300 rounded-md w-20 md:w-28"
            >
              {columns.map(({ accessor }) => (
                <option key={accessor} value={accessor}>
                  {accessor.charAt(0).toUpperCase() + accessor.slice(1)}
                </option>
              ))}
              <option value="createdAt">Created At</option>
            </select>
          </ div>
          <div>
            <label className="mx-2">Sort Order:</label>
            <select
              value={sortBy}
              onChange={(e) => setSortBy(e.target.value)}
              className="p-2 border border-gray-300 w-24 rounded-md"
            >
              <option value="ASC">Ascending</option>
              <option value="DESC">Descending</option>
            </select>
          </div>
        </div>
      </div>


      <table className="min-w-full bg-white rounded-lg shadow-md divide-y divide-gray-200">
        <thead>
          {renderTableHeaders()}
        </thead>
        <tbody>
          {renderTableRows()}
        </tbody>
      </table>

      <div className="flex justify-center mt-4 items-center">
        <button
          onClick={() => setPageIndex((prev) => Math.max(0, prev - 1))}
          className={`px-4 py-2 border border-gray-300 -z-1 rounded-md mx-1 ${pageIndex === 0 ? 'cursor-not-allowed opacity-50' : 'bg-white'}`}
          disabled={pageIndex === 0}
        >
          Previous
        </button>

        {pageNumbers.map((pageNumber, index) => (
          <button
            key={index}
            onClick={() => handlePageChange(pageNumber)}
            className={`px-4 py-2 border border-gray-300 rounded-md mx-1 ${pageNumber === pageIndex ? 'bg-blue-500 text-white' : 'bg-white'}`}
          >
            {pageNumber + 1}
          </button>
        ))}

        <button
          onClick={() => setPageIndex((prev) => Math.min(totalPages - 1, prev + 1))}
          className={`px-4 py-2 border border-gray-300 -z-1 rounded-md mx-1 ${pageIndex === totalPages - 1 ? 'cursor-not-allowed opacity-50' : 'bg-white'}`}
          disabled={pageIndex === totalPages - 1}
        >
          Next
        </button>

        <div className="ml-auto ">
          <label className="mr-2">Page Size</label>
          <select
            value={pageSize}
            onChange={(e) => setPageSize(e.target.value)}
            className="p-2 border border-gray-300 rounded-md"
          >
            {pageSizeOptions.map(size => (
              <option key={size} value={size}>{size}</option>
            ))}
          </select>
        </div>
      </div>
    </div>
  );
};

PaginationTable.propTypes = {
  apiUrl: PropTypes.string.isRequired,
  columns: PropTypes.arrayOf(
    PropTypes.shape({
      header: PropTypes.string,
      accessor: PropTypes.string,
    })
  ).isRequired,
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
  onAdd: PropTypes.func,
  searchParam: PropTypes.string.isRequired,
};

export default PaginationTable;
