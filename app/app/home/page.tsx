import CourseCard from "../components/CourseCard";

const HomePage = () => {
  return (
    <div className="bg-gray-100">
      <div className="shadow-md bg-white">
        <h1 className="font-semibold text-3xl py-4 px-2">Moje kursy</h1>
      </div>
      <div className="flex flex-wrap gap-8 m-12 justify-between">
        <CourseCard />
        <CourseCard />
        <CourseCard />
        <CourseCard />
        <CourseCard />
        <CourseCard />
        <CourseCard />
      </div>
    </div>
  );
};

export default HomePage;
