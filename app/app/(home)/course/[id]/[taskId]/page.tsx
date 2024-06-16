"use client";
import { useParams, usePathname, useRouter } from "next/navigation";
import FileUpload from "./FileUpload";
import { useAxios } from "@/hooks/use-axios";
import { useEffect, useState } from "react";
import { convertDate } from "@/utils/convert-date";

const TaskPage = () => {
  const pathname = usePathname();
  const router = useRouter();
  const axios = useAxios();

  const taskId = pathname.split("/").pop();

  const [taskInfo, setTaskInfo] = useState<any>({});

  const tmp = [
    {
      title: "XD",
      deadline: "2024-06-15T16:48:53.374Z",
    },
    {
      title: "Drugie zadanie",
      deadline: "2024-12-15T16:48:53.374Z",
    },
  ];

  const getTaskInfo = async () => {
    console.log(pathname.split("/").pop());
    const response = await axios.get(`/api/Task/${taskId}`);
    console.log(response.data);
    setTaskInfo({
      ...response.data,
      deadline: convertDate(response.data.deadline),
    });
  };

  useEffect(() => {
    getTaskInfo();
  }, []);

  const colorOfDeadline =
    new Date(taskInfo.deadline) < new Date()
      ? "text-red-500"
      : "text-green-500";

  const backToPreviousPage = () => {
    router.push(pathname.split("/").slice(0, -1).join("/"));
  };

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="flex justify-between shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">{taskInfo.title}</h1>
        <button
          onClick={backToPreviousPage}
          className="text-gray-500 hover:text-gray-700 px-4 uppercase font-medium"
        >
          Cofnij
        </button>
      </div>
      <div className="w-2/5 m-auto flex flex-col items-center justify-center">
        <div className="w-full py-8 flex justify-between">
          <p className="font-bold">{taskInfo.title}</p>
          <p className={`${colorOfDeadline}`}>{taskInfo.deadline}</p>
        </div>
        <div className="pb-8">{taskInfo.description}</div>
        <FileUpload />
      </div>
    </div>
  );
};

export default TaskPage;
