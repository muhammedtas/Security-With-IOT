import { Country } from './Country';
import { City } from './City';
import { District } from './District';
import { User } from './User';

export interface Address {
    id: number;
    description: string;
    country: Country;
    city: City;
    district: District;
    user: User;
}
