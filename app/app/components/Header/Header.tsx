import NavLinks from "./NavLinks";

const Header = () => {
  return (
    <header className="w-screen h-16 bg-gray-300 shadow-md flex justify-between items-center">
      <h1 className="ml-4 text-xl uppercase tracking-wider">Edukurs</h1>
      <div className="flex mr-4">
        <NavLinks />
        <button className="uppercase text-gray-600 pl-12 hover:text-black">
          Wyloguj się
        </button>
      </div>
    </header>
  );
};

export default Header;
