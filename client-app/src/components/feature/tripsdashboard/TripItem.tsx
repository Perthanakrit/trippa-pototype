import React from "react";

interface Props {
  trip: any;
}

export default function TripItem({ trip }: Props) {
  return (
    <div>
      <h1>{trip.name}</h1>
    </div>
  );
}
