import api from "../axios";

const SERVICE_CATEGORY_URL = "/service-categories";

export const getServiceCategories = async ({
    pageNumber = 1,
    pageSize = 10,
    search = "",
}) => {
    const response = await api.get(SERVICE_CATEGORY_URL, {
        params: {
            pageNumber,
            pageSize,
            search,
        },
    });

    return response.data;
};

export const getServiceCategoryById = async (serviceCategoryId) => {
    const response = await api.get(
        `${SERVICE_CATEGORY_URL}/${serviceCategoryId}`
    );

    return response.data;
};

export const createServiceCategory = async (data) => {
    const response = await api.post(
        SERVICE_CATEGORY_URL,
        data
    );

    return response.data;
};

export const updateServiceCategory = async (data) => {
    const response = await api.put(
        `${SERVICE_CATEGORY_URL}/${data.serviceCategoryId}`,
        data
    );

    return response.data;
};

export const archiveServiceCategory = async (serviceCategoryId) => {
    const response = await api.delete(
        `${SERVICE_CATEGORY_URL}/${serviceCategoryId}`
    );

    return response.data;
};