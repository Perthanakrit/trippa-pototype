// import { twMerge } from "tailwind-merge";
// import { ClassValue, clsx } from "clsx";
import { trips } from "../../data/trips";
import { FaAngleLeft, FaAngleRight } from "react-icons/fa";

// this function is used to combine (conditional) classNames and uses clsx and tailwind-merge
// function cn(...inputs: ClassValue[]) {
//   return twMerge(clsx(inputs));
// }
export default function TripRecommedation() {
  return (
    <div className="flex items-center justify-center">
      <div className="wrapper flex max-w-7xl relative items-center">
        <i id="left" className="mr-2">
          <FaAngleLeft size={30} />
        </i>
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

        <i id="right" className="ml-2">
          <FaAngleRight size={30} />
        </i>
      </div>
    </div>
  );
}
