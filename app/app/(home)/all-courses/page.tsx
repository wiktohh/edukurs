"use client";
import { useEffect, useState } from "react";
import CourseCard from "../../../components/CourseCard";
import { useAxios } from "@/hooks/use-axios";
import { useAuth } from "@/context/auth-context";

export type Course = {
  courseName: string;
  teacherName: string;
  id: string;
  isEnrolled: boolean;
};

const AllCoursesPage = () => {
  const [courses, setCourses] = useState<Course[]>([]);

  const axios = useAxios();
  const { user } = useAuth();

  const getOwnerOfCourse = async (userId: string) => {
    const response = await axios.get(`api/User/${userId}`);
    console.log(response.data);
    return `${response.data.firstName} ${response.data.lastName}`;
  };

  useEffect(() => {
    const fetchData = async () => {
      console.log(user);
      const response = await axios.get("api/Repository?repoEnum=0");
      const coursesTmp = await Promise.all(
        response.data.map(async (courseTmp: any) => {
          const teacher = await getOwnerOfCourse(courseTmp.ownerId);
          console.log(courseTmp);
          return {
            courseName: courseTmp.name,
            teacherName: teacher,
            id: courseTmp.id,
            isEnrolled: courseTmp.users.some((u: any) => u.id === user?.id),
          };
        })
      );
      setCourses(coursesTmp);
    };
    fetchData();
  }, []);

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Wszystkie kursy</h1>
      </div>
      <div className="flex flex-wrap justify-center gap-8 m-12">
        {courses.length === 0 && (
          <div className="text-4xl text-black">Brak kursów</div>
        )}
        {courses.map((course) => (
          <CourseCard key={course.id} {...course} />
        ))}
      </div>
    </div>
  );
};

export default AllCoursesPage;
