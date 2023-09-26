import React from "react";
import Header from "../../header/Header";
import TripRecommedation from "../../recommend_trip/TripRecommedation";
import Layout from "../../layout/Layout";

export default function Home() {
  return (
    <Layout>
      <div className=" my-6">
        <h1>Today</h1>
      </div>
      <div className=" border-2 divide-solid border-red-500 h-[200px]">
        {/* Features */}
      </div>
      {/* Recommad trip */}
      <TripRecommedation />
    </Layout>
  );
}
