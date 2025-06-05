using BlazorHiprint.DesignPaper.Components;
using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorHiPrint.Client.Pages;

public partial class HiPrintV2
{
    private List<MComponentCfgBase> MyPrintItems = new();
    private MComponentCfgBase? SelectedItem { get; set; }
    private Dictionary<string, object> configParameters = new();
    private bool isReadyAddNew = false;
    private UnitType newType;

    private Type? selectedType => PrintElementsFactory.GetPrintElementConfigureType(SelectedItem!);

    protected override void OnInitialized()
    {
        InitDemo();
    }

    void InitDemo()
    {
        MyPrintItems.Clear();

        MComponentCfgBase m2 = PrintElementsFactory.CreateMTmplt(new CreateMTmpltOptions()
        {
            Top = 10,
            Left = 10,
            //    FiledHasChanged = (_, _) => StateHasChanged(),
            UnitType = UnitType.Text
        });
        MComponentCfgBase m3 = PrintElementsFactory.CreateMTmplt(new CreateMTmpltOptions()
        {
            Top = 20,
            Left = 60,
            //    FiledHasChanged = (_, _) => StateHasChanged(),
            UnitType = UnitType.BarCode
        });

        MComponentCfgBase m4 = PrintElementsFactory.CreateMTmplt(new CreateMTmpltOptions()
        {
            Top = 160,
            Left = 10,
            //    FiledHasChanged = (_, _) => StateHasChanged(),
            UnitType = UnitType.Rectangle
        });
        MComponentCfgBase m5 = PrintElementsFactory.CreateMTmplt(new CreateMTmpltOptions()
        {
            Top = 180,
            Left = 30,
            //    FiledHasChanged = (_, _) => StateHasChanged(),
            UnitType = UnitType.Line
        });

        IEnumerable<Person> people = new[]
    {
        new Person(10895, "Jean Martin", new DateOnly(1985, 3, 16)),
        new Person(10944, "António Langa", new DateOnly(1991, 12, 1)),
        new Person(11203, "Julie Smith", new DateOnly(1958, 10, 10)),
        new Person(11205, "Nur Sari", new DateOnly(1922, 4, 27)),
        new Person(11898, "Jose Hernandez", new DateOnly(2011, 5, 3)),
        new Person(12130, "Kenji Sato", new DateOnly(2004, 1, 9)),
    };

        IEnumerable<MTableColumn> tableColumns = new[]
        {
            new MTableColumn("PersonId"),
            new MTableColumn("Name"),
            new MTableColumn("PromotionDate"),

        };
        MTableTmplt m6 = new MTableTmplt(100, 40, null)
        {
            TModel = typeof(Person),
            Columns = tableColumns,
            Items = people
        };
        SelectedItem = m2;
        configParameters["Data"] = SelectedItem;

        MyPrintItems.Add(m2);
        MyPrintItems.Add(m3);
        MyPrintItems.Add(m4);
        MyPrintItems.Add(m5);
        MyPrintItems.Add(m6);


    }

    void PrintItemClicked(MComponentCfgBase item)
    {

        configParameters["Data"] = item;
        SelectedItem = item;
        StateHasChanged();

    }


    void ClearSelectedItems(MComponentCfgBase item)
    {
        MyPrintItems.Remove(item);
        StateHasChanged();
    }

    private void HandleDrop(DragEventArgs args)
    {
        if (isReadyAddNew)
        {
            var newItem = PrintElementsFactory.CreateMTmplt(new CreateMTmpltOptions()
            {
                Top = args.OffsetY,
                Left = args.OffsetX,
                UnitType = newType,
                FieldHasChanged = (_, _) => StateHasChanged()
            });

            //   SelectedItem = newItem;
            MyPrintItems.Add(newItem);
            isReadyAddNew = false;
        }
    }

    private void HandleDragStart(DragEventArgs args, UnitType type)
    {
        isReadyAddNew = true;
        newType = type;
    }

    private void HandleDragEnd(DragEventArgs args)
    {
        isReadyAddNew = false;
    }
}
