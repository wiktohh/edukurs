"use client";
import { useParams, usePathname, useRouter } from "next/navigation";
import FileUpload from "./FileUpload";
import { useAxios } from "@/hooks/use-axios";
import { useEffect, useState } from "react";
import { convertDate } from "@/utils/convert-date";
import { useAuth } from "@/context/auth-context";
import { Role } from "@/model/enum";
import EditDeadlineDialog from "./EditDeadlineDialog";
import { displayDate } from "@/utils/display-date";
import { toast } from "react-toastify";

const TaskPage = () => {
  const pathname = usePathname();
  const router = useRouter();
  const axios = useAxios();
  const { user } = useAuth();

  const taskId = pathname.split("/").pop();

  const [taskInfo, setTaskInfo] = useState<any>({});
  const [isEditDeadlineDialogOpen, setIsEditDeadlineDialogOpen] =
    useState(false);

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

  const editDeadline = async (deadline: string) => {
    console.log(deadline);
    await axios.put(`/api/Task/${taskId}`, {
      value: deadline,
    });
    getTaskInfo();
    toast.success("Zaktualizowano termin");
  };

  const getAllFiles = async () => {
    const response = await axios.get(`/api/File/download/task/${taskId}`, {
      responseType: "blob",
    });
    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", `task-${taskId}-files.zip`);
    document.body.appendChild(link);
    link.click();
    link.parentNode?.removeChild(link);
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
      {user?.role !== Role.Student && (
        <div className="flex flex-col justify-end absolute right-2">
          <button
            onClick={getAllFiles}
            className="bg-blue-500 px-4 py-2 text-white rounded-md m-2 mt-4"
          >
            Pobierz
          </button>
          <button
            onClick={() => setIsEditDeadlineDialogOpen(true)}
            className="bg-yellow-500 px-4 py-2 text-white rounded-md m-2"
          >
            Edytuj
          </button>
        </div>
      )}
      <div className="w-2/5 m-auto flex flex-col items-center justify-center">
        <div className="w-full py-8 flex justify-between">
          <p className="font-bold">{taskInfo.title}</p>
          <p className={`${colorOfDeadline}`}>
            {displayDate(taskInfo.deadline)}
          </p>
        </div>
        <div className="pb-8">{taskInfo.description}</div>
        <FileUpload deadline={taskInfo.deadline} />
      </div>
      <EditDeadlineDialog
        isOpen={isEditDeadlineDialogOpen}
        onClose={() => setIsEditDeadlineDialogOpen(false)}
        editDeadline={(deadline) => editDeadline(deadline)}
        prevDeadline={taskInfo.deadline}
      />
    </div>
  );
};

export default TaskPage;
