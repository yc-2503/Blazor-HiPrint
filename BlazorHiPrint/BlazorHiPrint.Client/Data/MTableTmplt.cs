namespace BlazorHiPrint.Client.Data;

/// <summary>
/// 表格打印模板类，用于定义打印模板中的表格元素
/// </summary>
public class MTableTmplt : MTmpltBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="top">表格顶部位置</param>
    /// <param name="left">表格左侧位置</param>
    /// <param name="filedHasChanged">字段变更回调函数</param>
    public MTableTmplt(double top, double left, Action<string, object?>? filedHasChanged) : base(top, left, filedHasChanged, UnitType.Table)
    {
        
    }
    private Type? _tmodel; // 表格数据模型类型
    private IEnumerable<object> _items = new List<object>(); // 表格数据项集合
    private IEnumerable<MTableColumn> _columns = new[] { new MTableColumn("A") ,new MTableColumn("B") , new MTableColumn("C") }; // 表格列定义集合
    /// <summary>
    /// 获取或设置表格数据项集合
    /// </summary>
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
    public Type? TModel
    {
        get { return _tmodel; }
        set
        {
            var hasChanged = _tmodel != value;
            if (hasChanged)
            {

                _tmodel = value;
                FieldHasChanged?.Invoke(nameof(Type), value);
            }
        }
    }
    /// <summary>
    /// 获取或设置表格列定义集合
    /// </summary>
    public IEnumerable<MTableColumn> Columns { get { return _columns; }
        set { 
            var hasChanged = _columns != value;
            if (hasChanged)
            {
                _columns = value;
                FieldHasChanged?.Invoke(nameof(Columns), value);

            }
        }
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
        Visible = true;
    }
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
    
    /// <summary>
    /// 获取或设置是否可见
    /// </summary>
    public bool Visible { get; set; }
}
