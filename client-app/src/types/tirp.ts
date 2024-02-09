import { UUID } from "crypto";

export interface Trip
{
    id: UUID;
    name: string;
    description: string;
    landmark: string;
    price: number;
    fee: number;
    duration: string;
    origin: string;
    destination: string;
    hostname: string;
    typeOfTrip: string;
    attendee: TripAttendee[];
    agenda: TripAgenda[];
    createAt: string;
    updateAt: string;
    isActive: boolean;
}

export interface CustomTrip {
    customTripId:UUID;
    trip: Trip;
}

interface TripAttendee {
    displayName: string;
    bio:string;
    image:string;
    contacts: Contact[];
}

interface Contact {
    type: string;
    name: string;
}

interface TripAgenda {
    desciprtion: string;
    date: string;
    time: string;
}

