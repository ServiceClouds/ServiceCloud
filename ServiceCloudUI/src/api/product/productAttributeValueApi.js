import axios from "../axios";

// ============================================================
// PRODUCT ATTRIBUTE VALUE API
// ============================================================

const PRODUCT_ATTRIBUTE_VALUE_URL =
    "/product-attribute-value";

// ------------------------------------------------------------
// GET PAGED PRODUCT ATTRIBUTE VALUES
// ------------------------------------------------------------

export const getPagedProductAttributeValues = async (
    pageNumber = 1,
    pageSize = 10
) => {
    const response = await axios.get(
        PRODUCT_ATTRIBUTE_VALUE_URL,
        {
            params: {
                pageNumber,
                pageSize
            }
        }
    );

    return response.data;
};

// ------------------------------------------------------------
// GET ALL PRODUCT ATTRIBUTE VALUES
// ------------------------------------------------------------

export const getAllProductAttributeValues = async () => {
    const response = await axios.get(
        `${PRODUCT_ATTRIBUTE_VALUE_URL}/all`
    );

    return response.data;
};

// ------------------------------------------------------------
// GET PRODUCT ATTRIBUTE VALUE BY ID
// ------------------------------------------------------------

export const getProductAttributeValueById = async (id) => {
    const response = await axios.get(
        `${PRODUCT_ATTRIBUTE_VALUE_URL}/${id}`
    );

    return response.data;
};

// ------------------------------------------------------------
// CREATE PRODUCT ATTRIBUTE VALUE
// ------------------------------------------------------------

export const createProductAttributeValue = async (data) => {
    const response = await axios.post(
        PRODUCT_ATTRIBUTE_VALUE_URL,
        data
    );

    return response.data;
};

// ------------------------------------------------------------
// UPDATE PRODUCT ATTRIBUTE VALUE
// ------------------------------------------------------------

export const updateProductAttributeValue = async (
    id,
    data
) => {
    const response = await axios.put(
        `${PRODUCT_ATTRIBUTE_VALUE_URL}/${id}`,
        data
    );

    return response.data;
};