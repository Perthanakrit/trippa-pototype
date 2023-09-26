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
      <div>
        Favmu
        <TripList trips={trips} />
      </div>
    </Layout>
  );
}
