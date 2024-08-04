import { AiOutlineInstagram, AiOutlineTwitter } from "react-icons/ai";
import { FaFacebook, FaPinterest } from "react-icons/fa";
import logo from "../../../assets/Logo.png";

const Footer = () => {
  return (
    <footer className="bg-[#647075] py-6 mt-auto">
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-3">
          <div className="flex flex-col items-center md:items-start gap-4">
            <div className="flex items-center gap-2">
              <img className="h-8" src={logo} alt="logo" />
              <h1 className="text-2xl font-bold text-white">Organick</h1>
            </div>
            <p className="text-gray-50 text-center md:text-left">
              Simply dummy text of the printing and typesetting industry. Lorem
              Ipsum simply dummy text of the printing
            </p>
            <div className="flex justify-center items-center text-2xl gap-4 mt-4">
              <span className="p-2 rounded-full bg-[#EFF6F1]">
                <AiOutlineInstagram />
              </span>
              <span className="p-2 rounded-full bg-[#EFF6F1]">
                <FaFacebook />
              </span>
              <span className="p-2 rounded-full bg-[#EFF6F1]">
                <AiOutlineTwitter />
              </span>
              <span className="p-2 rounded-full bg-[#EFF6F1]">
                <FaPinterest />
              </span>
            </div>
          </div>

          <div className="flex flex-col items-center md:items-start gap-4">
            <h3 className="text-xl font-bold text-white">Important Links</h3>
            <ul className="flex flex-col gap-1 text-gray-50">
              <li>
                <a href="#" className="hover:text-gray-300">Home</a>
              </li>
              <li>
                <a href="#" className="hover:text-gray-300">Shop</a>
              </li>
              <li>
                <a href="#" className="hover:text-gray-300">About</a>
              </li>
              <li>
                <a href="#" className="hover:text-gray-300">News</a>
              </li>
            </ul>
          </div>

          <div className="flex flex-col items-center md:items-start gap-4">
            <h3 className="text-xl font-bold text-white">Contact Us</h3>
            <ul className="flex flex-col gap-1 text-gray-50">
              <li>
                <p className="text-sm font-semibold">Email</p>
                <span className="text-sm">organic@gmail.com</span>
              </li>
              <li>
                <p className="text-sm font-semibold">Phone</p>
                <span className="text-sm">+880993749</span>
              </li>
              <li>
                <p className="text-sm font-semibold">Address</p>
                <span className="text-sm">88 road, borklyn street, USA</span>
              </li>
            </ul>
          </div>
        </div>
        <div className="text-center text-sm text-gray-50 mt-8">
          <p>Copyright &copy; Organick - 2023</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
