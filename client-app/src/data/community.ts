import defaultImg from "../assets/images/profiesImg/profiesDefault.png"

/*
ไปไหน เกาะล้าน
กี่คน 2-100
เจอกันกี่โมง 12.00 
ที่ไหน   สถานีกรุงเทพ
อายุ 16-20
วันไหน 12/8/23
กี่วัน 2วัน 1คืน
*/

const place1:string = "https://upload.wikimedia.org/wikipedia/commons/4/44/Ko_Lan_sunset.jpg"
const place2:string = "https://f.ptcdn.info/590/074/000/qybxkklum4lU94rjdUc-o.jpg"
const posts = [
    {
        id: 1,
        name: 'John Doe',
        image: defaultImg,
        destination: "เกาะล้าน",
        tripImg: place1,
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
        tripImg: place2,
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
        tripImg: place2,
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
        tripImg: place2,
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
        destination: "เกาะล้าน",
        tripImg: place1,
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
        tripImg: place2,
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