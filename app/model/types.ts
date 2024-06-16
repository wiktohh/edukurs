export type Course = {
  courseName: string;
  teacherName: string;
  id: string;
  isEnrolled: boolean;
};

export type User = {
  email: string;
  firstName: string;
  id: string;
  lastName: string;
  role: string;
};

export type CourseInfo = {
  id: string;
  name: string;
  ownerId: string;
  users: User[];
};

export type Task = {
  title: string;
  description: string;
  deadline: string;
  id: string;
  repositoryId: string;
};

export type Invite = {
  email: string;
  firstName: string;
  id: string;
  lastName: string;
  repositoryId: string;
  status: string;
  userId: string;
};
