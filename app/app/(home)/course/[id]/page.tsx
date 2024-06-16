"use client";
import { useParams } from "next/navigation";
import TaskElement from "./components/Task";
import InvitesDialog from "./components/InvitesDialog";
import { useEffect, useState } from "react";
import { useAxios } from "@/hooks/use-axios";
import AddTaskDialog from "./components/AddTaskDialog";
import { CgLayoutGrid } from "react-icons/cg";

const CoursePage = () => {
  const { id } = useParams<{ id: string }>();
  const [isInvitesDialogOpen, setIsInvitesDialogOpen] = useState(false);
  const [isAddTaskDialogOpen, setIsAddTaskDialogOpen] = useState(false); // [id, title, deadline, description, status, repositoryId, userId, createdAt, updatedAt
  const [invites, setInvites] = useState([]); // [id, sender, message

  const axios = useAxios();

  const tmp = [
    {
      id: "1",
      title: "XD",
      deadline: "2024-06-15T16:48:53.374Z",
    },
    {
      id: "2",
      title: "Drugie zadanie",
      deadline: "2024-12-15T16:48:53.374Z",
    },
  ];

  useEffect(() => {
    const fetchData = async () => {
      const response = await axios.get(`/api/Ticket/pending`);
      setInvites(response.data);
      console.log(response.data);
    };
    fetchData();
  }, []);

  const addTask = async (title: string, description: string, date: string) => {
    console.log(date);
    const response = await axios.post(`/api/Task/repository/${id}`, {
      title,
      description,
      date,
    });
    console.log(response);
  };

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Nazwa kursu</h1>
      </div>
      <div className="relative flex m-8 justify-center">
        <p>{id}</p>
        <div className="absolute right-2 flex gap-4">
          <button
            className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 "
            onClick={() => setIsAddTaskDialogOpen(true)}
          >
            Dodaj zadanie
          </button>
          <button
            className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 "
            onClick={() => setIsInvitesDialogOpen(true)}
          >
            Zaproszenia
          </button>
        </div>
      </div>
      <p className="text-2xl font-semibold w-2/5 m-auto pb-4">Lista zadań:</p>
      <div className="flex flex-col items-center gap-4 justify-center">
        <TaskElement task={tmp[0]} />
        <TaskElement task={tmp[1]} />
      </div>
      <InvitesDialog
        invites={invites}
        isOpen={isInvitesDialogOpen}
        onClose={() => setIsInvitesDialogOpen(false)}
      />
      <AddTaskDialog
        isOpen={isAddTaskDialogOpen}
        onClose={() => setIsAddTaskDialogOpen(false)}
        onAddTask={addTask}
      />
    </div>
  );
};

export default CoursePage;
