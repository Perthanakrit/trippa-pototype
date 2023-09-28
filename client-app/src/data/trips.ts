import img1 from "../assets/images/img-1.jpg";
import img2 from "../assets/images/img-2.jpg";
import img3 from "../assets/images/img-3.jpg";
import img4 from "../assets/images/img-4.jpg";
import img5 from "../assets/images/img-5.jpg";

const trips = [
    {
        id: 1,
        name: "ไหว้พระขอพรจากเที่ยวเชียงใหม่-ลำปาง",
        tag: 1,
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
        tag: 1,
        description: "เดินทางไหว้ไปวัดต่างๆ ตั้งแต่ออกจากกรุงเทพถถึงสุดอีสานตอนล่าง อย่างจังหวัดอุบลราชธานี พร้อมดื่มดำ่ไปกับธรรมชาติ ",
        image: img2,
        origin: "กรุงเทพ",
        destination: "เมืองอุบล",
        landmark: "วัดสิรินธรวรารามภูพร้าว",
        duration: "3 วัน 2 คืน",
        price: "2000",
        fee: "7"
    },
    {
        id: 3,
        name: "เที่ยวเชียงใหม่-ลำปาง",
        tag: 2,
        description: "ท่องเที่ยวเดินทางชมวิว ตั้งแต่เชียงใหม่ถึงลำปาง ด้วยรถไฟ",
        image: img3,
        origin: "เมืองเชียงใหม่",
        destination: "เมืองลำปาง",
        landmark: "สถานีรถไฟขุนตาล",
        duration: "2 วัน 1 คืน",
        price: "1200",
        fee: "5"
    },
    {
        id:4,
        name: "ล่องเรือกลางแม่น้ำเจ้าพระยา",
        tag: 2,
        description: "ชมวิวแม่น้ำเจ้าพระยา ด้วยเรือท่องเที่ยว",
        image: img4,
        origin: "เมืองเชียงใหม่",
        destination: "เมืองลำปาง",
        landmark: "สะพานพระราม 9",
        duration: "1 วัน 1 คืน",
        price: "1000",
        fee: "4"
    },
    {
        id:5,
        name: "เที่ยวใกล้กรุง",
        tag: 2,
        description: "สูดบรรยากาศนอกเมื่อยามว่าง",
        image: img5,
        origin: "กรุงเทพ",
        destination: "นครปฐม",
        landmark: "",
        duration: "1 วัน",
        price: "500",
        fee: "2.1"
    }

]
export default trips;