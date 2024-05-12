const CourseCard = () => {
  return (
    <div className="w-[30%]">
      <div className="bg-white shadow-md p-4">
        <h2 className="text-xl font-semibold">Kurs React</h2>
        <p className="text-sm text-gray-500">Nauczyciel: Wiktor Rzeźnicki</p>
        <button className="bg-blue-500 text-white px-2 py-1 rounded-md mt-2">
          Zobacz
        </button>
      </div>
    </div>
  );
};

export default CourseCard;
