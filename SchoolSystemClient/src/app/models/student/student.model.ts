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
