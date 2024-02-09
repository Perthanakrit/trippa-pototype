import { useEffect, useState } from "react";
import Layout from "../../layout/Layout";
import CommunityList from "../../feature/community/CommunityList";
import posts from "../../../data/community";
import { loadCustomTrips } from "../../../store/slices/customTripSlice";
import { useAppDispatch } from "../../../store/Store";

//RiCommunityFill

export default function Community() {
  const [getPosts, setPosts] = useState<any[]>([]);
  const dispatch = useAppDispatch();
  useEffect(() => {
    setPosts(posts);
    dispatch(loadCustomTrips());
  }, []);

  return (
    <Layout>
      {/* <div className="flex justify-center items-center h-[80vh]">
        <h1 className="text-4xl font-bold text-slate-50">Coming Soon</h1>
      </div> */}
      <div className="mx-4 xl:mx-1">
        <h1 className=" text-3xl mb-2 text-slate-50">
          หาเพื่อน{" "}
          <span className=" font-medium text-yellow-300 text-4xl">เที่ยว</span>
        </h1>
        <CommunityList posts={getPosts} />
      </div>
    </Layout>
  );
}
