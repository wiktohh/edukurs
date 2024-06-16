"use client";
import { useAuth } from "@/context/auth-context";
import { Role } from "@/model/enum";
import { userlinks, adminLinks } from "./constants";
import Link from "next/link";

const NavLinks = () => {
  const { user } = useAuth();

  return (
    <nav>
      <ul className="flex flex-col gap-4">
        {userlinks.map((link, index) => (
          <li
            className="uppercase text-gray-600 hover:text-black flex gap-3 items-center"
            key={index}
          >
            <link.icon className="text-2xl" />
            <Link href={link.path}>{link.title}</Link>
          </li>
        ))}
        {user?.role === Role.Admin &&
          adminLinks.map((link, index) => (
            <li
              className="uppercase text-gray-600 hover:text-black flex gap-3 items-center"
              key={index}
            >
              <link.icon className="text-2xl" />
              <Link href={link.path}>{link.title}</Link>
            </li>
          ))}
      </ul>
    </nav>
  );
};

export default NavLinks;
