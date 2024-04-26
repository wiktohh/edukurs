export const validateRequired = (value: string, field: string) => {
  return !!value || `${field} is required`;
};

export const validateEmail = (value: string) => {
  const emailRegex = /.+@.+\..+/;
  return emailRegex.test(value) || "E-mail must be valid";
};

export const validatePassword = (value: string) => {
  return (
    (value && value.length >= 8) || "Password must be at least 8 characters"
  );
};
