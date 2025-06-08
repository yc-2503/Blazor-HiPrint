using BlazorHiprint.DesignPaper.Components;
using BlazorHiprint.DesignPaper.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;


namespace BlazorHiPrint.DesignPaper.Components;

public partial class MDesignPaper
{
    //组件被点击
    [Parameter]
    public Action<MComponentTmpltBase>? OnComponentClicked { get; set; }
    //组件被删除
    [Parameter]
    public Action<MComponentTmpltBase>? OnComponentDeleted { get; set; }
    //需要显示的组件列表
    IList<MComponentTmpltBase> _printItems = new List<MComponentTmpltBase>();

    [Parameter]
    public IList<MComponentTmpltBase> PrintItems
    {
        get { return _printItems; }
        set { _printItems = value; }
    }
    /// <summary>
    /// 当前选中的组件
    /// </summary>
    [Parameter]
    public MComponentTmpltBase? SelectedItem { get; set; }
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
                item.FieldHasChanged += (name, _) => {
                    StateHasChanged();
                };
                renderElements.Add(new MRenderElements(item));
            }
        }
        renderElements.RemoveAll(x=>!_printItems.Any(y=>y.ID==x.ID));
    }
    //元素拖拽过程，记录元素开始位置,用于计算鼠标移动的相对位置
    double dragStartTop { get; set; } = 20;
    double dragStartLeft { get; set; } = 20;

    List<MRenderElements> renderElements = new List<MRenderElements>();

    /// <summary>
    /// 页面控件被点击时触发的事件
    /// </summary>
    /// <param name="item"></param>
    void PrintCompnonetCliecked(MComponentTmpltBase item)
    {
        foreach (var pm in PrintItems)
        {
            if (pm != item)
            {
                pm.IsSelected = false;
            }
        }
        item.IsSelected = true;
        SelectedItem = item;
        if (OnComponentClicked != null)
        {
            OnComponentClicked.Invoke(SelectedItem); 
        }
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

    void ClearSelectedItems(MComponentTmpltBase item)
    {
        PrintItems.Remove(item);
        renderElements.RemoveAll((x=>x.ID==item.ID));
        if (OnComponentDeleted != null)
        {
            OnComponentDeleted.Invoke(item);
        }
    }
    void PrintPaperOnKeyPressed(KeyboardEventArgs args, MComponentTmpltBase item)
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
    }
}
class MRenderElements
{
    public MRenderElements(MComponentTmpltBase mcmpntConfig)
    {
        MCmpntConfig = mcmpntConfig;
        Fragment =  (cfg)=> (RenderTreeBuilder __builder) => {

            __builder.OpenComponent(1,PrintElementsFactory.GetPrintElementType(cfg));
            __builder.AddComponentParameter(2, "Data", cfg);
            __builder.CloseComponent();

        };
    }
    public string ID { get { return MCmpntConfig.ID; } }
    public MComponentTmpltBase MCmpntConfig { get; init; }
    public RenderFragment<MComponentTmpltBase>Fragment { get; init; }



}