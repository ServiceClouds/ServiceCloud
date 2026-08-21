import axios from "axios";

import APP_SETTINGS from "../config/apiSettings";
import { authStorage } from "../services/authStorage";

const api = axios.create({

    baseURL: APP_SETTINGS.API_BASE_URL,

});

api.interceptors.request.use(
    (config) => {

        const accessToken = authStorage.getAccessToken();

        if (accessToken) {

            config.headers.Authorization =
                `Bearer ${accessToken}`;

        }

        return config;

    },
    (error) => {

        return Promise.reject(error);

    }
);

export default api;