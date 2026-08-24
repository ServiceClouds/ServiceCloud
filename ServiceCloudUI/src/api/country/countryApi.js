import api from "../axios";

const COUNTRY_URL = "/countries";

/**
 * Get paginated countries
 */
export const getCountries = async (pagination) => {
    const response = await api.get(
        COUNTRY_URL,
        {
            params: pagination
        }
    );

    return response.data;
};

/**
 * Get country by ID
 */
export const getCountryById = async (countryId) => {
    const response = await api.get(
        `${COUNTRY_URL}/${countryId}`
    );

    return response.data;
};

/**
 * Create country
 */
export const createCountry = async (country) => {
    const response = await api.post(
        COUNTRY_URL,
        country
    );

    return response.data;
};

/**
 * Update country
 */
export const updateCountry = async (
    countryId,
    country
) => {
    const response = await api.put(
        `${COUNTRY_URL}/${countryId}`,
        {
            CountryId: countryId,
            ...country
        }
    );

    return response.data;
};

/**
 * Archive country
 */
export const archiveCountry = async (countryId) => {
    const response = await api.delete(
        `${COUNTRY_URL}/${countryId}`
    );

    return response.data;
};