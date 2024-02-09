import { CustomTrip } from './../types/tirp';
import axios, { AxiosError, AxiosResponse, InternalAxiosRequestConfig } from "axios";
import { User, UserFormValues } from "../types/user";
import { Trip } from "../types/tirp";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = process.env.APP_API_URL;

const apiKey = process.env.API_KEY;
axios.defaults.headers.common['X-API-Key'] = apiKey;

axios.interceptors.request.use((config : InternalAxiosRequestConfig<any>) => {
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXIxIiwibmFtZWlkIjoiMTNiNTE2OTAtYTZlMS00MGI1LWFlN2ItNjUxNjI2ZjA3YTgwIiwiZW1haWwiOiJ1c2VyMUB0ZXN0LmNvbSIsIm5iZiI6MTcwNzQ2NDExNywiZXhwIjoxNzA3NTUwNTE3LCJpYXQiOjE3MDc0NjQxMTd9.w9ryxhIB4I4zLNmqHnw8wH1cLHrzrRzdzcIOQhd12yU";
    
    if (apiKey) {
        config.headers.Accept = 'application/json';
    }

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
})

axios.interceptors.response.use(async response => {
    await sleep(600);
    return response;
}, (error:AxiosError) => {

    const {data, status, config} = error.response as AxiosResponse;

    if (status === 404) {
        throw new Error('Not found');
    }
})

const responseBody =<T> (response: AxiosResponse<T>) => response.data;

const request = {
    get:<T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T> (url, body).then(responseBody),
    put: <T> (url: string, body: {}) => axios.put<T> (url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T> (url).then(responseBody),
}

const Trips = {
    list: () => request.get<Trip[]>('/Trip/GetListOfAllTrips'),
    details: (id: string) => request.get<CustomTrip>(`/trips/${id}`),
    create: (trip: any) => request.post('/trips', trip),
    update: (trip: any) => request.put(`/trips/${trip.id}`, trip),
    delete: (id: string) => request.del(`/trips/${id}`)
}

const CustomTrips = {
    list: () => request.get<CustomTrip[]>('/CustomTrip'),
    details: (id: string) => request.get<CustomTrip>(`/customTrip/${id}`),
    create: (customTrip: any) => request.post('/customTrips', customTrip),
    update: (customTrip: any) => request.put(`/customTrips/${customTrip.id}`, customTrip),
    delete: (id: string) => request.del(`/customTrips/${id}`)
}

const Account = {
    current: () => request.get<User>('/account'),
    login: (user: UserFormValues) => request.post<User>('account/login', user),
    register: (user: UserFormValues) => request.post<User>('account/register', user)
}

const agent = {
    Trips,
    CustomTrips,
}

export default agent;
