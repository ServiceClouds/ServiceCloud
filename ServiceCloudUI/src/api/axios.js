import axios from "axios";
import APP_SETTINGS from "../config/apiSettings";

const api = axios.create({
    baseURL: APP_SETTINGS.API_BASE_URL,
});

export default api;