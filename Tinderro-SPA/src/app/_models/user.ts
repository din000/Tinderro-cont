import { Photo } from './photo';

// to bylo robione z automatu ale mozna tez recznie
// skopiowalem z postmana to co dostalem z httpget (pojedynczego usera) i wkleilem na strone json to ts
// potem wkleilem to tutaj
// - ogolnie mozna recznie ale tak bylo szybciej

export interface User {
    id: number;
    username: string;
    gender: string;
    age: number;
    zodiacSign: string;
    created: Date;
    lastActive: Date;
    city: string;
    country: string;
    growth: string;
    eyeColor: string;
    hairColor: string;
    martialStatus: string;
    education: string;
    profession: string;
    children: string;
    languages: string;
    motto: string;
    description: string;
    personality: string;
    lookingFor: string;
    interests: string;
    freeTime: string;
    sport: string;
    movies: string;
    music: string;
    iLike: string;
    idoNotLike: string;
    makesMeLaugh: string;
    itFeelsBestIn: string;
    friendeWouldDescribeMe: string;
    photos: Photo[];
    photoUrl: string;
}
