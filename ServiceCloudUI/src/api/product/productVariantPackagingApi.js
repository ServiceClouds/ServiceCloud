import axios from "../axios";

export const createProductVariantPackaging = async (data) => {
    const response = await axios.post(
        "/product-variant-packagings",
        data
    );

    return response.data;
};

export const getProductVariantPackagingById = async (id) => {
    const response = await axios.get(
        `/product-variant-packagings/${id}`
    );

    return response.data;
};

export const getPagedProductVariantPackagings = async (
    pageNumber = 1,
    pageSize = 10
) => {
    const response = await axios.get(
        "/product-variant-packagings",
        {
            params: {
                pageNumber,
                pageSize
            }
        }
    );

    return response.data;
};

export const updateProductVariantPackaging = async (
    id,
    data
) => {
    const response = await axios.put(
        `/product-variant-packagings/${id}`,
        data
    );

    return response.data;
};