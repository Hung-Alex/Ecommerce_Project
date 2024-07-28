// utils/formatDate.js

/**
 * Formats a date string into a more readable format.
 * @param {string} dateString - The date string to format (e.g., '2024-07-28T12:00:00Z').
 * @returns {string} - The formatted date (e.g., '28 Jul').
 */
export const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = date.getDate();
    const month = date.toLocaleString('default', { month: 'short' });
    return `${day} ${month}`;
  };
