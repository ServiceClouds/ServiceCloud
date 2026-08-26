import api from "../axios";

const SERVICE_URL = "/services";

/**
 * Get paginated services
 *
 * Backend:
 * GET /api/services?pageNumber=1&pageSize=10&search=
 */
export const getServices = async ({
    pageNumber = 1,
    pageSize = 10,
    search = ""
} = {}) => {
    const response = await api.get(SERVICE_URL, {
        params: {
            pageNumber,
            pageSize,
            search
        }
    });

    return response.data;
};

/**
 * Get service by ID
 *
 * Backend:
 * GET /api/services/{id}
 */
export const getServiceById = async (serviceId) => {
    const response = await api.get(
        `${SERVICE_URL}/${serviceId}`
    );

    return response.data;
};

/**
 * Create service
 *
 * Backend:
 * POST /api/services
 */
export const createService = async (service) => {
    const response = await api.post(
        SERVICE_URL,
        {
            serviceCategoryId: Number(
                service.serviceCategoryId
            ),

            serviceName: service.serviceName,

            description:
                service.description || null,

            specialInstruction:
                service.specialInstruction || null,

            hasBranchPermission:
                Boolean(service.hasBranchPermission),

            allowBranchEditPrice:
                Boolean(service.allowBranchEditPrice),

            appSourceTypeId: Number(
                service.appSourceTypeId
            )
        }
    );

    return response.data;
};

/**
 * Update service
 *
 * Backend:
 * PUT /api/services/{id}
 */
export const updateService = async (service) => {
    const response = await api.put(
        `${SERVICE_URL}/${service.serviceId}`,
        {
            serviceId: Number(service.serviceId),

            serviceCategoryId: Number(
                service.serviceCategoryId
            ),

            serviceName: service.serviceName,

            description:
                service.description || null,

            specialInstruction:
                service.specialInstruction || null,

            hasBranchPermission:
                Boolean(service.hasBranchPermission),

            allowBranchEditPrice:
                Boolean(service.allowBranchEditPrice)
        }
    );

    return response.data;
};

/**
 * Archive service
 *
 * Backend:
 * DELETE /api/services/{id}
 *
 * This does NOT physically delete the record.
 * Backend calls Service.Archive().
 */
export const archiveService = async (serviceId) => {
    const response = await api.delete(
        `${SERVICE_URL}/${serviceId}`
    );

    return response.data;
};