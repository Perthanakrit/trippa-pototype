import React, { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import { muteluhTrips, trips } from "../../../data";
import Layout from "../../layout/Layout";

export default function TripInfo() {
  const [currentTrip, setCurrentTrip] = useState<any>({});
  const { tripId } = useParams();

  useEffect(() => {
    setCurrentTrip(
      muteluhTrips.find((trip: any) => trip.id.toString() === tripId)
    );
    console.log("load");
  }, []);

  return (
    <Layout>
      <div>
        <h1>{currentTrip.name}</h1>
      </div>
    </Layout>
  );
}
