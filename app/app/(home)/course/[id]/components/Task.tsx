import { useAxios } from "@/hooks/use-axios";
import { convertDate } from "@/utils/convert-date";
import { ClientPageRoot } from "next/dist/client/components/client-page";
import { usePathname, useRouter } from "next/navigation";
import { useEffect } from "react";

type TaskElementProps = {
  task: {
    id: string;
    title: string;
    deadline: string;
  };
};

const TaskElement: React.FC<TaskElementProps> = ({ task }) => {
  const colorOfDeadline =
    new Date(task.deadline) < new Date() ? "text-red-500" : "text-green-500";

  const router = useRouter();
  const pathname = usePathname();

  const redirectToTask = () => {
    router.push(`${pathname}/${task.id}`);
  };

  return (
    <div className="w-2/5 border-2 border-gray-400 rounded-xl px-4 py-2">
      <div className="flex justify-between">
        <h1>{task.title}</h1>
        <p className={`${colorOfDeadline}`}>{convertDate(task.deadline)}</p>
      </div>
      <div className="flex justify-end">
        <button
          onClick={redirectToTask}
          className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600 mt-4"
        >
          Przejdź do zadania
        </button>
      </div>
    </div>
  );
};

export default TaskElement;
