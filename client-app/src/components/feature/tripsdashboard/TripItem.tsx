import { RiPinDistanceFill } from "react-icons/ri";
import { Link, Outlet } from "react-router-dom";

interface Props {
  trip: any;
}

export default function TripItem({ trip }: Props) {
  //const location = useLocation();

  return (
    <div className=" bg-slate-200 block md:grid md:grid-cols-3 my-5 relative rounded-md">
      <img
        src={trip.image}
        alt={trip.name}
        className=" w-full h-[400px] md:h-[280px] md:object-center object-cover bg-no-repeat bg-contain"
      />
      <div className=" p-2 mt-2 ml-5">
        <h1 className=" text-2xl mb-1 ">{trip.name}</h1>
        <a className="flex items-center text-gray-500 mb-2">
          <RiPinDistanceFill size={20} />
          <span className="ml-[0.25rem] text-sm">{trip.destination}</span>
        </a>
        <p className="mt-1 text-zinc-600">{trip.description}</p>
      </div>
      <div className=" flex p-3 items-center ">
        <div className=" bg-slate-400 h-[230px] w-[0.1px] mx-6 hidden md:block"></div>
        <Link
          to={`/trips/${trip.id}`}
          className=" bg-slate-50 p-2 rounded-full mx-auto w-[15rem] text-center text-orange-400 hover:text-zinc-50 hover:bg-orange-400"
        >
          ดูรายละเอียด
        </Link>
      </div>
      <Outlet />
    </div>
  );
}
