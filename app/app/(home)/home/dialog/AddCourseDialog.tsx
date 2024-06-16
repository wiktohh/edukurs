"use client";
import React, { useState } from "react";

type AddCourseDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onAddCourse?: (courseName: string) => void;
  onEditCourse?: (courseName: string) => void;
  repoName?: string;
};

const AddCourseDialog: React.FC<AddCourseDialogProps> = ({
  isOpen,
  onClose,
  onAddCourse,
  onEditCourse,
  repoName,
}) => {
  const [courseName, setCourseName] = useState(repoName || "");

  const handleAddCourse = () => {
    if (onAddCourse) {
      onAddCourse(courseName);
    }
    if (onEditCourse) {
      onEditCourse(courseName);
    }
    setCourseName("");
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="bg-white rounded-lg p-8 shadow-lg">
        <h2 className="text-2xl font-semibold mb-4">
          {onAddCourse ? "Dodaj kurs" : "Edytuj kurs"}
        </h2>
        <input
          type="text"
          className="border border-gray-300 p-2 rounded-lg w-full mb-4"
          placeholder="Nazwa kursu"
          value={courseName}
          onChange={(e) => setCourseName(e.target.value)}
        />
        <div className="flex justify-end">
          <button
            className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-lg mr-2"
            onClick={onClose}
          >
            Anuluj
          </button>
          <button
            disabled={courseName.length === 0}
            className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-lg disabled:bg-gray-300 disabled:cursor-not-allowed"
            onClick={handleAddCourse}
          >
            Dodaj
          </button>
        </div>
      </div>
    </div>
  );
};

export default AddCourseDialog;
