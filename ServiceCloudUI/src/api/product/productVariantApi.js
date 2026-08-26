import axios from "../axios";

// ============================================================
// ENDPOINT CONFIGURATION
// ============================================================
const PRODUCT_VARIANT_URL = "/product-variant";

// ============================================================
// CREATE
// ============================================================
export const createProductVariant = async (data) => {
    const response = await axios.post(
        PRODUCT_VARIANT_URL,
        data
    );

    return response.data;
};

// ============================================================
// GET BY ID
// ============================================================
export const getProductVariantById = async (id) => {
    const response = await axios.get(
        `${PRODUCT_VARIANT_URL}/${id}`
    );

    return response.data;
};

// ============================================================
// GET ALL
// ============================================================
export const getAllProductVariants = async () => {
    const response = await axios.get(
        `${PRODUCT_VARIANT_URL}/all`
    );

    return response.data;
};

// ============================================================
// GET PAGED
// ============================================================
export const getPagedProductVariants = async (
    pageNumber = 1,
    pageSize = 10,
    search = ""
) => {
    const response = await axios.get(
        PRODUCT_VARIANT_URL,
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

// ============================================================
// UPDATE
// ============================================================
export const updateProductVariant = async (id, data) => {
    const response = await axios.put(
        `${PRODUCT_VARIANT_URL}/${id}`,
        data
    );

    return response.data;
};

// ============================================================
// ARCHIVE
// ============================================================
export const archiveProductVariant = async (id) => {
    const response = await axios.delete(
        `${PRODUCT_VARIANT_URL}/${id}`
    );

    return response.data;
};
