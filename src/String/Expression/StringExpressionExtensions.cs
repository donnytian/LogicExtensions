using System.Linq.Expressions;

namespace LogicExtensions;

/// <summary>
/// Expression extensions for the <see cref="string"/> class.
/// </summary>
public static class StringExpressionExtensions
{
    /// <summary>
    /// Creates a lambda expression to select a property from the host object.
    /// </summary>
    /// <param name="propertyPath">The property path in the form of 'Prop1.Prop2.Prop3'.</param>
    /// <param name="host">The root host object. Only used for the real type detection for 'dynamic' type.</param>
    /// <param name="pathStartsWithHostType">Whether the propertyPath starts with the host name like 'obj.Prop1.Prop2.Prop3'.</param>
    /// <returns>The lambda expression to select a property from the host object.</returns>
    public static Expression<Func<THost, TProperty>> ToSelectorLambda<THost, TProperty>(
        this string? propertyPath, THost? host = default, bool pathStartsWithHostType = false)
    {
        var parts = propertyPath?.Split('.');
        if (parts is null || parts.Length == 0)
        {
            throw new ArgumentException($"Property path is invalid: {propertyPath}", nameof(propertyPath));
        }

        if (pathStartsWithHostType && parts.Length <= 1)
        {
            throw new ArgumentException($"If pathStartsWithHostType is true, at least 2 path parts required.", nameof(propertyPath));
        }

        // for 'dynamic' type, 'GetType' works better than typeof.
        var hostType = host?.GetType() ?? typeof(THost);
        var param = Expression.Parameter(typeof(THost), "x"); // T will be 'object' for dynamic
        Expression body = Expression.Convert(param, hostType); // force convert to underlying type for dynamic

        // Skip the first part if required, which is the host object type.
        for (var i = pathStartsWithHostType ? 1 : 0; i < parts.Length; i++)
        {
            var member = parts[i];
            body = Expression.PropertyOrField(body, member);
        }

        var selector = Expression.Lambda<Func<THost, TProperty>>(Expression.Convert(body, typeof(TProperty)), param);

        return selector;
    }

    /// <summary>
    /// Creates a Func to get the property value conditionally (return default if any host is null in the chain of path) by the given path.
    /// </summary>
    /// <param name="propertyPath">The property path in the form of 'Prop1.Prop2.Prop3'.</param>
    /// <param name="host">The root host object. Only used for the real type detection for 'dynamic' type.</param>
    /// <param name="pathStartsWithHostType">Whether the propertyPath starts with the host name like 'obj.Prop1.Prop2.Prop3'.</param>
    public static Func<THost?, TProperty?> CreateConditionalGetter<THost, TProperty>(
        this string? propertyPath, THost? host = default, bool pathStartsWithHostType = false)
    {
        var selector = ToSelectorLambda<THost, TProperty>(propertyPath, host, pathStartsWithHostType);
        return selector.CreateConditionalGetter();
    }

    /// <summary>
    /// Creates a Func to set the property value conditionally by the given path.
    /// </summary>
    /// <param name="propertyPath">The property path in the form of 'Prop1.Prop2.Prop3'.</param>
    /// <param name="host">The root host object. Only used for the real type detection for 'dynamic' type.</param>
    /// <param name="pathStartsWithHostType">Whether the propertyPath starts with the host name like 'obj.Prop1.Prop2.Prop3'.</param>
    /// <param name="newObjectInPath">
    /// True to create new object with the public parameterless constructor for any 'null' in the chain of property;
    /// False to abort the assignment and does nothing.</param>
    public static Action<THost, TProperty> CreateConditionalSetter<THost, TProperty>(
        this string propertyPath, THost? host = default, bool pathStartsWithHostType = false, bool newObjectInPath = true)
    {
        var selector = ToSelectorLambda<THost, TProperty>(propertyPath, host, pathStartsWithHostType);
        return selector.CreateConditionalSetter(newObjectInPath);
    }
}
