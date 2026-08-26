import axios from "../axios";

// ============================================================
// ENDPOINT CONFIGURATION
// ============================================================
const PRODUCT_VARIANT_PACKAGING_URL = "/product-variant-packagings";

// ============================================================
// CREATE
// ============================================================
export const createProductVariantPackaging = async (data) => {
    const response = await axios.post(
        PRODUCT_VARIANT_PACKAGING_URL,
        data
    );

    return response.data;
};

// ============================================================
// GET BY ID
// ============================================================
export const getProductVariantPackagingById = async (id) => {
    const response = await axios.get(
        `${PRODUCT_VARIANT_PACKAGING_URL}/${id}`
    );

    return response.data;
};

// ============================================================
// GET PAGED
// ============================================================
export const getPagedProductVariantPackagings = async (
    pageNumber = 1,
    pageSize = 10
) => {
    const response = await axios.get(
        PRODUCT_VARIANT_PACKAGING_URL,
        {
            params: {
                pageNumber,
                pageSize
            }
        }
    );

    return response.data;
};

// ============================================================
// UPDATE
// ============================================================
export const updateProductVariantPackaging = async (id, data) => {
    const response = await axios.put(
        `${PRODUCT_VARIANT_PACKAGING_URL}/${id}`,
        data
    );

    return response.data;
};
