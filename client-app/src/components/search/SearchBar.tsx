import React, { useState } from "react";
import { FaSearchLocation } from "react-icons/fa";
import { trips } from "../../data/trips";

export default function SearchBar() {
  const [query, setQuery] = useState<string>("");

  return (
    <div>
      <div className=" w-[50%] bg-white mx-auto mt-4 p-3 rounded-full">
        <form action="/" className=" flex">
          <button
            onClick={(e: React.FormEvent) => {
              e.preventDefault();
            }}
            className=" bg-stone-700 p-1 rounded-full focus:bg-slate-100 focus:text-stone-700"
          >
            <FaSearchLocation size={15} />
          </button>
          <input
            className=" border-none outline-none w-full ml-2 text-stone-700"
            type="text"
            placeholder="ค้นหาทัวร์ได้เลย..."
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              setQuery(e.target.value)
            }
          />
        </form>
      </div>

      {trips
        .filter((trip: any) => {
          if (query === "") {
            return trips;
          } else if (trip.name.toLowerCase().includes(query.toLowerCase())) {
            return trips;
          }
        })
        .map((trip: any) => {
          return (
            query !== "" && (
              <div
                key={trip.id}
                className=" relative text-center bg-slate-200 w-full md:w-[50%]  rounded-full mt-2 mx-auto p-2 mb-5"
              >
                <h1>{trip.name}</h1>
              </div>
            )
          );
        })}
    </div>
  );
}
