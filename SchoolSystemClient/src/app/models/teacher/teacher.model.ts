export interface TeacherResponse {
    id: string;
    fullName: string;
    age: number;
    email: string;
    isActive: boolean;
    branch: string;
    createdAt: Date;
}

export interface TeacherUpdateRequest {
    id: string;
    name: string;
    surname: string;
    age: number;
}
