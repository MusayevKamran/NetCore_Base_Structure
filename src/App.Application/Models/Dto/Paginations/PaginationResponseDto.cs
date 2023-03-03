namespace App.Application.Models.Dto.Paginations;

/// <summary>
///     Navigation information
/// </summary>
public class PaginationResponseDto
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PaginationResponseDto" /> class.
    /// </summary>
    /// <param name="total">The total number of objects</param>
    /// <param name="pageNumber">Current page number</param>
    /// <param name="pageSize">The number of elements per page</param>
    /// <param name="rowsCount">Number of records on the current page</param>
    public PaginationResponseDto(long total, int pageNumber, int pageSize, int rowsCount)
    {
        if (pageSize == 0) throw new ArgumentException("Cannot be 0", nameof(pageSize));

        Total = total;
        PageNumber = pageNumber;
        PageSize = pageSize;
        RowsCount = rowsCount;
        PageCount = (total + pageSize - 1) / pageSize;
    }

    /// <summary>
    ///     The total number of objects
    /// </summary>
    public long Total { get; set; }

    /// <summary>
    ///     Number of records on the current page
    /// </summary>
    public int RowsCount { get; set; }

    /// <summary>
    ///     Number of pages
    /// </summary>
    public long PageCount { get; set; }

    /// <summary>
    ///     The number of elements per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///     Current page number
    /// </summary>
    public int PageNumber { get; set; }
}