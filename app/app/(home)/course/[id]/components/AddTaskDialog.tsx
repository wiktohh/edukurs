"use client";
import React, { useState } from "react";

type AddTaskDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onAddTask: (
    taskName: string,
    taskDescription: string,
    taskDate: string
  ) => void;
};

const setToEndOfDay = (date: string) => {
  const taskDate = new Date(date);
  taskDate.setHours(23, 59, 0, 0);
  return taskDate.toISOString();
};

const AddTaskDialog: React.FC<AddTaskDialogProps> = ({
  isOpen,
  onClose,
  onAddTask,
}) => {
  const [taskName, setTaskName] = useState("");
  const [taskDescription, setTaskDescription] = useState("");
  const [taskDate, setTaskDate] = useState("");

  const handleAddTask = () => {
    const timestamp = setToEndOfDay(taskDate);
    onAddTask(taskName, taskDescription, timestamp);
    setTaskName("");
    setTaskDescription("");
    setTaskDate("");
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="bg-white rounded-lg p-8 shadow-lg">
        <h2 className="text-2xl font-semibold mb-4">Dodaj zadanie</h2>
        <input
          type="text"
          className="border border-gray-300 p-2 rounded-lg w-full mb-4"
          placeholder="Nazwa zadania"
          value={taskName}
          onChange={(e) => setTaskName(e.target.value)}
        />
        <input
          type="text"
          className="border border-gray-300 p-2 rounded-lg w-full mb-4"
          placeholder="Opis zadania"
          value={taskDescription}
          onChange={(e) => setTaskDescription(e.target.value)}
        />
        <input
          type="date"
          className="border border-gray-300 p-2 rounded-lg w-full mb-4"
          placeholder="Termin oddania"
          value={taskDate}
          onChange={(e) => setTaskDate(e.target.value)}
        />
        <div className="flex justify-end">
          <button
            className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-lg mr-2"
            onClick={onClose}
          >
            Anuluj
          </button>
          <button
            disabled={
              setTaskName.length === 0 ||
              setTaskDescription.length === 0 ||
              !setTaskDate
            }
            className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-lg disabled:bg-gray-300 disabled:cursor-not-allowed"
            onClick={handleAddTask}
          >
            Dodaj
          </button>
        </div>
      </div>
    </div>
  );
};

export default AddTaskDialog;
