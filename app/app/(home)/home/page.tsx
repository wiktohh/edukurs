import CourseCard from "../../components/CourseCard";

const HomePage = () => {
  const tmp = [
    {
      courseName: "Kurs React",
      teacherName: "Wiktor Rzeźnicki",
      id: "1",
      isEnrolled: true,
    },
    {
      courseName: "Kurs React",
      teacherName: "Wiktor Rzeźnicki",
      id: "4",
      isEnrolled: true,
    },
  ];

  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Moje kursy</h1>
      </div>
      <div className="flex flex-wrap gap-8 m-12 justify-center">
        {tmp.map((course) => (
          <CourseCard key={course.id} {...course} />
        ))}
      </div>
    </div>
  );
};

export default HomePage;
