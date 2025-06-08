using System;

namespace BlazorHiprint.DesignPaper.Attributes
{
    /// <summary>
    /// ���������Ƿ����Զ��������ɵ�����
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AutoGenerateColumnAttribute : Attribute
    {
        /// <summary>
        /// �Ƿ���Դ����ԣ����ڱ�����ʾ��
        /// </summary>
        public bool Ignore { get; set; } = false;
    }
}
