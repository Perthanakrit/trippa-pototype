import { useEffect, useState } from "react";
import Layout from "../../layout/Layout";
import TripList from "../../feature/tripsdashboard/TripList";
import { trips } from "../../../data/trips";

export default function Favmu() {
  const [gettrips, SetTrips] = useState<Array<any>>([]);
  const tripTag = 1;

  useEffect(() => {
    var tripArray: Array<any> = [];
    trips.forEach((trip: any) => {
      if (trip.tag === tripTag) {
        tripArray.push(trip);
      }
    });
    SetTrips(tripArray);
  }, []);

  return (
    <Layout>
      <div className="mx-4 xl:mx-1">
        <h1 className=" text-3xl mb-2 text-slate-50">
          ยอดนิยมสำหรับ{" "}
          <span className=" font-medium text-yellow-300 text-4xl">สายมู</span>
        </h1>
        <TripList trips={gettrips} />
      </div>
    </Layout>
  );
}
