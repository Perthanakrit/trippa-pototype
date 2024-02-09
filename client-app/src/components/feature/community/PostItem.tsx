import { Link } from "react-router-dom";
import "./styles.css";
import { CustomTrip } from "../../../types/tirp";
import profileImg from "../../../assets/images/profiesImg/profiesDefault.png";

type Props = {
  post: CustomTrip;
};

export default function PostItem({ post }: Props) {
  return (
    <div
      // to={`/community/${post.id}/${post.name}`}
      className=" bg-zinc-50/40 border-none border-[1.1px] p-5 md:mb-0 mb-4 rounded-md"
    >
      <Link
        to={`/community/${post.customTripId}/${post.trip.name}`}
        className=" flex md:block"
      >
        <div className=" md:flex md:gap-0 md:items-center">
          <img
            src={profileImg}
            alt="profile"
            className=" md:w-[46px] md:h-[42px]   w-[130px] h-auto  rounded"
          />
          <p className=" font-semibold md:ml-2 mt-1">{post.trip.name}</p>
        </div>
        <div className="mt-4 ml-4 md:ml-0">
          <p>
            <span>ไปไหน?</span>
            {post.trip.destination}
          </p>
          {/* <p>
            {" "}
            <span>กี่คน?</span>
            {post.people}
          </p>
          <p>
            <span>อายุ:</span>
            {post.age}
          </p> */}
          <p>
            <span>กี่วัน กี่คืน?</span>
            {post.trip.duration}
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
