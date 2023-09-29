import { Link } from "react-router-dom";
import "./styles.css";

type Props = {
  post: any;
};

export default function PostItem({ post }: Props) {
  return (
    <div
      // to={`/community/${post.id}/${post.name}`}
      className=" bg-zinc-50/40 border-none border-[1.1px] p-5 md:mb-0 mb-4 rounded-md"
    >
      <Link to={`/community/${post.id}/${post.name}`}>
        <div className=" flex items-center">
          <img
            src={post.image}
            width={46}
            height={46}
            alt="profile"
            className=" rounded-sm"
          />
          <p className=" font-semibold ml-2 ">{post.name}</p>
        </div>
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
        {/* <div>
          <p>{post.date}</p>
          <p>{post.time}</p>
          <p>{post.place}</p>
        </div> */}
      </Link>
    </div>
  );
}
