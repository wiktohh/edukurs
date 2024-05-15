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
      <ul className="flex flex-col gap-4">
        {links.map((link, index) => {
          return (
            <li
              className="uppercase text-gray-600 hover:text-black flex gap-3 items-center"
              key={index}
            >
              <link.icon className="text-2xl" />
              <Link href={link.path}>{link.title}</Link>
            </li>
          );
        })}
      </ul>
    </nav>
  );
};

export default NavLinks;
