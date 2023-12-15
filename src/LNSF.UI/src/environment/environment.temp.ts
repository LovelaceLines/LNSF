export const apiUrl: string = (() => {
  const apiURL = import.meta.env.VITE_REACT_APP_URL_BASE;

  if (!apiURL) throw new Error('API URL not defined');

  return apiURL;
})();