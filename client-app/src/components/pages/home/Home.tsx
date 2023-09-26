import React from "react";
import Header from "../../header/Header";
import TripRecommedation from "../../recommend_trip/TripRecommedation";
import Layout from "../../layout/Layout";
import Navbar from "../../header/Navbar";
import { FaSearchLocation } from "react-icons/fa";

export default function Home() {
  return (
    <Layout>
      <div className=" mt-7">
        <div className=" h-[200px] block">
          <h1 className=" text-center text-5xl">อยากไปไหน</h1>
          <Navbar />
          {/* Serach bar */}
          <div className=" w-[50%] bg-white mx-auto mt-4 p-3 rounded-full">
            <form action="/" className=" flex">
              <button className=" bg-slate-200 p-2 rounded-full">
                <FaSearchLocation size={20} />
              </button>
              <input
                className=" border-none outline-none w-full ml-2"
                type="text"
                placeholder="ค้นหาทัวร์ได้เลย..."
              />
            </form>
          </div>
        </div>
      </div>

      {/* Recommad trip */}
      <TripRecommedation />
    </Layout>
  );
}
