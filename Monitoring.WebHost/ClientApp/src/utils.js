export const fetchGet = (url, options) => {
  options = {
    ...options,
    method: 'GET'
  };

  options.headers = {
    ...options.headers,
    Authorization: `Bearer ${localStorage.accessToken}`
  };

  return fetch(url, options);
};

export const fetchPost = (url, options) => {
  options = {
    ...options,
    method: 'POST'
  };

  options.headers = {
    ...options.headers,
    Authorization: `Bearer ${localStorage.accessToken}`
  };

  return fetch(url, options);
};

export const fetchPut = (url, options) => {
  options = {
    ...options,
    method: 'PUT'
  };

  options.headers = {
    ...options.headers,
    Authorization: `Bearer ${localStorage.accessToken}`
  };

  return fetch(url, options);
};

export const fetchDelete = (url, options) => {
  options = {
    ...options,
    method: 'DELETE'
  };

  options.headers = {
    ...options.headers,
    Authorization: `Bearer ${localStorage.accessToken}`
  };

  return fetch(url, options);
};