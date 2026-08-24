function DataTable({
    columns = [],
    data = [],
    loading = false,
    onView,
    onEdit,
    onArchive,
    getRowKey,
    emptyMessage = "No records found."
}) {
    return (
        <div className="crud-table-wrapper">

            <table className="crud-table">

                <thead>
                    <tr>

                        {columns.map((column) => (
                            <th key={column.key}>
                                {column.label}
                            </th>
                        ))}

                        {(onView || onEdit || onArchive) && (
                            <th className="actions-column">
                                Actions
                            </th>
                        )}

                    </tr>
                </thead>

                <tbody>

                    {loading ? (

                        <tr>
                            <td
                                colSpan={
                                    columns.length +
                                    (onView || onEdit || onArchive ? 1 : 0)
                                }
                                className="table-loading"
                            >
                                Loading...
                            </td>
                        </tr>

                    ) : data.length === 0 ? (

                        <tr>
                            <td
                                colSpan={
                                    columns.length +
                                    (onView || onEdit || onArchive ? 1 : 0)
                                }
                                className="table-empty"
                            >
                                {emptyMessage}
                            </td>
                        </tr>

                    ) : (

                        data.map((row, index) => (

                            <tr
                                key={
                                    getRowKey
                                        ? getRowKey(row)
                                        : index
                                }
                            >

                                {columns.map((column) => (

                                    <td key={column.key}>

                                        {column.render
                                            ? column.render(
                                                row
                                            )
                                            : row[column.key]
                                        }

                                    </td>

                                ))}

                                {(onView || onEdit || onArchive) && (

                                    <td className="table-actions">

                                        {onView && (
                                            <button
                                                type="button"
                                                className="table-action view"
                                                onClick={() =>
                                                    onView(row)
                                                }
                                            >
                                                View
                                            </button>
                                        )}

                                        {onEdit && (
                                            <button
                                                type="button"
                                                className="table-action edit"
                                                onClick={() =>
                                                    onEdit(row)
                                                }
                                            >
                                                Edit
                                            </button>
                                        )}

                                        {onArchive && (
                                            <button
                                                type="button"
                                                className="table-action archive"
                                                onClick={() =>
                                                    onArchive(row)
                                                }
                                            >
                                                Archive
                                            </button>
                                        )}

                                    </td>

                                )}

                            </tr>

                        ))

                    )}

                </tbody>

            </table>

        </div>
    );
}

export default DataTable;