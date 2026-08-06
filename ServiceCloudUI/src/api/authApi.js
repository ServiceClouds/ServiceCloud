import API_ENDPOINTS from "../constants/apiEndpoints";
import api from "./axios";

export const getCompanies = async (email) => {
    console.log("Inside getCompanies");

    const response = await api.post(API_ENDPOINTS.AUTH.GET_COMPANIES, {
        email
    });

    return response.data;
};