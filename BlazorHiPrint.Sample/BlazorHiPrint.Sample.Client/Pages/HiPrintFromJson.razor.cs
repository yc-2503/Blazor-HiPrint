using BlazorHiPrint.DesignPaper.Components;
using BlazorHiPrint.DesignPaper.Components.Text;
using BlazorHiPrint.DesignPaper.Components.BarCode;
using BlazorHiPrint.DesignPaper.Components.Rectangle;
using BlazorHiPrint.DesignPaper.Components.Line;

namespace BlazorHiPrint.Sample.Client.Pages;

public partial class HiPrintFromJson
{
    private List<MComponentTmpltBase> MyPrintItems = new();
    void InitDemo()
    {
        MyPrintItems.Clear();
        
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { 
                new System.Text.Json.Serialization.JsonStringEnumConverter(),
                new DesignPaper.Data.TypeJsonConverter()
            }
        };

        // 文本组件
        string textJson = """
            {
                "Top": 10,
                "Left": 10,
                "Text": "示例文本",
                "UnitType":"Text",
                "FontSize": 12
            }
            """;
        var m2 = System.Text.Json.JsonSerializer.Deserialize<MTextTmplt>(textJson, options);

        // 条形码组件
        string barcodeJson = """
            {
                "Top": 20,
                "Left": 60,
                "UnitType":"BarCode",
                "Text": "123456789"
            }
            """;
        var m3 = System.Text.Json.JsonSerializer.Deserialize<MBarCodeTmplt>(barcodeJson, options);

        // 矩形组件
        string rectJson = """
            {
                "Top": 160,
                "Left": 10,
                "Width": 100,
                "Height": 20
            }
            """;
        var m4 = System.Text.Json.JsonSerializer.Deserialize<MRectangleTmplt>(rectJson, options);

        // 线条组件
        string lineJson = """
            {
                "Top": 180,
                "Left": 30,
                "X2": 100,
                "Y2": 180
            }
            """;
        var m5 = System.Text.Json.JsonSerializer.Deserialize<MLineTmplt>(lineJson, options);

        // 表格组件
        string tableJson = """
            {
                "Top": 100,
                "Left": 40,
                "TModel": "BlazorHiPrint.Sample.Client.Pages.Person, BlazorHiPrint.Sample.Client"
            }
            """;
        var m6 = System.Text.Json.JsonSerializer.Deserialize<MTableTmplt>(tableJson, options);

        // 设置表格数据
        if (m6 != null)
        {
            m6.Items = new[]
            {
                new Person(10895, "Jean Martin", new DateOnly(1985, 3, 16)),
                new Person(10944, "António Langa", new DateOnly(1991, 12, 1)),
                new Person(11203, "Julie Smith", new DateOnly(1958, 10, 10)),
                new Person(11205, "Nur Sari", new DateOnly(1922, 4, 27)),
                new Person(11898, "Jose Hernandez", new DateOnly(2011, 5, 3)),
                new Person(12130, "Kenji Sato", new DateOnly(2004, 1, 9))
            };
        }

        MyPrintItems.Add(m2);
        MyPrintItems.Add(m3);
        MyPrintItems.Add(m4);
        MyPrintItems.Add(m5);
        MyPrintItems.Add(m6);
    }
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        InitDemo();
    }

}
