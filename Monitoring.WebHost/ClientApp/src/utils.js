export const fetchGet = (url, options) => {
  options = {
    ...options,
    method: 'GET'
  };

  options.headers = {
    ...options.headers,
    'Authorization': `Bearer ${localStorage.accessToken}`
  };

  return fetch(url, options);
};

export const fetchPost = (url, params) => {
  let options = {
    method: 'POST',
    body: JSON.stringify(params)
  };

  options.headers = {
    'Authorization': `Bearer ${localStorage.accessToken}`,
    'Content-Type': 'application/json'
  };

  return fetch(url, options);
};

export const fetchPut = (url, params) => {
  let options = {
    method: 'PUT',
    body: JSON.stringify(params)
  };

  options.headers = {
    'Authorization': `Bearer ${localStorage.accessToken}`,
    'Content-Type': 'application/json'
  };

  return fetch(url, options);
};

export const fetchDelete = (url) => {
  let options = {
    method: 'DELETE'
  };

  options.headers = {
    'Authorization': `Bearer ${localStorage.accessToken}`
  };

  return fetch(url, options);
};

export const emptyFn = () => {};