"use client";
import React from "react";

type ConfirmRemoveUserDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onRemove: () => void;
};

const ConfirmRemoveUserDialog: React.FC<ConfirmRemoveUserDialogProps> = ({
  isOpen,
  onClose,
  onRemove,
}) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="bg-white rounded-lg p-8 shadow-lg">
        <h2 className="text-2xl font-semibold mb-4">
          Czy na pewno chcesz usunąć konto?
        </h2>
        <div className="flex justify-end">
          <button
            className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 rounded-lg mr-2"
            onClick={onClose}
          >
            Anuluj
          </button>
          <button
            className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded-lg"
            onClick={onRemove}
          >
            Usuń
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmRemoveUserDialog;
