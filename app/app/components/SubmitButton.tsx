type SubmitButtonProps = {
  content: string;
};

const SubmitButton: React.FC<SubmitButtonProps> = ({ content }) => {
  return (
    <button
      type="submit"
      className="bg-blue-500 text-white px-2 py-1 rounded-xl hover:bg-blue-600"
    >
      {content}
    </button>
  );
};

export default SubmitButton;
