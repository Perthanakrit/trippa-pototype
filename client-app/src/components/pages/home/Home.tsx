import Layout from "../../layout/Layout";
import Navbar from "../../header/Navbar";
import { FaSearchLocation } from "react-icons/fa";
import TripCarousel from "../../carousel/TripCarousel";
import { trips } from "../../../data/trips";

export default function Home() {
  return (
    <Layout>
      <div className=" mt-7">
        <div className=" h-[200px] block">
          <h1 className=" text-center text-5xl text-zinc-50">อยากไปไหน</h1>
          <Navbar />
          {/* Serach bar */}
          <div className=" w-[50%] bg-white mx-auto mt-4 p-3 rounded-full">
            <form action="/" className=" flex">
              <button className=" bg-stone-700 p-1 rounded-full focus:bg-slate-100 focus:text-stone-700">
                <FaSearchLocation size={15} />
              </button>
              <input
                className=" border-none outline-none w-full ml-2 text-stone-700"
                type="text"
                placeholder="ค้นหาทัวร์ได้เลย..."
              />
            </form>
          </div>
        </div>
      </div>

      {/* Recommad trip */}
      <div className=" bg-slate-300/60 p-5">
        <TripCarousel trips={trips} />
      </div>
    </Layout>
  );
}
