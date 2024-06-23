"use client";
import { useParams } from "next/navigation";
import TaskElement from "./components/Task";
import InvitesDialog from "./components/InvitesDialog";
import { useEffect, useState } from "react";
import { useAxios } from "@/hooks/use-axios";
import AddTaskDialog from "./components/AddTaskDialog";
import { CgLayoutGrid } from "react-icons/cg";
import AddCourseDialog from "../../../../components/AddEditCourseDialog";
import UsersDialog from "./components/UsersDialog";
import { CourseInfo, Invite, Task } from "@/model/types";
import AddEditCourseDialog from "../../../../components/AddEditCourseDialog";
import { useAuth } from "@/context/auth-context";
import { Role } from "@/model/enum";

const CoursePage = () => {
  const { id } = useParams<{ id: string }>();
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false); // [id, sender, message
  const [isInvitesDialogOpen, setIsInvitesDialogOpen] = useState(false);
  const [isUsersDialogOpen, setIsUsersDialogOpen] = useState(false);
  const [isAddTaskDialogOpen, setIsAddTaskDialogOpen] = useState(false); // [id, title, deadline, description, status, repositoryId, userId, createdAt, updatedAt
  const [invites, setInvites] = useState<Invite[] | []>([]); // [id, sender, message
  const [tasks, setTasks] = useState<Task[]>([]); // [id, title, deadline, description, status, repositoryId, userId, createdAt, updatedAt
  const [repoInfo, setRepoInfo] = useState<CourseInfo>(); // [id, name, description, createdAt, updatedAt, userId, courseId, course, user

  const axios = useAxios();
  const { user } = useAuth();

  const getTasks = async () => {
    const responseTasks = await axios.get(`/api/Task/repository/${id}`);
    setTasks(responseTasks.data);
  };

  const getInvites = async () => {
    const response = await axios.get(`/api/Ticket/pending`);
    console.log(response.data);
    setInvites(response.data);
  };

  const getRepoInfo = async () => {
    const responseRepo = await axios.get(`/api/Repository/${id}`);
    console.log(responseRepo.data);
    setRepoInfo(responseRepo.data);
  };

  useEffect(() => {
    const fetchData = async () => {
      await getRepoInfo();
      await getTasks();
      console.log(repoInfo);
      if (user?.role === Role.Teacher && user.id === repoInfo?.ownerId) {
        await getInvites();
      }
    };
    fetchData();
  }, [user?.role, user?.id, repoInfo?.ownerId]);

  const addTask = async (title: string, description: string, date: string) => {
    console.log(date);
    const response = await axios.post(`/api/Task/repository/${id}`, {
      title,
      description,
      deadline: date,
    });
    await getTasks();
  };

  const changeStatus = async (id: string, status: string) => {
    const response = await axios.put(`/api/Ticket/${id}`, { status });
    await getInvites();
    await getRepoInfo();
  };

  const removeUserFromCourse = async (userId: string) => {
    const response = await axios.post(`/api/Repository/remove-user/${id}`, {
      userId,
    });
    await getRepoInfo();
  };

  const editCourse = async (name: string) => {
    console.log(name);
    const response = await axios.put(`/api/Repository/${id}`, {
      name,
    });
  };

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">{repoInfo?.name}</h1>
      </div>
      <div className="relative flex m-8 justify-center">
        <p className="text-3xl font-semibold">
          Witamy w kursie: {repoInfo?.name}
        </p>
        {user?.role !== Role.Student && user?.id === repoInfo?.ownerId && (
          <div className="flex flex-col justify-end absolute right-2 gap-4">
            <button
              onClick={() => setIsEditDialogOpen(true)}
              className="bg-yellow-500 text-white px-4 py-2 rounded-lg hover:bg-yellow-600 "
            >
              Edycja
            </button>
            <button
              onClick={() => setIsUsersDialogOpen(true)}
              className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 "
            >
              Uczestnicy
            </button>
            <button
              className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 "
              onClick={() => setIsAddTaskDialogOpen(true)}
            >
              Dodaj zadanie
            </button>
            <button
              className="relative bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600"
              onClick={() => setIsInvitesDialogOpen(true)}
            >
              Zaproszenia
              {invites.length > 0 && (
                <div className="bg-red-500 absolute right-[-.5rem] top-[-.5rem] w-6 h-6 rounded-full flex justify-center items-center">
                  {invites.length}
                </div>
              )}
            </button>
          </div>
        )}
      </div>
      <p className="text-2xl font-semibold w-2/5 m-auto pb-4">Lista zadań:</p>
      <div className="flex flex-col items-center gap-4 justify-center">
        {tasks.map((task) => (
          <TaskElement key={task.id} task={task} />
        ))}
      </div>
      <InvitesDialog
        invites={invites}
        isOpen={isInvitesDialogOpen}
        onClose={() => setIsInvitesDialogOpen(false)}
        onChangeStatus={(id, status) => changeStatus(id, status)}
      />
      <AddTaskDialog
        isOpen={isAddTaskDialogOpen}
        onClose={() => setIsAddTaskDialogOpen(false)}
        onAddTask={addTask}
      />
      <AddEditCourseDialog
        isOpen={isEditDialogOpen}
        onClose={() => setIsEditDialogOpen(false)}
        onAddCourse={editCourse}
      />
      <UsersDialog
        isOpen={isUsersDialogOpen}
        onClose={() => setIsUsersDialogOpen(false)}
        users={repoInfo?.users}
        repoId={id}
        removeUser={(userId) => removeUserFromCourse(userId)}
      />
    </div>
  );
};

export default CoursePage;
