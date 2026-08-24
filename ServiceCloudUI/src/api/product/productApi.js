import axios from "../axios";

// ============================================================
// PRODUCT API
// ============================================================

const PRODUCT_URL = "/product";

// ------------------------------------------------------------
// GET PAGED PRODUCTS
// ------------------------------------------------------------

export const getPagedProducts = async (pageNumber = 1, pageSize = 10) => {
    const response = await axios.get(PRODUCT_URL, {
        params: {
            pageNumber,
            pageSize
        }
    });

    return response.data;
};

// ------------------------------------------------------------
// GET ALL PRODUCTS
// ------------------------------------------------------------

export const getAllProducts = async () => {
    const response = await axios.get(`${PRODUCT_URL}/all`);

    return response.data;
};

// ------------------------------------------------------------
// GET PRODUCT BY ID
// ------------------------------------------------------------

export const getProductById = async (productId) => {
    const response = await axios.get(
        `${PRODUCT_URL}/${productId}`
    );

    return response.data;
};

// ------------------------------------------------------------
// CREATE PRODUCT
// ------------------------------------------------------------

export const createProduct = async (productData) => {
    const response = await axios.post(
        PRODUCT_URL,
        productData
    );

    return response.data;
};

// ------------------------------------------------------------
// UPDATE PRODUCT
// ------------------------------------------------------------

export const updateProduct = async (productId, productData) => {
    const response = await axios.put(
        `${PRODUCT_URL}/${productId}`,
        productData
    );

    return response.data;
};

// ------------------------------------------------------------
// ARCHIVE PRODUCT
// ------------------------------------------------------------

export const archiveProduct = async (productId) => {
    const response = await axios.delete(
        `${PRODUCT_URL}/${productId}`
    );

    return response.data;
};