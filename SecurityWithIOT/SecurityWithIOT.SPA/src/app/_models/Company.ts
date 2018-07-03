import { Department } from './Department';
import { User } from './User';

export interface Company {
    id: number;
    companyname: string;
    tel: string;
    fax: string;
    mail: string;
    departments: Department[];
    users: User[];
}
