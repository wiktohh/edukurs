type CourseCardProps = {
  courseName: string;
  teacherName: string;
  id: string;
  isEnrolled: boolean;
};

const CourseCard: React.FC<CourseCardProps> = ({
  courseName,
  teacherName,
  id,
  isEnrolled,
}) => {
  return (
    <div className="w-[30%]">
      <div className="bg-white shadow-md p-4">
        <h2 className="text-xl font-semibold">{courseName}</h2>
        <p className="text-sm text-gray-500">Nauczyciel: {teacherName}</p>
        {isEnrolled ? (
          <button className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600 mt-4">
            Przejdź do kursu
          </button>
        ) : (
          <button className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600 mt-4">
            Zapisz się
          </button>
        )}
      </div>
    </div>
  );
};

export default CourseCard;
