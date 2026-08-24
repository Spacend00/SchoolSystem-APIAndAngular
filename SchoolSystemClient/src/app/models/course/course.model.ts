export interface CourseResponse {
    id: string;
    name: string;
    credit: number;
    createdAt: Date;
}

export interface CourseUpdateRequest {
    id: string;
    name: string;
    credit: number;
}
