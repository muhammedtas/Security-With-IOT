import { Company } from './Company';
import { User } from './User';

export interface Department {
    id: number;
    departmentname: string;
    company: Company;
    users: User[];
    tel: string;
    fax: string;
    mail: string;
}
