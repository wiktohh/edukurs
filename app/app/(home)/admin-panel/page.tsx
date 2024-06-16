"use client";
import { useEffect, useState } from "react";
import { useAxios } from "@/hooks/use-axios";
import ConfirmRemoveUserDialog from "./dialog/ConfirmRemoveUserDialog";

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

  useEffect(() => {
    const fetchData = async () => {
      const response = await axios.get(`/api/User/all`);
      setUsers(response.data);
      setOriginalUsers(response.data);
      console.log(response.data);
    };
    fetchData();
  }, []);

  const handleEditClick = (id: string) => {
    setEditingUserId(id);
  };

  const handleCancelClick = () => {
    setUsers(originalUsers); // Przywróć oryginalne wartości
    setEditingUserId(null);
  };

  const handleSaveClick = async () => {
    const user = users.find((u) => u.id === editingUserId);
    if (user) {
      try {
        await axios.put(`/api/User/${user.id}`, user);
        setOriginalUsers(users); // Aktualizuj oryginalne wartości po zapisaniu
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

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Lista użytkowników</h1>
      </div>
      <p>Lista użytkowników</p>
      <div className="w-full flex flex-wrap justify-center gap-4">
        {users.map((user) => (
          <div
            key={user.id}
            className="w-4/5 bg-white shadow-md p-4 rounded-lg flex justify-between"
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
                  className="border border-gray-300 p-2 rounded-lg"
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
                  className="border border-gray-300 p-2 rounded-lg"
                />
                <input
                  type="text"
                  value={user.role}
                  onChange={(e) =>
                    setUsers((prevUsers) =>
                      prevUsers.map((u) =>
                        u.id === user.id ? { ...u, role: e.target.value } : u
                      )
                    )
                  }
                  className="border border-gray-300 p-2 rounded-lg"
                />
                <div className="flex gap-4">
                  <button
                    onClick={handleCancelClick}
                    className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600"
                  >
                    Anuluj
                  </button>
                  <button
                    onClick={handleSaveClick}
                    className="bg-green-500 text-white px-4 py-2 rounded-lg hover:bg-green-600"
                  >
                    Zatwierdź
                  </button>
                </div>
              </>
            ) : (
              <>
                <h2 className="text-xl font-semibold">
                  {user.firstName} {user.lastName}
                </h2>
                <p>{user.email}</p>
                <p>{user.role}</p>
                <div className="flex gap-4">
                  <button
                    onClick={() => handleEditClick(user.id)}
                    className="bg-yellow-500 text-white px-4 py-2 rounded-lg hover:bg-yellow-600"
                  >
                    Edytuj
                  </button>
                  <button
                    onClick={() => handleRemoveClick(user.id)}
                    className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600"
                  >
                    Usuń
                  </button>
                </div>
              </>
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
