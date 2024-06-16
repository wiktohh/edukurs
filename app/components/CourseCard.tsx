"use client";
import { useAxios } from "@/hooks/use-axios";
import { useRouter } from "next/navigation";

type CourseCardProps = {
  courseName: string;
  teacherName: string;
  id: string;
  isEnrolled: boolean;
};

const CourseCard: React.FC<CourseCardProps> = ({
  courseName,
  teacherName,
  id,
  isEnrolled,
}) => {
  const router = useRouter();

  const axios = useAxios();

  const redirectToCourse = () => {
    router.push(`/course/${id}`);
  };

  const sendTicket = () => {
    const response = axios.post("/api/Ticket/send-ticket", {
      repositoryId: id,
    });
    console.log(response);
  };

  return (
    <div className="w-[30%]">
      <div className="bg-white shadow-md p-4">
        <h2 className="text-xl font-semibold">{courseName}</h2>
        <p className="text-sm text-gray-500">Nauczyciel: {teacherName}</p>
        {isEnrolled ? (
          <button
            onClick={redirectToCourse}
            className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600 mt-4"
          >
            Przejdź do kursu
          </button>
        ) : (
          <button
            onClick={sendTicket}
            className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600 mt-4"
          >
            Zapisz się
          </button>
        )}
      </div>
    </div>
  );
};

export default CourseCard;
