export interface StudentGetAllActiveResponse {
    fullName: string;
    age: number;
    email: string; 
    schoolNumber: string;
    totalCredit: number;
    courses: string[]; 
}

export interface StudentGetAllResponse {
    fullName: string;
    age: number;
    schoolNumber: string;
    isActive: boolean;
}

export interface StudentGetByEmailAndIdResponse {
    fullName: string;
    age: number;
    email: string;
    schoolNumber: string;
    totalCredit: number;
    isActive: boolean;
    createdAt: Date;
    courses: string[];
}

export interface StudentUpdateRequest {
    id: string;
    name: string;
    surname: string;
    age: number;
}

export interface CustomJwtPayload {
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string; 
    'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
    SchoolNumber: string;
    exp: number;
    iss: string;
    aud: string;
}
