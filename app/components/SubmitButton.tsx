type SubmitButtonProps = {
  children: React.ReactNode;
};

const SubmitButton: React.FC<SubmitButtonProps> = ({ children }) => {
  return (
    <button
      type="submit"
      className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600"
    >
      {children}
    </button>
  );
};

export default SubmitButton;
