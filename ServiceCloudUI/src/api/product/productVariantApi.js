import axios from "../axios";

// ============================================================
// CREATE
// ============================================================

export const createProductVariant = async (data) => {
    const response = await axios.post(
        "/product-variant",
        data
    );

    return response.data;
};


// ============================================================
// GET BY ID
// ============================================================

export const getProductVariantById = async (id) => {
    const response = await axios.get(
        `/product-variant/${id}`
    );

    return response.data;
};


// ============================================================
// GET ALL
// ============================================================

export const getAllProductVariants = async () => {
    const response = await axios.get(
        "/product-variant/all"
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
        "/product-variant",
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

export const updateProductVariant = async (
    id,
    data
) => {
    const response = await axios.put(
        `/product-variant/${id}`,
        data
    );

    return response.data;
};


// ============================================================
// ARCHIVE
// ============================================================

export const archiveProductVariant = async (id) => {
    const response = await axios.delete(
        `/product-variant/${id}`
    );

    return response.data;
};