import { Country } from './Country';
import { District } from './District';

export interface City {
    cityName: string;
    country: Country;
    latitude: number;
    longitude: number;
    population: number;
    region: string;
    districts: District[];

}
