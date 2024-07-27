import diacritics from 'diacritics';

export function titleToSlug(title) {
    return diacritics.remove(title)              // Loại bỏ dấu của các ký tự tiếng Việt
      .toLowerCase()                           // Chuyển thành chữ thường
      .trim()                                  // Loại bỏ khoảng trắng ở đầu và cuối
      .replace(/[\s\W-]+/g, '-')               // Thay thế các ký tự không phải chữ cái hoặc số và khoảng trắng bằng dấu gạch ngang
      .replace(/^-+|-+$/g, '');                // Loại bỏ dấu gạch ngang ở đầu và cuối
}
