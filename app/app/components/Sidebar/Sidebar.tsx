import NavLinks from "./NavLinks";
import { FaSignOutAlt } from "react-icons/fa";

const Sidebar = () => {
  return (
    <header className="h-screen min-w-64 bg-gray-200 shadow-md flex flex-col items-center gap-32">
      <div className="w-4/5 bg-white rounded-full px-4 py-2 shadow-md flex items-center justify-between mt-24">
        <div className="bg-gray-200 w-11 h-11 flex justify-center items-center rounded-full">
          WR
        </div>
        <div className="text-center">
          <p>Wiktor Rzeźnicki</p>
          <p className="text-gray-500">Student</p>
        </div>
      </div>
      <div className="w-3/4 h-full py-4 flex flex-col justify-between">
        <NavLinks />
        <button className="uppercase text-gray-600 hover:text-black text-left mt-4 flex gap-2 items-center">
          <FaSignOutAlt className="text-2xl" /> Wyloguj się
        </button>
      </div>
    </header>
  );
};

export default Sidebar;
