﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace BlazorHiPrint.DesignPaper.Components.BarCode;


public partial class MBarCode
{

    private static readonly Writer Encoder = new MultiFormatWriter();
    [Parameter]
    public BarcodeFormat _format { get; set; } = BarcodeFormat.DATA_MATRIX;

    [Parameter]
    public string? OuterStyle { get; set; }
    [Parameter]
    public string? EmptySrc { get; set; }

    [Parameter]
    public string? ErrorText { get; set; }

    /// <summary>
    /// Use this value on not square sized barcode formats like UPC_A and UPC_E.
    /// </summary>
    [Parameter]
    public int ForceHeight { get; set; } = 1;

    /// <summary>
    /// Increase the stroke width if readers can not read the barcode easily.
    /// </summary>
    [Parameter]
    public double StrokeWidth { get; set; }

    /// <summary>
    /// Determines how user go to href content. Default is open in a new tab.
    /// </summary>
    [Parameter]
    public string? Target { get; set; } = "_blank";

    ///// <summary>
    ///// The value of the barcode format.
    ///// </summary>
    //[Parameter]
    //public string? Value { get; set; }

    /// <summary>
    /// The color of the barcode as string. Accepts all kinds of CSS property values. (ex. #123456) Default is "black".
    /// </summary>
    [Parameter]
    public string? Color { get; set; } = "black";

    /// <summary>
    /// The background color of the barcode as string. Accepts all kinds of CSS property values. (ex. #123456) Default is "white".
    /// </summary>
    [Parameter]
    public string? BackgroundColor { get; set; } = "white";

    /// <summary>
    /// Fires when value changed.
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }



    /// <summary>
    /// Barcode process that returns BarcodeResult. Returns null if value is also null or empty.
    /// </summary>
    /// <returns></returns>
    protected BarcodeResult? GetCode(string value)
    {

        try
        {
            var matrix = Encoder.encode(value, _format, 0, 0);
            int sizeX = (int)(Data.Width / matrix.Width);
            if(sizeX == 0)
            {
                sizeX = 1;
            }
            var result = new BarcodeResult(matrix, sizeX, ForceHeight);
            ErrorText = null;
            return result;
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
            return null;
        }
    }
}

