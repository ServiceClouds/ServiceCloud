import axios from "../axios";

export const createProductCategory = async (data) => {
    const response = await axios.post(
        "/product-categories",
        data
    );

    return response.data;
};

export const getProductCategoryById = async (id) => {
    const response = await axios.get(
        `/product-categories/${id}`
    );

    return response.data;
};

export const getAllProductCategories = async () => {
    const response = await axios.get(
        "/product-categories"
    );

    return response.data;
};

export const getPagedProductCategories = async (
    pageNumber = 1,
    pageSize = 10,
    search = ""
) => {
    const response = await axios.get(
        "/product-categories/paged",
        {
            params: {
                pageNumber,
                pageSize,
                search
            }
        }
    );

    return response.data;
};

export const updateProductCategory = async (
    id,
    data
) => {
    const response = await axios.put(
        `/product-categories/${id}`,
        data
    );

    return response.data;
};

export const archiveProductCategory = async (id) => {
    const response = await axios.delete(
        `/product-categories/${id}`
    );

    return response.data;
};