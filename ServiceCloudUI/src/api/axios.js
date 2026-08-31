import axios from "axios";

import APP_SETTINGS from "../config/apiSettings";
import { authStorage } from "../services/authStorage";

const api = axios.create({

    baseURL: APP_SETTINGS.API_BASE_URL,

});

const refreshApi = axios.create({

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

api.interceptors.response.use(
    (response) => {

        return response;

    },
    async (error) => {

        const originalRequest = error.config;

        if (
            error.response?.status !== 401 ||
            originalRequest?._retry
        ) {

            return Promise.reject(error);

        }

        const refreshToken = authStorage.getRefreshToken();

        if (!refreshToken) {

            authStorage.clear();

            return Promise.reject(error);

        }

        originalRequest._retry = true;

        try {

            const response = await refreshApi.post(
                "/auth/refresh",
                {
                    refreshToken
                }
            );

            const data = response.data.messageData;

            authStorage.setTokens(
                data.accessToken,
                data.refreshToken
            );

            originalRequest.headers.Authorization =
                `Bearer ${data.accessToken}`;

            return api(originalRequest);

        }
        catch (refreshError) {

            authStorage.clear();

            return Promise.reject(refreshError);

        }

    }
);

export default api;