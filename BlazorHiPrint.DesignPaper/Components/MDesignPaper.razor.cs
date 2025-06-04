using BlazorHiprint.DesignPaper.Components;
using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;


namespace BlazorHiPrint.DesignPaper.Components;

public partial class MDesignPaper
{
    #region 页面尺寸
    private Dictionary<string, (double Width, double Height)> PaperSizes = new()
    {
        ["A3"] = (420, 297),
        ["A4"] = (297, 210),
        ["A5"] = (210, 148),
        ["B3"] = (500, 353),
        ["B4"] = (353, 250),
        ["B5"] = (250, 176)
    };

    private string CurrentPaperSizeKey { get; set; } = "A3";
    private (double Width, double Height) CurrentPaperSize => PaperSizes[CurrentPaperSizeKey];
    #endregion

    IEnumerable<MComponentCfgBase> _printItems = new List<MComponentCfgBase>();

    [Parameter]
    public IEnumerable<MComponentCfgBase> PrintItems
    {
        get { return _printItems; }
        set { _printItems = value; }
    }
    [Parameter]
    public MComponentCfgBase? SelectedItem { get; set; }
    //bool _shouldRender = false;
    //protected override bool ShouldRender()
    //{
    //    return _shouldRender;
    //}
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        foreach(var item in _printItems)
        {
            if(!renderElements.Any(x=>x.ID == item.ID))
            {
                renderElements.Add(new MRenderElements(item));
            }
        }
        renderElements.RemoveAll(x=>!_printItems.Any(y=>y.ID==x.ID));
    }
    //元素拖拽过程，记录元素开始位置,用于计算鼠标移动的相对位置
    double dragStartTop { get; set; } = 20;
    double dragStartLeft { get; set; } = 20;

    List<MRenderElements> renderElements = new List<MRenderElements>();

    void PrintItemCliecked(MComponentCfgBase item)
    {
        foreach (var pm in PrintItems)
        {
            if (pm != item)
            {
                pm.IsSelected = false;
            }
        }
        SelectedItem = item;

        StateHasChanged();
    }
    //打印纸空白区域被点击
    void PrintPaperClicked(MouseEventArgs args)
    {
        foreach (var pm in PrintItems)
        {
            pm.IsSelected = false;

        }
        SelectedItem = null;
    }

    void ClearSelectedItems(MComponentCfgBase item)
    {
        renderElements.RemoveAll((x=>x.ID==item.ID));
        StateHasChanged();
    }
    void PrintPaperOnKeyPressed(KeyboardEventArgs args, MComponentCfgBase item)
    {
        if (args.Key == "Delete")
        {
            ClearSelectedItems(item);
        }
    }


    //记录拖拽开始时的位置，用来计算移动的相对位置
    private void TempWindDragStart(DragEventArgs args)
    {
        dragStartTop = args.ClientY;
        dragStartLeft = args.ClientX;

    }
    private void TempWindDrag(DragEventArgs args)
    {
        if (SelectedItem != null)
        {
            if (args.ClientX <= 0 || args.ClientY <= 0)
            {
                return;
            }
            double dy = args.ClientY - dragStartTop;
            double dx = args.ClientX - dragStartLeft;
            if (Math.Abs(dx) < 2.2 && Math.Abs(dy) < 2)
            {
                return;
            }
            // 鼠标移动如果没有超过 MDragItem 的范围，offsetX Y 的值会是 MDragItem 内部的相对位置，移动会有问题，
            // 这里是要获取鼠标移动的相对距离
            SelectedItem.Top += dy;
            SelectedItem.Left += dx;
            //SelectedItem.MoveTo(SelectedItem.Left+dx,SelectedItem.Top+dy);

            dragStartLeft = args.ClientX;
            dragStartTop = args.ClientY;
        }
        // top = args.ClientY;
        // left = args.ClientX;
        // args.DataTransfer.EffectAllowed = "move";
    }
}
class MRenderElements
{
    public MRenderElements(MComponentCfgBase mcmpntConfig)
    {
        MCmpntConfig = mcmpntConfig;
        Fragment =  (cfg)=> (RenderTreeBuilder __builder) => {

            __builder.OpenComponent(1,mcmpntConfig.ComponentType);
            __builder.AddComponentParameter(2, "Data", cfg);
            __builder.CloseComponent();

        };
    }
    public string ID { get { return MCmpntConfig.ID; } }
    public MComponentCfgBase MCmpntConfig { get; init; }
    public RenderFragment<MComponentCfgBase>Fragment { get; init; }

}