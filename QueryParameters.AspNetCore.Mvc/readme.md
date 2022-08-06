# QueryParameters.AspNetCore.Mvc

A package designed to extend the QueryParameters functionality, extending the asp.net core implementation.

# Setup

## ControllerBase
Within a class inheriting `ControllerBase` an extension method will be available named `ApplyQueryParameters`.

This expects an `IQueryable` data source as a parameter to perform the query parameters on.

#### ProductController.cs
``` csharp
private DbContext _DbContext;

[ProducesResponseType(StatusCodes.Status200OK)]
[HttpGet()]
public IActionResult Get()
{
    return Ok(this.ApplyQueryParameters(_DbContext.Products));
}
```

# URL Syntax

```
?filter=(Code starts prod or Code starts 'pro ') and Price lt 1.23&take=10&skip=20&sort=ID desc,Code len,Code asc
```

## Filter
A sort parameter comprises of three required elements: identifier, condition and value. Filters must be separated by an operator.

The query parameter strings can be customised at runtime, by overriding the static variables found within `QueryParameters.AspNetCore.Mvc.Settings.SyntaxSettings`.

'filter' can be customised via the `Filter` setting.

### Filter Conditions
| Name               | Query parameter string | Setting Name             |
| ------------------ | ---------------------- | ------------------------ |
| Equal              | `eq`                   | `FilterEqual`            |
| Not equal          | `neq`                  | `FilterNotEqual`         |
| Less than          | `lt`                   | `FilterLessThan`         |
| Less than equal    | `lte`                  | `FilterLessThanEqual`    |
| Greater than       | `gt`                   | `FilterGreaterThan`      |
| Greater than equal | `gte`                  | `FilterGreaterThanEqual` |
| Starts with        | `starts`               | `FilterStarts`           |
| Ends with          | `ends`                 | `FilterEnds`             |
| Contains           | `contains`             | `FilterContains`         |

### Filter Operators
| Name | Query parameter string | Setting Name |
| ---- | ---------------------- | ------------ |
| And  | `and`                  | `FilterAnd`  |
| Or   | `or`                   | `FilterOr`   |

## Sort
A sort parameter comprises of one required elements: identifier and two optional elements, operator and direction. Sorts must be separated by a comma.

The query parameter strings can be customised at runtime, by overriding the static variables found within `QueryParameters.AspNetCore.Mvc.Settings.SyntaxSettings`.

'sort' can be customised via the `Sort` setting.

### Sort Operators
| Name    | Query parameter string | Setting Name | Default |
| ------- | ---------------------- | ------------ | ------- |
| Default |                        |              | ✔       |
| Length  | `len`                  |              | ✘       |

### Sort Direction
| Name       | Query parameter string | Setting Name     | Default |
| ---------- | ---------------------- | ---------------- | ------- |
| Ascending  | `asc`                  | `SortAscending`  | ✔       |
| Descending | `desc`                 | `SortDescending` | ✘       |

## Pagination
### Take
'take' can be customised via the `Take` setting.

### Skip
'skip' can be customised via the `Skip` setting.