

import { Sex } from './sex';

export interface ForViewModel {


    id?: string;
    login?: string;
    firstName?: string;
    lastName?: string;
    sex: Sex;
    birthday: Date;
    registrationDate: Date;
    rating: number;
    email?: string;
    phoneNumber?: string;
}
