import axios from "../axios";

// ============================================================
// CREATE
// ============================================================

export const createProductVariantBranch = async (data) => {
    const response = await axios.post(
        "/product-variant-branch",
        data
    );

    return response.data;
};


// ============================================================
// GET BY ID
// ============================================================

export const getProductVariantBranchById = async (id) => {
    const response = await axios.get(
        `/product-variant-branch/${id}`
    );

    return response.data;
};


// ============================================================
// GET ALL
// ============================================================

export const getAllProductVariantBranches = async () => {
    const response = await axios.get(
        "/product-variant-branch/all"
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
        "/product-variant-branch",
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

export const updateProductVariantBranch = async (
    id,
    data
) => {
    const response = await axios.put(
        `/product-variant-branch/${id}`,
        data
    );

    return response.data;
};


// ============================================================
// ARCHIVE
// ============================================================

export const archiveProductVariantBranch = async (id) => {
    const response = await axios.delete(
        `/product-variant-branch/${id}`
    );

    return response.data;
};