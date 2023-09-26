import React from "react";
import { FaAmazon, FaHome, FaInvision } from "react-icons/fa";
import Home from "../pages/home/Home";
import { Link } from "react-router-dom";
import FavEat from "../pages/faveat/FavEat";
import FavNature from "../pages/favnature/FavNature";

const menus = [
  {
    name: "Home",
    path: "/",
    element: <Home />,
    icon: <FaHome />,
  },
  {
    name: "FavEat",
    path: "/faveat",
    element: <FavEat />,
    icon: <FaAmazon />,
  },
  {
    path: "/favnature",
    element: <FavNature />,
    icon: <FaInvision />,
  },
];

export default function Navbar() {
  return (
    <nav className=" flex flex-col items-center gap-y-4 fixed h-max bottom-0 mt-auto  z-50 top-0 w-full">
      <div
        className="flex w-full  items-center justify-between  gap-y-10  px-4 md:px-40 h-[80px]  py-8 
        backdrop-blur-sm text-3xl bg-black/5"
      >
        {menus.map((menu) => (
          <Link
            key={menu.path}
            to={menu.path}
            className="text-white font-bold text-4xl hover:text-slate-200 transition-all duration-300"
          >
            {/* icon */}
            {menu.icon}
          </Link>
        ))}
      </div>
    </nav>
  );
}
