// utils/formatCurrency.js
export const formatCurrency = (amount, locale = 'vi-VN', currency = 'VND') => {
    // Kiểm tra xem amount có phải là số hợp lệ không
    if (isNaN(amount) || amount === null) {
      return '0';
    }
  
    // Chuyển amount thành số và định dạng theo locale và currency
    return new Intl.NumberFormat(locale, {
      style: 'currency',
      currency: currency,
      minimumFractionDigits: 0, // Số chữ số thập phân
    }).format(amount);
  };
  