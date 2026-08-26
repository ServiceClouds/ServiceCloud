import axios from "../axios";

const SERVICE_CATEGORY_BRANCH_URL =
    "/service-category-branches";

export const createServiceCategoryBranch = async (data) => {
    const response = await axios.post(
        SERVICE_CATEGORY_BRANCH_URL,
        data
    );

    return response.data;
};

export const getServiceCategoryBranchById = async (id) => {
    const response = await axios.get(
        `${SERVICE_CATEGORY_BRANCH_URL}/${id}`
    );

    return response.data;
};

export const getAllServiceCategoryBranches = async () => {
    const response = await axios.get(
        SERVICE_CATEGORY_BRANCH_URL
    );

    return response.data;
};

export const updateServiceCategoryBranch = async (
    id,
    data
) => {
    const response = await axios.put(
        `${SERVICE_CATEGORY_BRANCH_URL}/${id}`,
        data
    );

    return response.data;
};

export const deleteServiceCategoryBranch = async (id) => {
    const response = await axios.delete(
        `${SERVICE_CATEGORY_BRANCH_URL}/${id}`
    );

    return response.data;
};