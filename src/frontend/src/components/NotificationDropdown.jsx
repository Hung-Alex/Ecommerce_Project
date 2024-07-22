import React from "react";

const NotificationDropdown = () => {
  return (
    <div className="absolute top-full right-0 mt-2 w-48 bg-white text-black border border-gray-300 rounded-lg shadow-lg">
      <div className="p-2 border-b border-gray-200">Notification 1</div>
      <div className="p-2 border-b border-gray-200">Notification 2</div>
      <div className="p-2">Notification 3</div>
    </div>
  );
};

export default NotificationDropdown;
