import API_ENDPOINTS from "../constants/apiEndpoints";
import api from "./axios";

export const getCompanies = async (email) => {

    const response = await api.post(
        API_ENDPOINTS.AUTH.GET_COMPANIES,
        {
            email
        }
    );

    return response.data.messageData;
};

export const verifyLogin = async (request) => {

    const response = await api.post(
        API_ENDPOINTS.AUTH.VERIFY_LOGIN,
        request
    );

    return response.data.messageData;
};

export const login = async (request) => {

    const response = await api.post(
        API_ENDPOINTS.AUTH.LOGIN,
        request
    );

    return response.data.messageData;
};

export const refreshToken = async (refreshToken) => {

    const response = await api.post(
        API_ENDPOINTS.AUTH.REFRESH_TOKEN,
        {
            refreshToken
        }
    );

    return response.data.messageData;
};