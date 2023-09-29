import defaultImg from "../assets/images/profiesImg/profiesDefault.png"
import img1 from "../assets/images/img-7.jpg";
import img2 from "../assets/images/img-8.jpg";
import img3 from "../assets/images/img-4.jpg";
import img4 from "../assets/images/img-1.jpg";
import img5 from "../assets/images/img-2.jpg";
import img6 from "../assets/images/img-5.jpg";
/*
ไปไหน เกาะล้าน
กี่คน 2-100
เจอกันกี่โมง 12.00 
ที่ไหน   สถานีกรุงเทพ
อายุ 16-20
วันไหน 12/8/23
กี่วัน 2วัน 1คืน
*/

const posts = [
    {
        id: 1,
        name: 'John Doe',
        image: defaultImg,
        destination: "เกาะล้าน",
        tripImg: img1,
        people: "2-100",
        time: "12.00",
        place: "สถานีกรุงเทพ",
        date: "12/8/23",
        duration: "2วัน 1คืน",
        age: "16-20",
        contact: "094-xxx-xxxx"   
    },
    {
        id: 2,
        name: 'Bob',
        image: defaultImg,
        destination: "หาดบางแสน",
        tripImg: img2,
        people: "2-10",
        time: "8.00",
        place: "สถานีกรุงเทพ",
        date: "30/9/23",
        duration: "2วัน 1คืน",
        age: "18-22",
        contact: "084-xxx-xxxx"
    },
    {
        id: 3,
        name: 'Per',
        image: defaultImg,
        destination: "หาดบางแสน",
        tripImg: img3,
        people: "2-10",
        time: "8.00",
        place: "สถานีกรุงเทพ",
        date: "30/9/23",
        duration: "2วัน 1คืน",
        age: "18-22",
        contact: "091-xxx-xxxx"
    },
    {
        id: 4,
        name: 'Pocky',
        image: defaultImg,
        destination: "หาดบางแสน",
        tripImg: img4,
        people: "2-10",
        time: "8.00",
        place: "สถานีกรุงเทพ",
        date: "30/9/23",
        duration: "2วัน 1คืน",
        age: "18-22",
        contact: "090-xxx-xxxx"
    },
     {
        id: 5,
        name: 'Top',
        image: defaultImg,
        destination: "หาดบางแสน",
        tripImg: img5,
        people: "2-10",
        time: "8.00",
        place: "สถานีกรุงเทพ",
        date: "30/9/23",
        duration: "2วัน 1คืน",
        age: "18-22",
        contact: "096-xxx-xxxx"
    },
    {
        id: 6,
        name: 'One',
        image: defaultImg,
        destination: "หาดบางแสน",
        tripImg: img6,
        people: "2-10",
        time: "8.00",
        place: "สถานีกรุงเทพ",
        date: "30/9/23",
        duration: "2วัน 1คืน",
        age: "18-22",
        contact: "089-xxx-xxxx"
    }
]

export default posts