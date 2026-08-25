function CrudToolbar({
    title,
    description,
    searchValue = "",
    onSearch,
    searchPlaceholder = "Search...",
    onAdd,
    addButtonText = "Add New"
}) {
    return (
        <div className="crud-toolbar">

            <div className="crud-toolbar-left">

                {title && (
                    <div className="crud-title-section">

                        <h2>
                            {title}
                        </h2>

                        {description && (
                            <p>
                                {description}
                            </p>
                        )}

                    </div>
                )}

            </div>


            <div className="crud-toolbar-right">

                {onSearch && (
                    <div className="crud-search">

                        <input
                            type="text"
                            value={searchValue}
                            onChange={(event) =>
                                onSearch(
                                    event.target.value
                                )
                            }
                            placeholder={
                                searchPlaceholder
                            }
                        />

                    </div>
                )}


                {onAdd && (
                    <button
                        type="button"
                        className="crud-add-button"
                        onClick={onAdd}
                    >
                        + {addButtonText}
                    </button>
                )}

            </div>

        </div>
    );
}

export default CrudToolbar;