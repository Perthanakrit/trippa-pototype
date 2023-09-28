import { useEffect, useState } from "react";
import { timeLines } from "../../../data/trips";
import { it } from "node:test";

type Props = {
  agenda: any;
};

export default function AgendaTrip({ agenda }: Props) {
  // const [currentAgenda, setCurrentAgenda] = useState<Array<any>>([]);
  const currentAgenda: Array<any> = agenda.timeline;

  return (
    <>
      <ul className=" my-5">
        {currentAgenda != null &&
          currentAgenda.map((item: any, idx: number) => (
            <li key={idx} className="relative flex gap-6 pb-5 items-baseline">
              <div
                className={`before:absolute before:left-[4.6px] ${
                  idx != currentAgenda.length - 1 ? "before:h-full" : ""
                } before:w-[2px] before:bg-[#FD924B] `}
              >
                <svg xmlns="http://www.w3.org/2000/svg" width={12} height={12}>
                  <circle cx={6} cy={6} r={6} fill="#FD924B" />
                </svg>
              </div>
              <div className=" text-sm">
                <p className=" font-semibold text-gray-600">{item.time}</p>
                <p className="ml-1 text-gray-500">{item.detail}</p>
              </div>
            </li>
          ))}
      </ul>
    </>
  );
}
