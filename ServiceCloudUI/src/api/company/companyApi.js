import api from "../axios";

const COMPANY_URL = "/company";

/**
 * Get paginated companies
 */
export const getCompanies = async (pagination) => {
    const response = await api.get(COMPANY_URL, {
        params: pagination
    });

    return response.data;
};

/**
 * Get company by ID
 */
export const getCompanyById = async (companyId) => {
    const response = await api.get(
        `${COMPANY_URL}/${companyId}`
    );

    return response.data;
};

/**
 * Create company
 */
export const createCompany = async (company) => {
    const response = await api.post(
        COMPANY_URL,
        company
    );

    return response.data;
};

/**
 * Update company
 */
export const updateCompany = async (company) => {
    const response = await api.put(
        `${COMPANY_URL}/${company.companyId}`,
        company
    );

    return response.data;
};

/**
 * Archive company
 */
export const archiveCompany = async (companyId) => {
    const response = await api.delete(
        `${COMPANY_URL}/${companyId}`
    );

    return response.data;
};