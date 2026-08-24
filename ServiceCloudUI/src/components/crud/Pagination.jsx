function Pagination({
    currentPage = 1,
    totalPages = 1,
    onPageChange,
    disabled = false
}) {
    if (totalPages <= 1) {
        return null;
    }

    const handlePrevious = () => {

        if (
            currentPage > 1 &&
            !disabled
        ) {
            onPageChange(
                currentPage - 1
            );
        }

    };


    const handleNext = () => {

        if (
            currentPage < totalPages &&
            !disabled
        ) {
            onPageChange(
                currentPage + 1
            );
        }

    };


    const pages = [];

    for (
        let page = 1;
        page <= totalPages;
        page++
    ) {
        pages.push(page);
    }


    return (
        <div className="crud-pagination">

            <button
                type="button"
                onClick={handlePrevious}
                disabled={
                    disabled ||
                    currentPage === 1
                }
            >
                Previous
            </button>


            <div className="pagination-pages">

                {pages.map((page) => (

                    <button
                        key={page}
                        type="button"
                        className={
                            page === currentPage
                                ? "active"
                                : ""
                        }
                        onClick={() =>
                            onPageChange(page)
                        }
                        disabled={disabled}
                    >
                        {page}
                    </button>

                ))}

            </div>


            <button
                type="button"
                onClick={handleNext}
                disabled={
                    disabled ||
                    currentPage === totalPages
                }
            >
                Next
            </button>

        </div>
    );
}

export default Pagination;