import axios from "../axios";

// ============================================================
// ENDPOINT CONFIGURATION
// ============================================================
const PRODUCT_VARIANT_BRANCH_URL = "/product-variant-branch";

// ============================================================
// CREATE
// ============================================================
export const createProductVariantBranch = async (data) => {
    const response = await axios.post(
        PRODUCT_VARIANT_BRANCH_URL,
        data
    );

    return response.data;
};

// ============================================================
// GET BY ID
// ============================================================
export const getProductVariantBranchById = async (id) => {
    const response = await axios.get(
        `${PRODUCT_VARIANT_BRANCH_URL}/${id}`
    );

    return response.data;
};

// ============================================================
// GET ALL
// ============================================================
export const getAllProductVariantBranches = async () => {
    const response = await axios.get(
        `${PRODUCT_VARIANT_BRANCH_URL}/all`
    );

    return response.data;
};

// ============================================================
// GET PAGED
// ============================================================
export const getPagedProductVariantBranches = async (
    pageNumber = 1,
    pageSize = 10,
    search = ""
) => {
    const response = await axios.get(
        PRODUCT_VARIANT_BRANCH_URL,
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
export const updateProductVariantBranch = async (id, data) => {
    const response = await axios.put(
        `${PRODUCT_VARIANT_BRANCH_URL}/${id}`,
        data
    );

    return response.data;
};

// ============================================================
// ARCHIVE
// ============================================================
export const archiveProductVariantBranch = async (id) => {
    const response = await axios.delete(
        `${PRODUCT_VARIANT_BRANCH_URL}/${id}`
    );

    return response.data;
};
