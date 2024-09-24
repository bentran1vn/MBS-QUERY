using MBS_QUERY.Contract.Attributes;
using MBS_QUERY.Domain.Abstractions.Entities;
using MBS_QUERY.Domain.Constrants;

namespace MBS_QUERY.Domain.Documents;

[BsonCollection(TableNames.Product)]
public class ProductProjection : Document
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}