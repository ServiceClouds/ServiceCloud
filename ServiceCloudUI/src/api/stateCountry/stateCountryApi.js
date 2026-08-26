import api from "../axios";

const STATE_COUNTRY_URL = "/state-countries";

/**
 * Get paginated state/countries
 */
export const getStateCountries = async (pagination) => {
    const response = await api.get(
        STATE_COUNTRY_URL,
        {
            params: pagination
        }
    );

    return response.data;
};

/**
 * Get state/country by ID
 */
export const getStateCountryById = async (stateCountryId) => {
    const response = await api.get(
        `${STATE_COUNTRY_URL}/${stateCountryId}`
    );

    return response.data;
};

/**
 * Create state/country
 */
export const createStateCountry = async (stateCountry) => {
    const response = await api.post(
        STATE_COUNTRY_URL,
        stateCountry
    );

    return response.data;
};

/**
 * Update state/country
 */
export const updateStateCountry = async (stateCountry) => {
    const response = await api.put(
        `${STATE_COUNTRY_URL}/${stateCountry.stateCountryId}`,
        stateCountry
    );

    return response.data;
};

/**
 * Archive state/country
 */
export const archiveStateCountry = async (stateCountryId) => {
    const response = await api.delete(
        `${STATE_COUNTRY_URL}/${stateCountryId}`
    );

    return response.data;
};