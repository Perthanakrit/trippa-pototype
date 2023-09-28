import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Layout from "../../layout/Layout";
import trips from "../../../data/trips";
//import tamples from "../../../data/tamples";

export default function TripInfo() {
  const [currentTrip, setCurrentTrip] = useState<any>({});
  //const [currentTample, setCurrentTample] = useState<any>({});
  const { tripId } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    setCurrentTrip(trips.find((trip: any) => trip.id.toString() === tripId));
  }, []);

  // useEffect(() => {
  //   setCurrentTample(
  //     tamples.find((tample: any) => tample.id.toString() === tripId)
  //   );
  // }, []);

  return (
    <Layout>
      <div>
        <h1 className=" text-3xl font-bold mb-4 text-zinc-50">
          {currentTrip.name}
        </h1>
        {/* <p>{currentTample}</p> */}
        {/* <Carousel data={currentTample.gralley} /> */}
        <div className="">
          <img
            className=" w-full h-[400px] object-cover"
            src={currentTrip.image}
            alt="trip image"
          />
        </div>
        <div className="mt-6 border-[1px] border-slate-400/60 p-4">
          <h1 className="text-xl font-bold">Details</h1>
          {/* detail content */}
          <div className=" flex justify-between items-center">
            <div className="">
              <h4 className="flex">
                <p className=" italic">From</p>
                <p className=" ml-2">{currentTrip.origin}</p>
              </h4>
              <h4 className="flex">
                <p className=" italic">To</p>
                <p className=" ml-2">{currentTrip.destination}</p>
              </h4>
            </div>
            <div className="mx-auto">
              <h1>Duration</h1>
              <p>{currentTrip.duration}</p>
            </div>
            <button
              onClick={() => navigate(-1)}
              className=" bg-orange-400 rounded-full py-2 px-4 text-center"
            >
              จองทัวร์นี้
            </button>
          </div>
        </div>
      </div>
    </Layout>
  );
}
