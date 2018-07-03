import { Photo } from './Photo';
import { Department } from './Department';
import { Address } from './Address';
import { Company } from './Company';

export interface User {
    id: number;
    username: string;
    firstname: string;
    lastname: string;
    knownAs: string;
    age: number;
    gender: string;
    lastEnterance: Date;
    title: string;
    nationalIdentificationNumber?: string;
    createdDate?: Date;
    dateOfBirth?: Date;
    introduction?: string;
    photoUrl?: string;
    photos?: Photo[];
    // address?: Address[];
    // department?: Department;
    // company?: Company;
}
