import { useParams } from "react-router-dom";
import Layout from "../../layout/Layout";
import { useEffect, useState } from "react";
import posts from "../../../data/community";
import { BsTelephoneFill } from "react-icons/bs";

export default function PostDetail() {
  const { postId, name } = useParams();
  const [post, setPost] = useState<any>({});

  useEffect(() => {
    setPost(
      posts.find((p: any) => p.id.toString() === postId && p.name === name)
    );
  }, []);

  return (
    <Layout>
      <div className=" container md:grid md:grid-cols-2 mt-4 md:items-center ">
        <img
          src={post.tripImg}
          alt="activity"
          className=" w-full h-[300px] object-fill rounded"
        />
        {/* <p className=" font-semibold ml-2 ">{post.name}</p> */}

        <div className="ml-5 mt-4 md:mt-0 md:py-3 ">
          <div>
            <p>
              <span>ไปไหน?</span>
              {post.destination}
            </p>
            <p>
              {" "}
              <span>กี่คน?</span>
              {post.people}
            </p>
            <p>
              <span>อายุ:</span>
              {post.age}
            </p>
            <p>
              <span>กี่วัน กี่คืน?</span>
              {post.duration}
            </p>
          </div>
          <div className=" flex items-center my-3">
            <span className=" font-bold text-lg">นัดหมาย</span>
            <div className=" h-[60px] bg-black w-[1px] mx-3 "></div>
            <div>
              <p>วันที่ {post.date}</p>
              <p>เวลา {post.time}</p>
              <p>ณ {post.place}</p>
            </div>
          </div>
          <div className=" flex justify-between items-center mt-5 w-full pr-5 pl-2 py-2 bg-zinc-50/60 rounded-lg  -translate-x-3 md:translate-x-0">
            <div className=" w-[70%] flex items-center">
              <img
                src={post.image}
                className=" w-[30%] h-[30%] object-fill mr-1"
              />
              <div>
                <h1>{post.name}</h1>
                <p className=" text-zinc-500 flex items-center">
                  <BsTelephoneFill size={16} className=" ml-1 mr-2" />
                  {post.contact}
                </p>
              </div>
            </div>

            <button className=" bg-[#FD924B] p-2 rounded-full w-[100px]">
              เข้าร่วม
            </button>
          </div>
        </div>
      </div>
    </Layout>
  );
}
