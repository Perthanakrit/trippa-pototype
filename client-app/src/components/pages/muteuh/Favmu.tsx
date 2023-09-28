import React, { useEffect, useState } from "react";
import Layout from "../../layout/Layout";
import TripList from "../../feature/tripsdashboard/TripList";
import { muteluhTrips } from "../../../data";

export default function Favmu() {
  const [trips, SetTrips] = useState<Array<any>>([]);
  useEffect(() => {
    SetTrips(muteluhTrips);
  }, []);

  return (
    <Layout>
      <div className="mx-4 xl:mx-1">
        <h1 className=" text-3xl mb-2 text-slate-50">
          ยอดนิยมสำหรับ{" "}
          <span className=" font-medium text-yellow-300 text-4xl">สายมู</span>
        </h1>
        <TripList trips={trips} />
      </div>
    </Layout>
  );
}
