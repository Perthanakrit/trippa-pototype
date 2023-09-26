import React from "react";
import TripItem from "./TripItem";

interface Props {
  trips: any;
}

export default function TripList({ trips }: Props) {
  return (
    <div>
      {trips.map((trip: any) => (
        <TripItem key={trip.id} trip={trip} />
      ))}
    </div>
  );
}
