import axios from "../axios";

// ============================================================
// PRODUCT BRANCH PERMISSION API
// ============================================================

const PRODUCT_BRANCH_PERMISSION_URL =
    "/product-branch-permission";

// ------------------------------------------------------------
// GET PAGED PRODUCT BRANCH PERMISSIONS
// ------------------------------------------------------------

export const getPagedProductBranchPermissions = async (
    pageNumber = 1,
    pageSize = 10
) => {
    const response = await axios.get(
        PRODUCT_BRANCH_PERMISSION_URL,
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
// GET ALL PRODUCT BRANCH PERMISSIONS
// ------------------------------------------------------------

export const getAllProductBranchPermissions = async () => {
    const response = await axios.get(
        `${PRODUCT_BRANCH_PERMISSION_URL}/all`
    );

    return response.data;
};

// ------------------------------------------------------------
// GET PRODUCT BRANCH PERMISSION BY ID
// ------------------------------------------------------------

export const getProductBranchPermissionById = async (id) => {
    const response = await axios.get(
        `${PRODUCT_BRANCH_PERMISSION_URL}/${id}`
    );

    return response.data;
};

// ------------------------------------------------------------
// CREATE PRODUCT BRANCH PERMISSION
// ------------------------------------------------------------

export const createProductBranchPermission = async (data) => {
    const response = await axios.post(
        PRODUCT_BRANCH_PERMISSION_URL,
        data
    );

    return response.data;
};

// ------------------------------------------------------------
// UPDATE PRODUCT BRANCH PERMISSION
// ------------------------------------------------------------

export const updateProductBranchPermission = async (
    id,
    data
) => {
    const response = await axios.put(
        `${PRODUCT_BRANCH_PERMISSION_URL}/${id}`,
        data
    );

    return response.data;
};

// ------------------------------------------------------------
// DEACTIVATE PRODUCT BRANCH PERMISSION
// ------------------------------------------------------------

export const deactivateProductBranchPermission = async (id) => {
    const response = await axios.delete(
        `${PRODUCT_BRANCH_PERMISSION_URL}/${id}`
    );

    return response.data;
};