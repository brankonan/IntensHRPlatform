import axios from 'axios'

const api = axios.create({
    baseURL: "https://localhost:44311/api"
});

export default api;