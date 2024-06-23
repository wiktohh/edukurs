"use client";
import React, { useEffect, useState } from "react";

type EditDeadlineDialog = {
  isOpen: boolean;
  onClose: () => void;
  editDeadline: (deadline: string) => void;
  prevDeadline: string;
};

const setToEndOfDay = (date: string) => {
  const taskDate = new Date(date);
  taskDate.setHours(23, 59, 0, 0);
  return taskDate.toISOString();
};

const EditDeadlineDialog: React.FC<EditDeadlineDialog> = ({
  isOpen,
  onClose,
  editDeadline,
  prevDeadline,
}) => {
  const [deadline, setDeadline] = useState(prevDeadline);

  useEffect(() => {
    setDeadline(prevDeadline);
  }, [prevDeadline]);

  const handleChangeDeadline = () => {
    editDeadline(setToEndOfDay(deadline));
    onClose();
  };

  const minValue = new Date().toISOString().split("T")[0];

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="bg-white rounded-lg p-8 shadow-lg">
        <h2 className="text-2xl font-semibold mb-4">Edytuj deadline</h2>
        <input
          type="date"
          className="border border-gray-300 p-2 rounded-lg w-full mb-4"
          placeholder="Termin oddania"
          value={deadline}
          min={minValue}
          onChange={(e) => setDeadline(e.target.value)}
        />
        <div className="flex justify-end">
          <button
            className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-lg mr-2"
            onClick={onClose}
          >
            Anuluj
          </button>
          <button
            className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-lg"
            onClick={handleChangeDeadline}
          >
            Zapisz
          </button>
        </div>
      </div>
    </div>
  );
};

export default EditDeadlineDialog;
