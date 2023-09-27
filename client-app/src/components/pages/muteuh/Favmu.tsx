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
        <h1 className=" text-4xl mb-2">
          ยอดนิยมสำหรับ <span className=" font-medium">สายมู</span>
        </h1>
        <TripList trips={trips} />
      </div>
    </Layout>
  );
}
