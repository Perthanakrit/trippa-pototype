import { RiBardFill, RiCommunityFill } from "react-icons/ri";
import { MdFoodBank } from "react-icons/md";
import { SiYourtraveldottv } from "react-icons/si";

const menus = [
  {
    name: "FavEat",
    path: "/faveat",
    title: "สายกิน",
    icon: <MdFoodBank />,
  },
  {
    name: "FavNature",
    path: "/favnature",
    title: "สายเที่ยว",
    icon: <SiYourtraveldottv />,
  },
  {
    name: "FavMuteuh",
    path: "/favmu",
    title: "สายมู",
    icon: <RiBardFill />,
  },
  {
    name: "Community",
    path: "/community",
    title: "ชุมชน",
    icon: <RiCommunityFill />,
  },
];

export default function Navbar() {
  return (
    <nav className=" bg-slate-300/50  w-[45%] mx-auto p-2 rounded-full mt-4">
      <div className=" mx-2">
        <ul className="flex justify-between items-center">
          {menus.map((menu) => (
            <li key={menu.path}>
              <a
                href={menu.path}
                className=" flex items-center md:text-lg hover:text-[#ffec3f] hover:text-xl focus:text-[#ffec3f]"
              >
                {menu.icon}{" "}
                <span className=" hidden md:block bottom-4 ml-1 text-zinc-50">
                  {menu.title}
                </span>
              </a>
            </li>
          ))}
        </ul>
      </div>
    </nav>
    // <nav className=" flex flex-col items-center gap-y-4 fixed h-max bottom-0 mt-auto  z-50 top-0 w-full">
    //   <div
    //     className="flex w-full  items-center justify-between  gap-y-10  px-4 md:px-40 h-[80px]  py-8
    //     backdrop-blur-sm text-3xl bg-black/5"
    //   >
    //     {menus.map((menu) => (
    //       <Link
    //         key={menu.path}
    //         to={menu.path}
    //         className="text-white font-bold text-4xl hover:text-slate-200 transition-all duration-300"
    //       >
    //         {/* icon */}
    //         {menu.icon}
    //       </Link>
    //     ))}
    //   </div>
    // </nav>
  );
}
