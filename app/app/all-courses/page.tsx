import CourseCard from "../components/CourseCard";

const tmp = [
  {
    courseName: "Kurs React",
    teacherName: "Wiktor Rzeźnicki",
    id: "1",
    isEnrolled: true,
  },
  {
    courseName: "Kurs JavaScript",
    teacherName: "Wiktor Rzeźnicki",
    id: "2",
    isEnrolled: false,
  },
  {
    courseName: "Kurs TypeScript",
    teacherName: "Wiktor Rzeźnicki",
    id: "3",
    isEnrolled: false,
  },
  {
    courseName: "Kurs React",
    teacherName: "Wiktor Rzeźnicki",
    id: "4",
    isEnrolled: true,
  },
  {
    courseName: "Kurs JavaScript",
    teacherName: "Wiktor Rzeźnicki",
    id: "5",
    isEnrolled: false,
  },
  {
    courseName: "Kurs TypeScript",
    teacherName: "Wiktor Rzeźnicki",
    id: "6",
    isEnrolled: false,
  },
];

const AllCoursesPage = () => {
  return (
    <div className="bg-gray-100 flex-grow">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Wszystkie kursy</h1>
      </div>
      <div className="flex flex-wrap justify-center gap-8 m-12">
        {tmp.map((course) => (
          <CourseCard key={course.id} {...course} />
        ))}
      </div>
    </div>
  );
};

export default AllCoursesPage;
