import React from "react";
import { RiPinDistanceFill } from "react-icons/ri";
import { Link, Outlet } from "react-router-dom";

interface Props {
  trip: any;
}

export default function TripItem({ trip }: Props) {
  return (
    <div className=" bg-slate-200 block md:grid md:grid-cols-3 my-5 relative">
      <img
        src={trip.image}
        alt={trip.name}
        className=" w-full h-[400px] md:h-[280px] object-cover md:object-fill bg-no-repeat bg-contain"
      />
      <div className=" p-2">
        <h1 className=" text-3xl">{trip.name}</h1>
        <a className="flex items-center text-gray-500">
          <RiPinDistanceFill size={20} />
          <span className="ml-[0.25rem]">{trip.destination}</span>
        </a>
        <p className="mt-1">{trip.description}</p>
      </div>
      <div className=" flex p-3 items-center">
        <Link
          to={`/favmu/${trip.id}`}
          className=" bg-slate-50 p-2 rounded-full "
        >
          ดูรายละเอียด
        </Link>
      </div>
      <Outlet />
    </div>
  );
}
