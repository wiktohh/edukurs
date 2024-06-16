import React, { useState } from "react";

const FileUpload: React.FC = () => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [uploadTime, setUploadTime] = useState<Date | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files.length > 0) {
      setSelectedFile(event.target.files[0]);
    }
  };

  const handleFileUpload = () => {
    if (!selectedFile) {
      alert("Please select a file first!");
      return;
    }

    const formData = new FormData();
    formData.append("file", selectedFile);

    // Replace 'your-upload-url' with the actual endpoint you want to send the file to.
    fetch("your-upload-url", {
      method: "POST",
      body: formData,
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Success:", data);
        setUploadTime(new Date());
        alert("File uploaded successfully!");
      })
      .catch((error) => {
        console.error("Error:", error);
        alert("File upload failed!");
      });
  };

  return (
    <div className="file-upload">
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
      {uploadTime && (
        <div className="mt-4">
          <p className="text-green-600">
            File uploaded at {uploadTime.toLocaleTimeString()} on{" "}
            {uploadTime.toLocaleDateString()}
          </p>
        </div>
      )}
    </div>
  );
};

export default FileUpload;
