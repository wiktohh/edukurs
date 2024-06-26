"use client";
import { useAxios } from "@/hooks/use-axios";
import { Status } from "@/model/enum";
import React, { useEffect, useState } from "react";
import { FaClosedCaptioning } from "react-icons/fa";
import { IoClose } from "react-icons/io5";

type InvitesDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  invites: any[];
  onChangeStatus: (id: string, status: string) => void;
};

const InvitesDialog: React.FC<InvitesDialogProps> = ({
  isOpen,
  onClose,
  invites,
  onChangeStatus,
}) => {
  if (!isOpen) return null;

  const axios = useAxios();

  const changeStatusOfInvite = async (id: string, status: string) => {
    onChangeStatus(id, status);
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
                    onClick={() =>
                      changeStatusOfInvite(invite.id, Status.Rejected)
                    }
                    className="bg-red-500 text-white px-2 py-1 rounded-lg"
                  >
                    Odrzuć
                  </button>
                  <button
                    onClick={() =>
                      changeStatusOfInvite(invite.id, Status.Approved)
                    }
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

export default InvitesDialog;
