using System.Diagnostics.CodeAnalysis;


namespace BlazorHiPrint.DesignPaper.Components.Table;

public partial class MTable
{
    [NotNull]
    IQueryable<object> items;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        items = Data.Items.AsQueryable();

    }
    public object? GetValue(object obj, string propertyName)
    {
        if (Data.TModel == null)
        {
            return string.Empty;
        }
        var prpt = Data.TModel.GetProperty(propertyName);
        if (prpt == null)
        {
            return string.Empty;
        }
        else
        {

            return prpt.GetValue(obj, null);
        }
    }
    public void ColumnClick(string column)
    {
        Console.WriteLine("ColumnClick "+column);
    }


}
