import PostItem from "./PostItem";

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
  return (
    <div className=" md:grid md:grid-cols-3 gap-4 mt-4">
      {/* <div>
        <div>
          <img src="" alt="profile" />
          <p>Name</p>
        </div>
        <div>
          <p>ไปเกาะล้าน</p>
          <p>2-100</p>
          <p>จำนวน 2 วัน 1 คืน</p>
        </div>
        <div>
          <p>12/8/23</p>
          <p>12.00</p>
          <p>สถานีกรุงเทพ</p>
        </div>
        <h4>อายุ 16-20</h4>
      </div>
      <div>
      </div>   */}
      {posts.map((post: any) => (
        <PostItem key={post.id} post={post} />
      ))}
    </div>
  );
}
