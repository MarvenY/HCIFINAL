namespace Le2me.Converters;

/// <summary>Returns true if the string is non-null and non-empty.</summary>
public class StringToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => !string.IsNullOrEmpty(value as string);

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => throw new NotImplementedException();
}

/// <summary>Inverts a bool value.</summary>
public class InvertBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => value is bool b && !b;

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => value is bool b && !b;
}

/// <summary>
/// Converts bool to one of two strings separated by '|'.
/// ConverterParameter="TrueString|FalseString"
/// </summary>
public class BoolToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        var parts = (parameter as string ?? "|").Split('|');
        var isTrue = value is bool b && b;
        return isTrue ? (parts.Length > 0 ? parts[0] : "") : (parts.Length > 1 ? parts[1] : "");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        => throw new NotImplementedException();
}
