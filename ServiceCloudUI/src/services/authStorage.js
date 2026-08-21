const ACCESS_TOKEN_KEY = "accessToken";

export const authStorage = {
    getAccessToken: () => {
        return localStorage.getItem(ACCESS_TOKEN_KEY);
    },

    setAccessToken: (token) => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token);
    },

    removeAccessToken: () => {
        localStorage.removeItem(ACCESS_TOKEN_KEY);
    },

    clear: () => {
        localStorage.removeItem(ACCESS_TOKEN_KEY);
    }
};