"use client";
import { useEffect, useState } from "react";
import CourseCard from "../../../components/CourseCard";
import AddCourseDialog from "./dialog/AddCourseDialog";
import { useAxios } from "@/hooks/use-axios";
import { Course } from "../all-courses/page";

const HomePage = () => {
  const tmp = [
    {
      courseName: "Kurs React",
      teacherName: "Wiktor Rzeźnicki",
      id: "1",
      isEnrolled: true,
    },
    {
      courseName: "Kurs React",
      teacherName: "Wiktor Rzeźnicki",
      id: "4",
      isEnrolled: true,
    },
  ];

  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [courses, setCourses] = useState<Course[]>([]);

  const axios = useAxios();

  const handleAddCourse = (courseName: string) => {
    const response = axios.post("/api/Repository", { name: courseName });
    console.log(response);
  };

  const getOwnerOfCourse = async (userId: string) => {
    const response = await axios.get(`api/User/${userId}`);
    console.log(response.data);
    return `${response.data.firstName} ${response.data.lastName}`;
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await axios.get("api/Repository?repoEnum=1");
      const coursesTmp = await Promise.all(
        response.data.map(async (courseTmp: any) => {
          const teacher = await getOwnerOfCourse(courseTmp.ownerId);
          console.log(courseTmp);
          return {
            courseName: courseTmp.name,
            teacherName: teacher,
            id: courseTmp.id,
            isEnrolled: true,
          };
        })
      );
      setCourses(coursesTmp);
    };
    fetchData();
  }, []);

  return (
    <div className="bg-gray-100 flex-grow ">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Moje kursy</h1>
      </div>
      <div className="flex justify-end">
        <button
          onClick={() => setIsDialogOpen(true)}
          className="bg-blue-500 hover:bg-blue-600 px-4 py-2 text-white rounded-lg m-4 ml-auto"
        >
          Stwórz kurs
        </button>
      </div>
      <div className="flex flex-wrap gap-8 m-8 justify-center">
        {courses.length === 0 && (
          <div className="text-4xl text-black">
            Nie należysz do żadnego kursu
          </div>
        )}
        {courses.map((course) => (
          <CourseCard key={course.id} {...course} />
        ))}
      </div>
      <AddCourseDialog
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onAddCourse={(courseName) => handleAddCourse(courseName)}
      />
    </div>
  );
};

export default HomePage;
