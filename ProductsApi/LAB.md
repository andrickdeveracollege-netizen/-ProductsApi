
# ASP.NET Core Web API Upgrade Lab

## Team Members

- Andrick
- Jacob

## Part 0 — Setup and Baseline

### Invalid product

Request:
POST /api/products

Input:
- Empty product name
- Negative price

Actual status code:
[Record the status code before validation]

Observed behavior:
[Describe what the starter API actually did]

### Missing product

Request:
GET /api/products/9999

Actual status code:
[Record the baseline status code]

Observed behavior:
[Describe the actual result]

## Part 1 — Data Annotations Validation

Implemented:
- Required
- StringLength
- Range
- ApiController automatic validation

### Invalid POST response

[Paste the actual 400 response body here]

## Part 2 — Custom Validation and Domain Exceptions

Custom rule:
SalePrice must be less than Price.

Expected validation message:
SalePrice must be less than Price.

Duplicate SKU rule:
A product SKU must be unique.

Missing product before global exception handling:
[Record the actual observed result]

## Part 3 — Global Exception Handling

Exception mappings:
- NotFoundException -> 404
- ConflictException -> 409
- Unexpected exception -> 500

### Missing product response

[Paste the actual 404 Problem Details body here]

### Server log

[Paste the corresponding actual server log entry here]

### Unexpected exception

[Record the actual 500 response and verify that
the stack trace is not exposed to the client]

## Part 4 — Standard API Responses

### POST

Status: 201 Created

[Paste the actual response and Location header]

### DELETE

Status: 204 No Content

[Record the result]

### Paged GET

[Paste the actual data/meta response]

### Final 404

[Paste the actual 404 response]

## Reflection

[Write 3–5 sentences about how validation,
domain exceptions, and centralized exception
handling work together.]
