import { NavLink } from "react-router-dom";
import DropDown from "./DropDown";

const NavList = () => {
  return (
    <nav>
      <ul className="flex items-center gap-8 font-semibold">
        <li>
          <NavLink onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} to="/">Home</NavLink>
        </li>
        <DropDown />
        <li>
          <NavLink onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} to="/about">About</NavLink>
        </li>
        <li>
          <NavLink onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} to="/news">News</NavLink>
        </li>
      </ul>
    </nav>
  );
};

export default NavList;
