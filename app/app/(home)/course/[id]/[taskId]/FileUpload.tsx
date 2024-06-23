import { useAuth } from "@/context/auth-context";
import { useAxios } from "@/hooks/use-axios";
import { usePathname } from "next/navigation";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

type FileUpload = {
  deadline: string;
};

const FileUpload: React.FC<FileUpload> = ({ deadline }) => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [uploadTime, setUploadTime] = useState<Date | null>(null);

  const axios = useAxios();
  const pathname = usePathname();
  const taskId = pathname.split("/").at(-1);
  const { user } = useAuth();

  useEffect(() => {
    if (user) {
      const storedUploadTime = localStorage.getItem(
        `uploadTime-${taskId}-${user.id}`
      );
      if (storedUploadTime) {
        setUploadTime(new Date(storedUploadTime));
      }
    }
  }, [taskId, user]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files.length > 0) {
      setSelectedFile(event.target.files[0]);
    }
  };

  const handleFileUpload = async () => {
    if (!selectedFile) {
      alert("Dodaj plik!");
      return;
    }

    const formData = new FormData();
    formData.append("file", selectedFile);

    try {
      const response = await axios.post(
        "/api/File/upload/" + pathname.split("/").at(-1),
        formData
      );
      console.log(response);
      setUploadTime(new Date());
      toast.success("Plik został przesłany pomyślnie!");
      if (user) {
        localStorage.setItem(
          `uploadTime-${taskId}-${user.id}`,
          new Date().toString()
        );
      }
    } catch (error) {
      console.error("Error uploading file:", error);
      toast.error("Wystąpił błąd podczas przesyłania pliku");
    }
  };

  return (
    <div className="file-upload">
      {new Date() < new Date(deadline) ? (
        <>
          <input
            type="file"
            onChange={handleFileChange}
            className="mb-4 p-2 border rounded"
          />
          <button
            onClick={handleFileUpload}
            className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded"
          >
            Upload
          </button>
        </>
      ) : (
        <div className="bg-red-300 border-red-600 border-[.1rem] rounded-md p-4 mb-4">
          <p className="text-red-600 text-xl">
            Upłynął termin oddawania plików, nie można już ich przesyłać.
          </p>
        </div>
      )}
      {uploadTime && (
        <div className="bg-green-200 border-green-600 border-[.1rem] rounded-md p-4">
          <p className="text-green-600 text-xl text-semibold">
            Przesłano plik o godzinie {uploadTime.toLocaleTimeString()} dnia{" "}
            {uploadTime.toLocaleDateString()}
          </p>
        </div>
      )}
    </div>
  );
};

export default FileUpload;
