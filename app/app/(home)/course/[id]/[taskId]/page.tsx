"use client";
import { useParams, usePathname, useRouter } from "next/navigation";
import FileUpload from "./FileUpload";

const TaskPage = () => {
  const { id } = useParams<{ id: string }>();
  const pathname = usePathname();
  const router = useRouter();

  const tmp = [
    {
      title: "XD",
      deadline: "2024-06-15T16:48:53.374Z",
    },
    {
      title: "Drugie zadanie",
      deadline: "2024-12-15T16:48:53.374Z",
    },
  ];

  const backToPreviousPage = () => {
    router.push(pathname.split("/").slice(0, -1).join("/"));
  };

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="flex justify-between shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Nazwa zadania</h1>
        <button
          onClick={backToPreviousPage}
          className="text-gray-500 hover:text-gray-700 px-4 uppercase font-medium"
        >
          Cofnij
        </button>
      </div>
      <div className="w-2/5 m-auto flex flex-col items-center justify-center">
        <div className="w-full py-8 flex justify-between">
          <p className="font-bold">Nazwa zadania</p>
          <p>Deadline</p>
        </div>
        <div>
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Natus
          fugiat, labore eos, at nostrum quaerat voluptate a quod earum cum non
          sequi corporis optio dolore, esse eum? Molestias autem omnis libero
          soluta, nisi aperiam officia doloremque, unde commodi dicta deleniti
          mollitia repudiandae, ipsa hic corporis!
        </div>
        <FileUpload />
      </div>
    </div>
  );
};

export default TaskPage;
