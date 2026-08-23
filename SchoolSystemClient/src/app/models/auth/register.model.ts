export interface RegisterRequestStudent {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
}

export interface RegisterRequestTeacher {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    branch: number;
}

export interface RegisterResponse {
    id: string;
    email: string;
}