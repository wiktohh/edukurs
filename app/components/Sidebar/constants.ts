import path from "path";
import { FaHome, FaListAlt } from "react-icons/fa";

export const userlinks = [
  {
    title: "Strona główna",
    path: "/home",
    icon: FaHome,
  },
  {
    title: "Wszystkie kursy",
    path: "/all-courses",
    icon: FaListAlt,
  },
];

export const adminLinks = [
  {
    title: "Lista kont",
    path: "/admin-panel",
    icon: FaListAlt,
  },
];
