import api from "../axios";

const CURRENCY_URL = "/currencies";

/**
 * Get paginated currencies
 */
export const getCurrencies = async (pagination) => {
    const response = await api.get(
        CURRENCY_URL,
        {
            params: pagination
        }
    );

    return response.data;
};

/**
 * Get currency by ID
 */
export const getCurrencyById = async (currencyId) => {
    const response = await api.get(
        `${CURRENCY_URL}/${currencyId}`
    );

    return response.data;
};

/**
 * Create currency
 */
export const createCurrency = async (currency) => {
    const response = await api.post(
        CURRENCY_URL,
        currency
    );

    return response.data;
};

/**
 * Update currency
 */
export const updateCurrency = async (currency) => {
    const response = await api.put(
        `${CURRENCY_URL}/${currency.currencyId}`,
        currency
    );

    return response.data;
};

/**
 * Archive currency
 */
export const archiveCurrency = async (currencyId) => {
    const response = await api.delete(
        `${CURRENCY_URL}/${currencyId}`
    );

    return response.data;
};