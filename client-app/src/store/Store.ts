import { configureStore } from "@reduxjs/toolkit"
import { useDispatch } from "react-redux";
import customTripSlice from "./slices/customTripSlice";

const reducer = {
    // นำ slice ที่เราสร้างมาใส่ในนี้
    customTripSlice,
}

const store = configureStore({
    reducer,
    devTools: process.env.NODE_ENV === 'development'
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch; // นิยาม type ของ dispatch ให้เป็น typeof store.dispatch
export const useAppDispatch = () => useDispatch<AppDispatch>();
export default store;