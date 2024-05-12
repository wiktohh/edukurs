type InputProps = {
  type: string;
  placeholder: string;
  halfWidth?: boolean;
};

const Input: React.FC<InputProps> = ({ type, placeholder, halfWidth }) => {
  return (
    <input
      className={`${
        halfWidth ? "w-1/2" : "w-100"
      } bg-gray-200 px-3 py-1 rounded-md mb-2 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:opacity-80`}
      type={type}
      placeholder={placeholder}
    />
  );
};

export default Input;
