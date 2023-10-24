using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace GodotAssetLibrary.Commands
{
    public class CreateSelectListItems<T> : IRequest<IEnumerable<SelectListItem>>
    {
        public IEnumerable<T> Items { get; set; }

        public Expression<Func<T, object>> LabelExpression { get; set; }

        public Expression<Func<T, object>> ValueExpression { get; set; }

        public Expression<Func<T, object>> GroupExpression { get; set; }

    }

    public class CreateSelectListItemsHandler<T> : IRequestHandler<CreateSelectListItems<T>, IEnumerable<SelectListItem>>
    {
        public Task<IEnumerable<SelectListItem>> Handle(CreateSelectListItems<T> request, CancellationToken cancellationToken)
        {
            var labelExpr = request.LabelExpression.Compile();
            var valueExpr = request.ValueExpression.Compile();

            if (request.GroupExpression != null)
            {
                var groupExpr = request.GroupExpression.Compile();
                var grouped = request.Items.GroupBy(x => groupExpr(x));

                IDictionary<object, SelectListGroup> groupElementMap = new Dictionary<object, SelectListGroup>();
                foreach (var group in grouped)
                {
                    groupElementMap[group.Key] = new SelectListGroup()
                    {
                        Name = group.Key.ToString()
                    };
                }

                return Task.FromResult(grouped.SelectMany(g => g.Select(x => new SelectListItem(labelExpr(x).ToString(), valueExpr(x).ToString())
                {
                    Group = groupElementMap[g.Key],
                })));
            }
            else
            {
                return Task.FromResult(request.Items.Select(x => new SelectListItem(labelExpr(x).ToString(), valueExpr(x).ToString())));
            }
        }
    }
}
