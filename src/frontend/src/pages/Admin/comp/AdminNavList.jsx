import { NavLink } from "react-router-dom";

const AdminNavList = () => {
  return (
    <nav>
      <ul className="items-center gap-8 font-semibold">
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/products"
          >
            Products
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/category"
          >
            Category
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/brands"
          >
            Brand
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/banner"
          >
            Banner
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/sliders"
          >
            Sliders
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/roles"
          >
            Roles
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/users"
          >
            Users
          </NavLink>
        </li>
        <li className="p-2 flex justify-center text-xl border-b-2">
          <NavLink 
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} 
            to="/admin/news"
          >
            News
          </NavLink>
        </li>
      </ul>
    </nav>
  );
};

export default AdminNavList;
