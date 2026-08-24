export interface RegisterRequestStudent {
    name: string;
    surname: string;
    age: number;
    schoolNumber: string;
    email: string;
    password: string;
}

export interface RegisterRequestTeacher {
    name: string;
    surname: string;
    age: number;
    branch: number;
    email: string;
    password: string;
}

export interface RegisterResponse {
    id: string;
    email: string;
}