import api from "./axios";

export const getCompanies = async (email) => {

    const response = await api.post(API_ENDPOINTS.AUTH.GET_COMPANIES, {
        email
    });

    return response.data;
};