export const displayDate = (date: string) => {
  return new Date(date).toLocaleString().split(",")[0];
};
