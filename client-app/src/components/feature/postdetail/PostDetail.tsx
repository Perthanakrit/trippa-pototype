import { useParams } from "react-router-dom";
import Layout from "../../layout/Layout";
import { useEffect, useState } from "react";
import profileImg from "../../../assets/images/profiesImg/profiesDefault.png";
import { BsTelephoneFill } from "react-icons/bs";
import ComingSoon from "../../pages/ComingSoon";
import { useSelector } from "react-redux";
import {
  customTripSelector,
  loadCustomTrip,
} from "../../../store/slices/customTripSlice";
import { useAppDispatch } from "../../../store/Store";
import { BeatLoader } from "react-spinners";

export default function PostDetail() {
  const [join, setJoin] = useState(false);
  const { postId, name } = useParams();
  const [post, setPost] = useState<any>({});

  const dispatch = useAppDispatch();
  const { customTrip, loading } = useSelector(customTripSelector);

  useEffect(() => {
    dispatch(loadCustomTrip(postId!));
  }, []);

  return (
    <Layout>
      {!join ? (
        loading ? (
          <BeatLoader />
        ) : (
          <div className=" container md:grid md:grid-cols-2 mt-4 md:items-center ">
            <img
              src={""}
              alt="activity"
              className=" w-full h-[300px] object-fill rounded"
            />
            {/* <p className=" font-semibold ml-2 ">{post.name}</p> */}

            <div className="ml-5 mt-4 md:mt-0 md:py-3 ">
              <div>
                <p>
                  <span>ไปไหน?</span>
                  {customTrip?.trip.destination}
                </p>
                <p>
                  {" "}
                  <span>กี่คน?</span>
                  {10}
                </p>
                {/* <p>
                <span>อายุ:</span>
                {post.age}
              </p> */}
                <p>
                  <span>กี่วัน กี่คืน?</span>
                  {customTrip?.trip.duration}
                </p>
              </div>
              <div className=" flex items-center my-3">
                <span className=" font-bold text-lg">นัดหมาย</span>
                <div className=" h-[60px] bg-black w-[1px] mx-3 "></div>
                <div>
                  <p>วันที่ {1}</p>
                  <p>เวลา {10.0}</p>
                  <p>ณ {customTrip?.trip.destination}</p>
                </div>
              </div>
              <div className=" flex justify-between items-center mt-5 w-full pr-5 pl-2 py-2 bg-zinc-50/60 rounded-lg  -translate-x-3 md:translate-x-0">
                <div className=" w-[70%] flex items-center">
                  <img
                    src={profileImg}
                    className=" w-[30%] h-[30%] object-fill mr-1"
                  />
                  <div>
                    <h1>{customTrip?.trip.name}</h1>
                    <p className=" text-zinc-500 flex items-center">
                      <BsTelephoneFill size={16} className=" ml-1 mr-2" />
                      {
                        customTrip?.trip.attendee[0].contacts.find(
                          (c) => c.channel.toLowerCase() === "phone"
                        )?.value
                      }
                    </p>
                  </div>
                </div>

                <button
                  onClick={() => setJoin(!join)}
                  className=" bg-[#FD924B] p-2 rounded-full w-[100px]"
                >
                  เข้าร่วม
                </button>
              </div>
            </div>
          </div>
        )
      ) : (
        <ComingSoon />
      )}
    </Layout>
  );
}
