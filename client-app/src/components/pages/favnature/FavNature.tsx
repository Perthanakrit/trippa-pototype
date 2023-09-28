import { useEffect, useState } from "react";
import Layout from "../../layout/Layout";
import { travelTrips } from "../../../data";
import TripList from "../../feature/tripsdashboard/TripList";

export default function FavNature() {
  const [trips, SetTrips] = useState<Array<any>>([]);
  useEffect(() => {
    SetTrips(travelTrips);
  }, []);

  return (
    <Layout>
      <div className="mx-4 xl:mx-1">
        <h1 className=" text-3xl mb-2 text-slate-50">
          ยอดนิยมสำหรับ{" "}
          <span className=" font-medium text-yellow-300 text-4xl">
            สายเที่ยว
          </span>
        </h1>
        <TripList trips={trips} />
      </div>
    </Layout>
  );
}
