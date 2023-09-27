import imgs from "*.jpg";
import img1 from "./assets/images/img-1.jpg";
import img2 from "./assets/images/img-2.jpg";
import img3 from "./assets/images/img-3.jpg";
import img4 from "./assets/images/img-4.jpg";
import img5 from "./assets/images/img-5.jpg";

export const trips = [
  {
    name: "Trip 1",
    description: "Trip 1 description",
    image: img1,
  },
  {
    name: "Trip 2",
    description: "Trip 2 description",
    image: img2,
  },
  {
    name: "Trip 3",
    description: "Trip 3 description",
    image: img3,
  },
  {
    name: "Trip 4",
    description: "Trip 4 description",
    image: img4,
  },
  {
    name: "Trip 5",
    description: "Trip 5 description",
    image: img5,
  },
] as const;


export const muteluhTrips = [
  {
    id:1,
    name: "เที่ยวเชียงใหม่-ลำปาง",
    description: "เดินทางไหว้ไปวัดต่างๆ ตั้งแต่เชียงใหม่ถึงลำปาง ",
    image: img1,
    origin: "เมืองเชียงใหม่",
    destination: "เมืองลำปาง",
    landmark: "วัดพระธาตุลำปางหลวง",
    duration: "2 วัน 1 คืน",
    price: "1000",
    fee: "5"
  },
  {
    id: 2,
    name: "กลางสู่อีสาน",
    description: "เดินทางไหว้ไปวัดต่างๆ ตั้งแต่ออกจากกรุงเทพถถึงสุดอีสานตอนล่าง อย่างจังหวัดอุบลราชธานี พร้อมดื่มดำ่ไปกับธรรมชาติ ",
    image: img2,
    origin: "กรุงเทพ",
    destination: "เมืองอุบล",
    landmark: "วัดสิรินธรวรารามภูพร้าว",
    duration: "3 วัน 2 คืน",
    price: "2000",
    fee: "7"
  }
  
];
