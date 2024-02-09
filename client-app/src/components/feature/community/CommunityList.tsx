import { useSelector } from "react-redux";
import PostItem from "./PostItem";
import { customTripSelector } from "../../../store/slices/customTripSlice";
import { CustomTrip } from "../../../types/tirp";
import { BeatLoader } from "react-spinners";

type Props = {
  posts: Array<any>;
};
/*
ไปไหน เกาะล้าน
กี่คน 2-100
เจอกันกี่โมง 12.00 
ที่ไหน   สถานีกรุงเทพ
อายุ 16-20
วันไหน 12/8/23
กี่วัน 2วัน 1คืน
*/

export default function CommunityList({ posts }: Props) {
  const { customTrips, loading } = useSelector(customTripSelector);

  return customTrips.length > 0 ? (
    <div className=" md:grid md:grid-cols-3 gap-4 mt-4">
      {customTrips.map((post: CustomTrip, idx: number) => (
        <PostItem key={idx} post={post} />
      ))}
    </div>
  ) : (
    <div className="w-full">
      <div className=" w-fit mx-auto">
        <BeatLoader color="#FF8000" size="20" margin={5} />
      </div>
    </div>
  );
}
