import api from "../axios";

const STAFF_BRANCH_URL = "/staff-branch";

// GET PAGED
export const getStaffBranches = async (pagination) => {
    const response = await api.get(STAFF_BRANCH_URL, {
        params: pagination
    });

    return response.data;
};

// GET BY ID
export const getStaffBranchById = async (staffBranchId) => {
    const response = await api.get(
        `${STAFF_BRANCH_URL}/${staffBranchId}`
    );

    return response.data;
};

// CREATE
export const createStaffBranch = async (staffBranch) => {
    const response = await api.post(
        STAFF_BRANCH_URL,
        staffBranch
    );

    return response.data;
};

// UPDATE
export const updateStaffBranch = async (
    staffBranchId,
    staffBranch
) => {
    const response = await api.put(
        `${STAFF_BRANCH_URL}/${staffBranchId}`,
        {
            staffBranchId,
            ...staffBranch
        }
    );

    return response.data;
};

// ARCHIVE
export const archiveStaffBranch = async (staffBranchId) => {
    const response = await api.delete(
        `${STAFF_BRANCH_URL}/${staffBranchId}`
    );

    return response.data;
};