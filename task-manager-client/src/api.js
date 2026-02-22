// src/api.js
import axios from 'axios';

// Ensure this matches the PORT your .NET API is running on (check launchSettings.json)
const API_URL = 'http://localhost:5048/api/tasks';

const api = axios.create({
    baseURL: API_URL,
});

export const getTasks = () => api.get('/');
export const getTaskById = (id) => api.get(`/${id}`);
export const createTask = (task) => api.post('/', task);
export const updateTask = (id, task) => api.put(`/${id}`, task);
export const deleteTask = (id) => api.delete(`/${id}`);

export default api;