export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginationResult<T> {
    result: T; // za T moze wpasc cokolwiek
    pagination: Pagination;
}
