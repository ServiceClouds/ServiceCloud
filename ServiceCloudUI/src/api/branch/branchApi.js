import api from "../axios";

const BRANCH_URL = "/branch";

/**
 * Get paginated branches
 */
export const getBranches = async (pagination) => {
    const response = await api.get(
        BRANCH_URL,
        {
            params: pagination
        }
    );

    return response.data;
};


/**
 * Get branch by ID
 */
export const getBranchById = async (branchId) => {
    const response = await api.get(
        `${BRANCH_URL}/${branchId}`
    );

    return response.data;
};


/**
 * Create branch
 */
export const createBranch = async (branch) => {
    const response = await api.post(
        BRANCH_URL,
        branch
    );

    return response.data;
};


/**
 * Update branch
 */
export const updateBranch = async (branch) => {
    const response = await api.put(
        `${BRANCH_URL}/${branch.branchId}`,
        branch
    );

    return response.data;
};


/**
 * Archive branch
 */
export const archiveBranch = async (branchId) => {
    const response = await api.delete(
        `${BRANCH_URL}/${branchId}`
    );

    return response.data;
};