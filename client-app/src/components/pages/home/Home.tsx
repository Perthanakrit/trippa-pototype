import React from "react";
import Header from "../../header/Header";
import TripRecommedation from "../../recommend_trip/TripRecommedation";

export default function Home() {
  return (
    <>
      <Header />
      <div className=" my-6">
        <h1>Today</h1>
      </div>
      <div className=" border-2 divide-solid border-red-500 h-[200px] mx-auto max-w-[800px]">
        {/* Features */}
      </div>
      {/* Recommad trip */}
      <TripRecommedation />
    </>
  );
}
