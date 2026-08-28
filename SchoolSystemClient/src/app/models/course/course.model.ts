export interface CourseResponse {
    id: string;
    name: string;
    description: string;
    imageData: string;
}

export interface CourseUpdateRequest {
    id: string;
    name: string;
    credit: number;
}

export interface CourseResponseById {
    name: string;
    description: string;
    imageData: string;
    goal: string;
    summary: string;
    targetGroup: string;
    gains: string;
    requirements: string;
}

export interface CreateCourseResponse {
    id: string;
}

export interface CreateCourseRequest {
    teacherId: any;
    name: string;
    description: string;
    imageData: string;
    goal: string;
    summary: string;
    targetGroup: string;
    gains: string;
    requirements: string;
}