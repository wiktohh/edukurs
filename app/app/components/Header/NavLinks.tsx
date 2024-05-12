"use client";
import { useEffect } from "react";
import { links } from "./constants";
import Link from "next/link";

const NavLinks = () => {
  useEffect(() => {
    console.log(links);
  }, []);
  return (
    <nav>
      <ul className="flex gap-4 mr-4">
        {links.map((link, index) => {
          return (
            <li
              className="uppercase text-gray-600 hover:text-black"
              key={index}
            >
              <Link href={link.path}>{link.title}</Link>
            </li>
          );
        })}
      </ul>
    </nav>
  );
};

export default NavLinks;
