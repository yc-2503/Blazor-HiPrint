using BlazorHiprint.DesignPaper.Attributes;
using System.Data;
using System.Reflection;
using ZXing.QrCode.Internal;


namespace BlazorHiPrint.DesignPaper.Components.MEditorForm;

public partial class MEditorForm
{
    bool IsBasicType(Type type)
    {
        if (type.IsPrimitive ||
            type == typeof(string) ||
            type == typeof(decimal) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(TimeSpan) ||
            type == typeof(Guid) ||
            type.IsEnum)
        {
            return true;
        }

        Type underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
        {
            return IsBasicType(underlyingType);
        }

        // Check for IEnumerable<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            return false;
        }

        return false;
    }
    /// <summary>
    /// 把所有 model 的所有需要编辑显示的属性转换成 ComponentPrptInfo 列表
    /// </summary>
    /// <param name="model"></param>
    /// <param name="level"></param>
    /// <param name="parentPrpt"></param>
    /// <returns></returns>
    List<ComponentPrptInfo> ParsePrpt(object model,  int level,ComponentPrptInfo? parentPrpt=null)
    {
        Type type = model.GetType();

        List<ComponentPrptInfo> prptInfos = new List<ComponentPrptInfo>();
        //如果是一个 IEnumerable 类型，需要把列表中的每个元素显示出来。
        if (parentPrpt != null && parentPrpt.Type.IsGenericType && parentPrpt.Type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            var collection = parentPrpt.Value as System.Collections.IEnumerable;
            if (collection != null)
            {

                var itemType = parentPrpt.Type.GetGenericArguments()[0];
                int i = 0;
                foreach (var prop in collection)
                {

                    ComponentPrptInfo pt = new ComponentPrptInfo(prop, itemType, parentPrpt.Name + (i++), parentPrpt);
                    pt.Level = level;
                    pt.NeedCollapse = true;
                    prptInfos.Add(pt);
                }
            }

        }
        else
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p =>
            !typeof(Delegate).IsAssignableFrom(p.PropertyType) && (p.GetCustomAttribute<AutoGenerateColumnAttribute>()?.Ignore != true)).ToArray();
            foreach (var prop in properties)
            {
                ComponentPrptInfo pt = new ComponentPrptInfo(model, prop, parentPrpt);
                pt.Level = level;
                if (IsBasicType(prop.PropertyType))
                {
                    pt.NeedCollapse = false;
                }
                else
                {

                    pt.NeedCollapse = true;
                }
                prptInfos.Add(pt);
            }


        }
        return prptInfos;
    }
}
/// <summary>
/// 控件属性信息，主要用于自动生成控件属性编辑页面
/// </summary>
public class ComponentPrptInfo
{
    public ComponentPrptInfo(object model, PropertyInfo property, ComponentPrptInfo? parent=null) 
    { 
        this._name = property.Name;
        this._type = property.PropertyType;
        this._property = property;
        this._parentModel = model;
        Id = Guid.NewGuid();
        if(parent != null)
        {
            ParentId = parent.Id;
        }
    }
    public ComponentPrptInfo(object model, Type propertyType,string name, ComponentPrptInfo? parent = null)
    {
        this._property = null;
        this._parentModel = model;
        this._name = name;
        this._type = propertyType;
        Id = Guid.NewGuid();
        if (parent != null)
        {
            ParentId = parent.Id;
        }
    }
    public Guid Id { get; init; }
    /// <summary>
    /// 父属性的ID，比如父属性的类型是列表和Class的场景
    /// </summary>
    public Guid ParentId { get; set; }
    string _name;
    public string Name { get { return _name; } }
    /// <summary>
    /// 折叠的层级
    /// </summary>
    public int Level { get; set; }
    Type _type;
    public Type Type { get { return _type; } }
    /// <summary>
    /// 是否需要折叠，比如列表和Class 需要折叠
    /// </summary>
    public bool NeedCollapse { get; set; }
    /// <summary>
    /// 是否已经折叠
    /// </summary>
    public bool IsCollapsed { get; set; }
    private object _parentModel;
    private PropertyInfo? _property;
    //public List<ComponentPrptInfo>? SubProperties { get; set; }
    public object? Value
    {
        get
        {
            return _property!=null?_property.GetValue(_parentModel):_parentModel;
        }
        set
        {
            SetPropertyValue(value);
        }
        
    }
    void SetPropertyValue(object? value)
    {
        if(_property == null)
        {
            return;
        }
        if (_property.PropertyType == typeof(string))
        {
            _property.SetValue(_parentModel, value);
        }else if(_property.PropertyType.IsValueType)
        {
            if(value == null)
            {
                return;
            }
            if (_property.PropertyType.IsEnum)
            {
                if (!string.IsNullOrEmpty((string)value))
                {

                    var enumValue = Enum.Parse(_property.PropertyType, (string)value);
                    _property.SetValue(_parentModel, enumValue);

                }
            }else if(_property.PropertyType == typeof(bool))
            {
                _property.SetValue(_parentModel, value);
            }
            else
            {
                var convertedValue = Convert.ChangeType((string)value, _property.PropertyType);
                _property.SetValue(_parentModel, convertedValue);
            }

        }


    }


}