"use client";
import { useEffect, useState } from "react";
import { useAxios } from "@/hooks/use-axios";
import ConfirmRemoveUserDialog from "./dialog/ConfirmRemoveUserDialog";
import { useAuth } from "@/context/auth-context";
import { Role } from "@/model/enum";
import { MdCancel, MdEdit } from "react-icons/md";
import { FaTrash } from "react-icons/fa";
import { ImCheckmark } from "react-icons/im";

type User = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  role: string;
};

const AdminPanelPage = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [editingUserId, setEditingUserId] = useState<string | null>(null);
  const [originalUsers, setOriginalUsers] = useState<User[]>([]);
  const [isConfirmRemoveUserDialogOpen, setIsConfirmRemoveUserDialogOpen] =
    useState(false);
  const [userToRemove, setUserToRemove] = useState<string | null>(null);

  const axios = useAxios();
  const { user } = useAuth();

  useEffect(() => {
    const fetchData = async () => {
      const response = await axios.get(`/api/User/all`);
      setUsers(response.data);
      setOriginalUsers(response.data);
      console.log(response.data);
    };
    if (user?.role === Role.Admin) {
      fetchData();
    }
  }, []);

  const handleEditClick = (id: string) => {
    setEditingUserId(id);
  };

  const handleCancelClick = () => {
    setUsers(originalUsers);
    setEditingUserId(null);
  };

  const handleSaveClick = async () => {
    const user = users.find((u) => u.id === editingUserId);
    if (user) {
      try {
        await axios.put(`/api/User/${user.id}`, user);
        setOriginalUsers(users);
        setEditingUserId(null);
      } catch (error) {
        console.error("Error updating user:", error);
      }
    }
  };

  const handleRemoveClick = (id: string) => {
    setUserToRemove(id);
    setIsConfirmRemoveUserDialogOpen(true);
  };

  const handleConfirmRemove = async () => {
    if (userToRemove) {
      try {
        await axios.delete(`/api/User/${userToRemove}`);
        setUsers((prevUsers) => prevUsers.filter((u) => u.id !== userToRemove));
      } catch (error) {
        console.error("Error removing user:", error);
      }
      setIsConfirmRemoveUserDialogOpen(false);
      setUserToRemove(null);
    }
  };

  const roleOptions = ["Admin", "Student", "Teacher"];

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Lista użytkowników</h1>
      </div>
      <h2 className="text-2xl font-semibold w-4/5 m-auto py-4">
        Liczba użytkowników ({users.length})
      </h2>
      <div className="w-full flex flex-wrap justify-center gap-4">
        {users.map((user) => (
          <div
            key={user.id}
            className="w-4/5 h-20 bg-white shadow-md p-4 rounded-lg flex justify-between"
          >
            {editingUserId === user.id ? (
              <>
                <input
                  type="text"
                  value={user.firstName + " " + user.lastName}
                  onChange={(e) => {
                    const [firstName, lastName] = e.target.value.split(" ");
                    setUsers((prevUsers) =>
                      prevUsers.map((u) =>
                        u.id === user.id ? { ...u, firstName, lastName } : u
                      )
                    );
                  }}
                  className="border border-gray-300 p-2 rounded-lg w-1/4 h-full"
                />
                <input
                  type="text"
                  value={user.email}
                  onChange={(e) =>
                    setUsers((prevUsers) =>
                      prevUsers.map((u) =>
                        u.id === user.id ? { ...u, email: e.target.value } : u
                      )
                    )
                  }
                  className="border border-gray-300 p-2 rounded-lg w-1/4 h-full"
                />
                <select
                  value={user.role}
                  onChange={(e) =>
                    setUsers((prevUsers) =>
                      prevUsers.map((u) =>
                        u.id === user.id ? { ...u, role: e.target.value } : u
                      )
                    )
                  }
                  className="border border-gray-300 p-2 rounded-lg w-1/4 h-full"
                >
                  {roleOptions.map((role, index) => (
                    <option key={index} value={role}>
                      {role}
                    </option>
                  ))}
                </select>
                <div className="relative flex gap-4 w-1/4 justify-end items-center">
                  <button
                    onClick={handleCancelClick}
                    className="bg-blue-500 text-white p-2 rounded-lg hover:bg-blue-600 w-10 h-10 flex justify-center items-center"
                  >
                    <MdCancel className="text-3xl" />
                  </button>
                  <button
                    onClick={handleSaveClick}
                    className="bg-green-500 text-white p-2 rounded-lg hover:bg-green-600 w-10 h-10 flex justify-center items-center"
                  >
                    <ImCheckmark className="text-xl" />
                  </button>
                </div>
              </>
            ) : (
              <div className="relative w-full flex justify-between items-center">
                <h2 className="text-xl font-semibold w-1/4">
                  {user.firstName} {user.lastName}
                </h2>
                <p className="w-1/4 text-lg">{user.email}</p>
                <p className="w-1/4 text-lg">{user.role}</p>
                <div className="flex gap-4 w-1/4 justify-end">
                  <button
                    onClick={() => handleEditClick(user.id)}
                    className="bg-yellow-500 text-white p-2 rounded-lg hover:bg-yellow-600 w-10 h-10 flex justify-center items-center"
                  >
                    <MdEdit className="text-2xl" />
                  </button>
                  <button
                    onClick={() => handleRemoveClick(user.id)}
                    className="bg-red-500 text-white p-2 rounded-lg hover:bg-red-600 w-10 h-10 flex justify-center items-center"
                  >
                    <FaTrash className="text-xl" />
                  </button>
                </div>
              </div>
            )}
          </div>
        ))}
      </div>
      <ConfirmRemoveUserDialog
        isOpen={isConfirmRemoveUserDialogOpen}
        onClose={() => setIsConfirmRemoveUserDialogOpen(false)}
        onRemove={handleConfirmRemove}
      />
    </div>
  );
};

export default AdminPanelPage;
