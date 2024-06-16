"use client";
import { useAxios } from "@/hooks/use-axios";
import React, { useEffect, useState } from "react";
import { FaClosedCaptioning } from "react-icons/fa";
import { IoClose } from "react-icons/io5";

type InvitesDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  invites: any[];
};

enum Status {
  Pending = "Pending",
  Approved = "Approved",
  Rejected = "Rejected",
}

const UsersDialog: React.FC<InvitesDialogProps> = ({
  isOpen,
  onClose,
  invites,
}) => {
  if (!isOpen) return null;

  const axios = useAxios();

  const changeStatus = async (id: string, status: string) => {
    const response = await axios.put(`/api/Ticket/${id}`, { status });
    console.log(response);
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="relative bg-white rounded-lg p-8 shadow-lg">
        <IoClose
          className="text-4xl hover:text-red-500 text-black absolute top-1 right-1 cursor-pointer"
          onClick={onClose}
        />
        <h2 className="text-2xl font-semibold mb-4">Zaproszenia do kursu</h2>
        {invites.length === 0 && (
          <div className="text-center text-xl ">Brak zaproszeń</div>
        )}
        {invites.map((invite) => (
          <div key={invite.id} className="mb-4 flex gap-8">
            <h3 className="text-lg font-semibold">
              {invite.firstName} {invite.lastName}
            </h3>
            <div>
              {invite.status === "Pending" && (
                <>
                  <button
                    onClick={() => changeStatus(invite.id, Status.Rejected)}
                    className="bg-red-500 text-white px-2 py-1 rounded-lg"
                  >
                    Odrzuć
                  </button>
                  <button
                    onClick={() => changeStatus(invite.id, Status.Approved)}
                    className="bg-green-500 text-white px-2 py-1 rounded-lg ml-2"
                  >
                    Zaakceptuj
                  </button>
                </>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default UsersDialog;
