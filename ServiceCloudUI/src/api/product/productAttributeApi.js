import axios from "../axios";

export const createProductAttribute = async (data) => {
    const response = await axios.post(
        "/product-attribute",
        data
    );

    return response.data;
};

export const getProductAttributeById = async (id) => {
    const response = await axios.get(
        `/product-attribute/${id}`
    );

    return response.data;
};

export const getAllProductAttributes = async () => {
    const response = await axios.get(
        "/product-attribute/all"
    );

    return response.data;
};

export const getPagedProductAttributes = async (
    pageNumber = 1,
    pageSize = 10
) => {
    const response = await axios.get(
        "/product-attribute",
        {
            params: {
                pageNumber,
                pageSize
            }
        }
    );

    return response.data;
};

export const updateProductAttribute = async (
    id,
    data
) => {
    const response = await axios.put(
        `/product-attribute/${id}`,
        data
    );

    return response.data;
};