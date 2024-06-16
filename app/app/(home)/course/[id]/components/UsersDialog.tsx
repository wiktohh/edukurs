"use client";
import { useAuth } from "@/context/auth-context";
import { useAxios } from "@/hooks/use-axios";
import React, { useEffect, useState } from "react";
import { FaClosedCaptioning } from "react-icons/fa";
import { IoClose, IoPersonRemoveSharp } from "react-icons/io5";
import { MdRemove } from "react-icons/md";

type InvitesDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  users: any[];
  repoId: string;
};
const UsersDialog: React.FC<InvitesDialogProps> = ({
  isOpen,
  onClose,
  users,
  repoId,
}) => {
  if (!isOpen) return null;

  const axios = useAxios();

  const { user } = useAuth();

  const removeUserFromCourse = async (userId: string) => {
    const response = await axios.post(`/api/Repository/remove-user/${repoId}`, {
      id: userId,
    });
    console.log(response);
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="relative bg-white rounded-lg p-8 shadow-lg">
        <IoClose
          className="text-4xl hover:text-red-500 text-black absolute top-1 right-1 cursor-pointer"
          onClick={onClose}
        />
        <h2 className="text-2xl font-semibold mb-4">
          Lista studentów w kursie
        </h2>
        {users.map((member) => (
          <div key={member.id} className="mb-4 flex justify-between">
            <h3 className="text-lg font-semibold">
              {member.firstName} {member.lastName}
            </h3>
            {user.id !== member.id && (
              <button onClick={() => removeUserFromCourse(member.id)}>
                <IoPersonRemoveSharp className="text-2xl hover:text-red-500" />
              </button>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};

export default UsersDialog;
