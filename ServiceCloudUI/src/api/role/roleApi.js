import api from "../axios";

const ROLE_URL = "/roles";

/**
 * Get paginated roles
 */
export const getRoles = async (pagination) => {
    const response = await api.get(
        ROLE_URL,
        {
            params: pagination
        }
    );

    return response.data;
};


/**
 * Get role by ID
 */
export const getRoleById = async (roleId) => {
    const response = await api.get(
        `${ROLE_URL}/${roleId}`
    );

    return response.data;
};


/**
 * Create role
 */
export const createRole = async (role) => {
    const response = await api.post(
        ROLE_URL,
        role
    );

    return response.data;
};


/**
 * Update role
 */
export const updateRole = async (role) => {
    const response = await api.put(
        `${ROLE_URL}/${role.roleId}`,
        role
    );

    return response.data;
};


/**
 * Archive role
 */
export const archiveRole = async (roleId) => {
    const response = await api.delete(
        `${ROLE_URL}/${roleId}`
    );

    return response.data;
};