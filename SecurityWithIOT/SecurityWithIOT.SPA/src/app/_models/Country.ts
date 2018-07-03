import { City } from './City';

export interface Country {
    id: number;
    countryName: string;
    countryCode: string;
    cities: City[];
}
