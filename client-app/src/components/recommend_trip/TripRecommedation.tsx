import React, { useRef } from "react";
import { Link } from "react-router-dom";
import { twMerge } from "tailwind-merge";
import { ClassValue, clsx } from "clsx";
import img1 from "../../assets/images/img-1.jpg";
import trips from "../../data";

// this function is used to combine (conditional) classNames and uses clsx and tailwind-merge
function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}
export default function TripRecommedation() {
  return (
    <div className="flex px-0 py-9 items-center justify-center">
      <div className="wrapper flex max-w-7xl relative">
        <i id="left" className="fa-solid fa-angle-left"></i>
        <div className="carousel cursor-pointer overflow-hidden whitespace-nowrap scroll-smooth flex">
          {trips.map((trip: any) => (
            <img
              className=" h-[340px] object-cover select-none ml-4 w-[calc(100% / 3)] first:ml-0 md:w-[calc(100% / 2)] sm:w-full"
              key={trip.name}
              src={trip.image}
              draggable="false"
            />
          ))}
        </div>

        <i id="right" className="fa-solid fa-angle-right"></i>
      </div>
    </div>
  );
}
