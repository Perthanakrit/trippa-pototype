import Layout from "../../layout/Layout";
import Navbar from "../../header/Navbar";
import TripCarousel from "../../carousel/TripCarousel";
import { trips } from "../../../data/trips";
import SearchBar from "../../search/SearchBar";
export default function Home() {
  return (
    <Layout>
      <div className=" mt-7">
        <div className=" h-[110px] block">
          <h1 className=" text-center text-5xl text-zinc-50">
            <span className="text-5xl text-[#f9ad7a]">อยาก</span>
            <span className="text-5xl text-[#fdf185]">ไป</span>ไหน
          </h1>
          <Navbar />
          {/* Serach bar */}
        </div>
      </div>
      <SearchBar />

      {/* Recommad trip */}
      <div className="mt-8 mb-2">
        <h1 className=" text-2xl text-zinc-50">
          <span className=" text-2xl border-none text-[#ffcaaa]">แนะนำ</span>
          สำหรับคุณ
        </h1>
        <TripCarousel trips={trips} />
      </div>
    </Layout>
  );
}
