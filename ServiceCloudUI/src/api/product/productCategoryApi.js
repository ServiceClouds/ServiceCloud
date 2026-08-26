import axios from "../axios";

// ============================================================
// PRODUCT CATEGORY API
// ============================================================

const PRODUCT_CATEGORY_URL = "/product-categories";

// ------------------------------------------------------------
// GET PAGED PRODUCT CATEGORIES
// ------------------------------------------------------------

export const getPagedProductCategories = async (
    pageNumber = 1,
    pageSize = 10,
    search = ""
) => {
    const response = await axios.get(
        `${PRODUCT_CATEGORY_URL}/paged`,
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

// ------------------------------------------------------------
// GET ALL PRODUCT CATEGORIES
// ------------------------------------------------------------

export const getAllProductCategories = async () => {
    const response = await axios.get(
        PRODUCT_CATEGORY_URL
    );

    return response.data;
};

// ------------------------------------------------------------
// GET PRODUCT CATEGORY BY ID
// ------------------------------------------------------------

export const getProductCategoryById = async (id) => {
    const response = await axios.get(
        `${PRODUCT_CATEGORY_URL}/${id}`
    );

    return response.data;
};

// ------------------------------------------------------------
// CREATE PRODUCT CATEGORY
// ------------------------------------------------------------

export const createProductCategory = async (data) => {
    const response = await axios.post(
        PRODUCT_CATEGORY_URL,
        data
    );

    return response.data;
};

// ------------------------------------------------------------
// UPDATE PRODUCT CATEGORY
// ------------------------------------------------------------

export const updateProductCategory = async (id, data) => {
    const response = await axios.put(
        `${PRODUCT_CATEGORY_URL}/${id}`,
        data
    );

    return response.data;
};

// ------------------------------------------------------------
// ARCHIVE PRODUCT CATEGORY
// ------------------------------------------------------------

export const archiveProductCategory = async (id) => {
    const response = await axios.delete(
        `${PRODUCT_CATEGORY_URL}/${id}`
    );

    return response.data;
};