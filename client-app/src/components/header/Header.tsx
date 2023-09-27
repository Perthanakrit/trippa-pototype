import React from "react";
import Navbar from "./Navbar";
import { RiLoginBoxFill } from "react-icons/ri";
import { Link } from "react-router-dom";
import banner from "../../assets/images/banner2.png";

export default function Header() {
  return (
    <header className="relative mb-4">
      {/* Baner */}
      <div className=" flex items-center justify-between mx-0 bg-slate-600 py-3 px-4">
        <img src={banner} className="w-[20%] h-[3.125rem] object-cover" />
        <Link to="/" className=" block text-center mt-0 mr-1 text-slate-50">
          <RiLoginBoxFill size={40} color="" />
          Login
        </Link>
      </div>
    </header>
  );
}
