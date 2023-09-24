using System.Linq.Expressions;

namespace LogicExtensions;

/// <summary>
/// Expression extensions for the <see cref="string"/> class.
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// Creates a Func to get the property value conditionally (return default if any host is null in the chain of path) by the given path.
    /// </summary>
    /// <param name="propertySelector">The property selector lambda.</param>
    /// <exception cref="ArgumentException">When the PropertySelector not a LambdaExpression.</exception>
    public static Func<T?, TResult?> CreateConditionalGetter<T, TResult>(this Expression<Func<T, TResult>> propertySelector)
    {
        if (propertySelector is not LambdaExpression lambdaExpression)
        {
            throw new ArgumentException($"Unsupported property selector: {propertySelector}", nameof(propertySelector));
        }

        var body = lambdaExpression.Body;
        var newBody = body;
        while (body is MemberExpression or UnaryExpression { Operand: MemberExpression })
        {
            var memberAccess = body as MemberExpression ?? (MemberExpression)((UnaryExpression)body).Operand;

            if (!memberAccess.Expression.Type.IsValueType)
            {
                newBody = Expression.Condition(
                    Expression.Equal(memberAccess.Expression, Expression.Constant(null)),
                    Expression.Default(typeof(TResult)),
                    Expression.Convert(newBody, typeof(TResult)));
            }

            body = memberAccess.Expression;
        }

        return Expression.Lambda<Func<T?, TResult?>>(newBody, propertySelector.Parameters.First()).Compile();
    }

    /// <summary>
    /// Creates a Func to set the property value conditionally by the given path.
    /// </summary>
    /// <param name="propertySelector">The property selector lambda.</param>
    /// <param name="newObjectInPath">
    /// True to create new object with the public parameterless constructor for any 'null' in the chain of property;
    /// False to abort the assignment and does nothing.</param>
    /// <exception cref="ArgumentException">When the PropertySelector not a LambdaExpression.</exception>
    public static Action<THost?, TProperty?> CreateConditionalSetter<THost, TProperty>(
        this Expression<Func<THost, TProperty>> propertySelector, bool newObjectInPath = true)
    {
        if (propertySelector is not LambdaExpression lambdaExpression)
        {
            throw new ArgumentException($"Unsupported property selector: {propertySelector}", nameof(propertySelector));
        }

        var valueType = typeof(TProperty);
        var assigningObject = valueType == typeof(object);
        var writeableBody = lambdaExpression.Body is UnaryExpression { Operand: MemberExpression writeableMember }
            ? writeableMember
            : (MemberExpression)lambdaExpression.Body;
        var newValueParam = Expression.Parameter(valueType, "v");

        // If we are assigning value in the type of object, boxing and unboxing might be performed.
        Expression rightExpression = assigningObject
            ? Expression.Condition( // Write default value if the new value is null.
                Expression.Equal(newValueParam, Expression.Constant(null)),
                Expression.Default(writeableBody.Type),
                Expression.Convert(newValueParam, writeableBody.Type))
            : Expression.Convert(newValueParam, writeableBody.Type);

        var assignExpression = Expression.Assign(writeableBody, rightExpression);
        var returnLabel = Expression.Label();
        var statements = new List<Expression> { Expression.Label(returnLabel), assignExpression };

        var innerBody = writeableBody.Expression;

        while (innerBody is MemberExpression or UnaryExpression { Operand: MemberExpression })
        {
            var memberAccess = innerBody as MemberExpression ?? (MemberExpression)((UnaryExpression)innerBody).Operand;

            if (!memberAccess.Type.IsValueType)
            {
                Expression actionWhenNull = newObjectInPath
                    ? Expression.Assign(memberAccess, Expression.New(memberAccess.Type))
                    : Expression.Return(returnLabel);
                var nullCheck = Expression.IfThen(
                    Expression.Equal(memberAccess, Expression.Constant(null)),
                    actionWhenNull);

                statements.Add(nullCheck);
            }

            innerBody = memberAccess.Expression;
        }

        statements.Reverse();
        var newBody = Expression.Block(statements);

        return Expression.Lambda<Action<THost?, TProperty?>>(newBody, propertySelector.Parameters[0], newValueParam).Compile();
    }
}
