using System;

namespace BlazorHiprint.DesignPaper.Attributes
{
    /// <summary>
    /// 控制属性是否在自动表单中生成的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AutoGenerateColumnAttribute : Attribute
    {
        /// <summary>
        /// 是否忽略此属性（不在表单中显示）
        /// </summary>
        public bool Ignore { get; set; } = false;
    }
}
