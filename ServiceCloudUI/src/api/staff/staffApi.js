import api from "../axios";

const STAFF_URL = "/staff";

/**
 * Get paginated staff
 */
export const getStaff = async (pagination) => {
    const response = await api.get(
        STAFF_URL,
        {
            params: pagination
        }
    );

    return response.data;
};


/**
 * Get staff by ID
 */
export const getStaffById = async (staffId) => {
    const response = await api.get(
        `${STAFF_URL}/${staffId}`
    );

    return response.data;
};


/**
 * Create staff
 */
export const createStaff = async (staff) => {
    const response = await api.post(
        STAFF_URL,
        staff
    );

    return response.data;
};


/**
 * Update staff
 */
export const updateStaff = async (
    staffId,
    staff
) => {

    const response = await api.put(
        `${STAFF_URL}/${staffId}`,
        {
            StaffId: staffId,
            ...staff
        }
    );

    return response.data;
};


/**
 * Archive staff
 */
export const archiveStaff = async (staffId) => {

    const response = await api.delete(
        `${STAFF_URL}/${staffId}`
    );

    return response.data;
};