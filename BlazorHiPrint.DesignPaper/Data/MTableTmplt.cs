using BlazorHiprint.DesignPaper.Attributes;
using BlazorHiprint.DesignPaper.Data;
using System.Reflection;

public class MTableTmplt : MComponentTmpltBase
{
    public int RowHeight { get; set; } = 30;
    public int ColumnWidth { get; set; } = 100;

    public MTableTmplt(double top, double left, Action<string, object?>? fieldHasChanged)
        : base(top, left, fieldHasChanged, UnitType.Table)
    {
    }
    private Type? _tmodel; // 表格数据模型类型
    private IEnumerable<object> _items = new List<object>(); // 表格数据项集合
    private IEnumerable<MTableColumn> _columns = new[] { new MTableColumn("A"), new MTableColumn("B"), new MTableColumn("C") }; // 表格列定义集合
    /// <summary>
    /// 获取或设置表格数据项集合
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public IEnumerable<object> Items
    {
        get { return _items; }
        set
        {
            var hasChanged = _items != value;
            if (hasChanged)
            {
                _items = value;
                FieldHasChanged?.Invoke(nameof(Items), value);
            }
        }
    }
    /// <summary>
    /// 获取或设置表格数据模型类型
    /// </summary>
    [AutoGenerateColumn(Ignore = true)]
    public Type? TModel
    {
        get { return _tmodel; }
        set
        {
            var hasChanged = _tmodel != value;
            if (hasChanged)
            {

                _tmodel = value;
                if(_tmodel != null)
                {
                    var properties = _tmodel
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p =>!typeof(Delegate).IsAssignableFrom(p.PropertyType) ).ToArray();
                    var cls = new List<MTableColumn>();
                    foreach (var pt in properties)
                    {
                        cls.Add(new MTableColumn(pt.Name) { FieldHasChanged=this.FieldHasChanged});
                    }
                    _columns = cls;
                }

                FieldHasChanged?.Invoke(nameof(Type), _tmodel);
            }
        }
    }
    /// <summary>
    /// 获取或设置表格列定义集合
    /// </summary>
    public IEnumerable<MTableColumn> Columns
    {
        get { return _columns; }

    }

}

/// <summary>
/// 表格列定义类
/// </summary>
public class MTableColumn 
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    public MTableColumn(string propertyName)
    {
        PropertyName = propertyName;
        DisplayName = propertyName;

    }
    public Action<string, object?>? FieldHasChanged { get; set; }
    /// <summary>
    /// 获取或设置属性名称
    /// </summary>
    public string PropertyName { get; set; }
    
    /// <summary>
    /// 获取或设置显示名称
    /// </summary>
    public string DisplayName { get; set; }
    
    /// <summary>
    /// 获取或设置属性类型
    /// </summary>
    public string PropertyType { get; set; }
    bool _visible=true;
    /// <summary>
    /// 获取或设置是否可见
    /// </summary>
    public bool Visible { get { return _visible; } set {
            var hasChanged = _visible != value;
            if (hasChanged)
            {
                _visible = value;
                FieldHasChanged?.Invoke(nameof(Visible), value);

            }
        } }
}
