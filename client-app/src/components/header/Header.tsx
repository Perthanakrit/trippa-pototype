import { RiLoginBoxFill } from "react-icons/ri";
import { Link } from "react-router-dom";
import banner from "../../assets/images/banner2.png";

export default function Header() {
  return (
    <header className="relative">
      {/* Baner */}
      <div className=" flex items-center justify-between mx-0 bg-sky-600 py-3 px-4">
        <Link to="/">
          <img src={banner} className="h-[3rem] border-none " />
        </Link>
        <Link to="/" className=" block text-center mt-0 mr-1 text-slate-50">
          <RiLoginBoxFill size={40} color="" />
          Login
        </Link>
      </div>
    </header>
  );
}
