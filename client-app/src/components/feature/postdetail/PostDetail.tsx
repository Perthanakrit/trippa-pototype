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
      <div className=" container md:grid md:grid-cols-2 h-[720px]">
        <div>
          <img
            src={post.tripImg}
            alt="activity"
            className=" w-full h-[300px] object-none rounded"
          />
          {/* <p className=" font-semibold ml-2 ">{post.name}</p> */}
        </div>
        <div className=" ml-5 mt-4 md:mt-0">
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
            <h1>นัดหมาย</h1>
            <div className=" h-[60px] bg-black w-[1px] mx-3 "></div>
            <div>
              <p>วันที่ {post.date}</p>
              <p>เวลา {post.time}</p>
              <p>ณ {post.place}</p>
            </div>
          </div>
          <p className="inline-flex items-center">
            ติดต่อ <BsTelephoneFill size={18} className=" ml-1 mr-2" />{" "}
            {post.contact}
          </p>
        </div>
      </div>
    </Layout>
  );
}
