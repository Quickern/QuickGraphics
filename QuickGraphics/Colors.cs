namespace QuickGraphics;

public static class Colors
{
    internal static readonly Dictionary<string, Color> NameToColor = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
    internal static readonly Dictionary<Color, string> ColorToName = new Dictionary<Color, string>();

    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F0F8FFFF</c>.</summary>
    public static readonly Color AliceBlue = AddColor(nameof(AliceBlue), 0xF0F8FFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FAEBD7FF</c>.</summary>
    public static readonly Color AntiqueWhite = AddColor(nameof(AntiqueWhite), 0xFAEBD7FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00FFFFFF</c>.</summary>
    public static readonly Color Aqua = AddSynonym(nameof(Aqua), 0x00FFFFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#7FFFD4FF</c>.</summary>
    public static readonly Color Aquamarine = AddColor(nameof(Aquamarine), 0x7FFFD4FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F0FFFFFF</c>.</summary>
    public static readonly Color Azure = AddColor(nameof(Azure), 0xF0FFFFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F5F5DCFF</c>.</summary>
    public static readonly Color Beige = AddColor(nameof(Beige), 0xF5F5DCFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFE4C4FF</c>.</summary>
    public static readonly Color Bisque = AddColor(nameof(Bisque), 0xFFE4C4FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#000000FF</c>.</summary>
    public static readonly Color Black = AddColor(nameof(Black), 0x000000FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFEBCDFF</c>.</summary>
    public static readonly Color BlanchedAlmond = AddColor(nameof(BlanchedAlmond), 0xFFEBCDFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#0000FFFF</c>.</summary>
    public static readonly Color Blue = AddColor(nameof(Blue), 0x0000FFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#8A2BE2FF</c>.</summary>
    public static readonly Color BlueViolet = AddColor(nameof(BlueViolet), 0x8A2BE2FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#A52A2AFF</c>.</summary>
    public static readonly Color Brown = AddColor(nameof(Brown), 0xA52A2AFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#DEB887FF</c>.</summary>
    public static readonly Color BurlyWood = AddColor(nameof(BurlyWood), 0xDEB887FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#5F9EA0FF</c>.</summary>
    public static readonly Color CadetBlue = AddColor(nameof(CadetBlue), 0x5F9EA0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#7FFF00FF</c>.</summary>
    public static readonly Color Chartreuse = AddColor(nameof(Chartreuse), 0x7FFF00FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#D2691EFF</c>.</summary>
    public static readonly Color Chocolate = AddColor(nameof(Chocolate), 0xD2691EFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF7F50FF</c>.</summary>
    public static readonly Color Coral = AddColor(nameof(Coral), 0xFF7F50FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#6495EDFF</c>.</summary>
    public static readonly Color CornflowerBlue = AddColor(nameof(CornflowerBlue), 0x6495EDFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFF8DCFF</c>.</summary>
    public static readonly Color Cornsilk = AddColor(nameof(Cornsilk), 0xFFF8DCFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#DC143CFF</c>.</summary>
    public static readonly Color Crimson = AddColor(nameof(Crimson), 0xDC143CFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00FFFFFF</c>.</summary>
    public static readonly Color Cyan = AddColor(nameof(Cyan), 0x00FFFFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00008BFF</c>.</summary>
    public static readonly Color DarkBlue = AddColor(nameof(DarkBlue), 0x00008BFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#008B8BFF</c>.</summary>
    public static readonly Color DarkCyan = AddColor(nameof(DarkCyan), 0x008B8BFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#B8860BFF</c>.</summary>
    public static readonly Color DarkGoldenrod = AddColor(nameof(DarkGoldenrod), 0xB8860BFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#A9A9A9FF</c>.</summary>
    public static readonly Color DarkGray = AddColor(nameof(DarkGray), 0xA9A9A9FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#006400FF</c>.</summary>
    public static readonly Color DarkGreen = AddColor(nameof(DarkGreen), 0x006400FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#A9A9A9FF</c>.</summary>
    public static readonly Color DarkGrey = AddSynonym(nameof(DarkGrey), DarkGray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#BDB76BFF</c>.</summary>
    public static readonly Color DarkKhaki = AddColor(nameof(DarkKhaki), 0xBDB76BFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#8B008BFF</c>.</summary>
    public static readonly Color DarkMagenta = AddColor(nameof(DarkMagenta), 0x8B008BFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#556B2FFF</c>.</summary>
    public static readonly Color DarkOliveGreen = AddColor(nameof(DarkOliveGreen), 0x556B2FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF8C00FF</c>.</summary>
    public static readonly Color DarkOrange = AddColor(nameof(DarkOrange), 0xFF8C00FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#9932CCFF</c>.</summary>
    public static readonly Color DarkOrchid = AddColor(nameof(DarkOrchid), 0x9932CCFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#8B0000FF</c>.</summary>
    public static readonly Color DarkRed = AddColor(nameof(DarkRed), 0x8B0000FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#E9967AFF</c>.</summary>
    public static readonly Color DarkSalmon = AddColor(nameof(DarkSalmon), 0xE9967AFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#8FBC8FFF</c>.</summary>
    public static readonly Color DarkSeaGreen = AddColor(nameof(DarkSeaGreen), 0x8FBC8FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#483D8BFF</c>.</summary>
    public static readonly Color DarkSlateBlue = AddColor(nameof(DarkSlateBlue), 0x483D8BFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#2F4F4FFF</c>.</summary>
    public static readonly Color DarkSlateGray = AddColor(nameof(DarkSlateGray), 0x2F4F4FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#2F4F4FFF</c>.</summary>
    public static readonly Color DarkSlateGrey = AddSynonym(nameof(DarkSlateGrey), DarkSlateGray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00CED1FF</c>.</summary>
    public static readonly Color DarkTurquoise = AddColor(nameof(DarkTurquoise), 0x00CED1FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#9400D3FF</c>.</summary>
    public static readonly Color DarkViolet = AddColor(nameof(DarkViolet), 0x9400D3FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF1493FF</c>.</summary>
    public static readonly Color DeepPink = AddColor(nameof(DeepPink), 0xFF1493FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00BFFFFF</c>.</summary>
    public static readonly Color DeepSkyBlue = AddColor(nameof(DeepSkyBlue), 0x00BFFFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#696969FF</c>.</summary>
    public static readonly Color DimGray = AddColor(nameof(DimGray), 0x696969FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#696969FF</c>.</summary>
    public static readonly Color DimGrey = AddSynonym(nameof(DimGrey), DimGray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#1E90FFFF</c>.</summary>
    public static readonly Color DodgerBlue = AddColor(nameof(DodgerBlue), 0x1E90FFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#B22222FF</c>.</summary>
    public static readonly Color Firebrick = AddColor(nameof(Firebrick), 0xB22222FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFAF0FF</c>.</summary>
    public static readonly Color FloralWhite = AddColor(nameof(FloralWhite), 0xFFFAF0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#228B22FF</c>.</summary>
    public static readonly Color ForestGreen = AddColor(nameof(ForestGreen), 0x228B22FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF00FFFF</c>.</summary>
    public static readonly Color Fuchsia = AddSynonym(nameof(Fuchsia), 0xFF00FFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#DCDCDCFF</c>.</summary>
    public static readonly Color Gainsboro = AddColor(nameof(Gainsboro), 0xDCDCDCFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F8F8FFFF</c>.</summary>
    public static readonly Color GhostWhite = AddColor(nameof(GhostWhite), 0xF8F8FFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFD700FF</c>.</summary>
    public static readonly Color Gold = AddColor(nameof(Gold), 0xFFD700FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#DAA520FF</c>.</summary>
    public static readonly Color Goldenrod = AddColor(nameof(Goldenrod), 0xDAA520FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#808080FF</c>.</summary>
    public static readonly Color Gray = AddColor(nameof(Gray), 0x808080FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#008000FF</c>.</summary>
    public static readonly Color Green = AddColor(nameof(Green), 0x008000FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#ADFF2FFF</c>.</summary>
    public static readonly Color GreenYellow = AddColor(nameof(GreenYellow), 0xADFF2FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#808080FF</c>.</summary>
    public static readonly Color Grey = AddSynonym(nameof(Grey), Gray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F0FFF0FF</c>.</summary>
    public static readonly Color Honeydew = AddColor(nameof(Honeydew), 0xF0FFF0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF69B4FF</c>.</summary>
    public static readonly Color HotPink = AddColor(nameof(HotPink), 0xFF69B4FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#CD5C5CFF</c>.</summary>
    public static readonly Color IndianRed = AddColor(nameof(IndianRed), 0xCD5C5CFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#4B0082FF</c>.</summary>
    public static readonly Color Indigo = AddColor(nameof(Indigo), 0x4B0082FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFFF0FF</c>.</summary>
    public static readonly Color Ivory = AddColor(nameof(Ivory), 0xFFFFF0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F0E68CFF</c>.</summary>
    public static readonly Color Khaki = AddColor(nameof(Khaki), 0xF0E68CFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#E6E6FAFF</c>.</summary>
    public static readonly Color Lavender = AddColor(nameof(Lavender), 0xE6E6FAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFF0F5FF</c>.</summary>
    public static readonly Color LavenderBlush = AddColor(nameof(LavenderBlush), 0xFFF0F5FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#7CFC00FF</c>.</summary>
    public static readonly Color LawnGreen = AddColor(nameof(LawnGreen), 0x7CFC00FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFACDFF</c>.</summary>
    public static readonly Color LemonChiffon = AddColor(nameof(LemonChiffon), 0xFFFACDFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#ADD8E6FF</c>.</summary>
    public static readonly Color LightBlue = AddColor(nameof(LightBlue), 0xADD8E6FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F08080FF</c>.</summary>
    public static readonly Color LightCoral = AddColor(nameof(LightCoral), 0xF08080FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#E0FFFFFF</c>.</summary>
    public static readonly Color LightCyan = AddColor(nameof(LightCyan), 0xE0FFFFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FAFAD2FF</c>.</summary>
    public static readonly Color LightGoldenrodYellow = AddColor(nameof(LightGoldenrodYellow), 0xFAFAD2FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#D3D3D3FF</c>.</summary>
    public static readonly Color LightGray = AddColor(nameof(LightGray), 0xD3D3D3FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#90EE90FF</c>.</summary>
    public static readonly Color LightGreen = AddColor(nameof(LightGreen), 0x90EE90FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#D3D3D3FF</c>.</summary>
    public static readonly Color LightGrey = AddSynonym(nameof(LightGrey), LightGray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFB6C1FF</c>.</summary>
    public static readonly Color LightPink = AddColor(nameof(LightPink), 0xFFB6C1FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFA07AFF</c>.</summary>
    public static readonly Color LightSalmon = AddColor(nameof(LightSalmon), 0xFFA07AFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#20B2AAFF</c>.</summary>
    public static readonly Color LightSeaGreen = AddColor(nameof(LightSeaGreen), 0x20B2AAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#87CEFAFF</c>.</summary>
    public static readonly Color LightSkyBlue = AddColor(nameof(LightSkyBlue), 0x87CEFAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#778899FF</c>.</summary>
    public static readonly Color LightSlateGray = AddColor(nameof(LightSlateGray), 0x778899FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#778899FF</c>.</summary>
    public static readonly Color LightSlateGrey = AddSynonym(nameof(LightSlateGrey), LightSlateGray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#B0C4DEFF</c>.</summary>
    public static readonly Color LightSteelBlue = AddColor(nameof(LightSteelBlue), 0xB0C4DEFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFFE0FF</c>.</summary>
    public static readonly Color LightYellow = AddColor(nameof(LightYellow), 0xFFFFE0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00FF00FF</c>.</summary>
    public static readonly Color Lime = AddColor(nameof(Lime), 0x00FF00FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#32CD32FF</c>.</summary>
    public static readonly Color LimeGreen = AddColor(nameof(LimeGreen), 0x32CD32FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FAF0E6FF</c>.</summary>
    public static readonly Color Linen = AddColor(nameof(Linen), 0xFAF0E6FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF00FFFF</c>.</summary>
    public static readonly Color Magenta = AddColor(nameof(Magenta), 0xFF00FFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#800000FF</c>.</summary>
    public static readonly Color Maroon = AddColor(nameof(Maroon), 0x800000FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#66CDAAFF</c>.</summary>
    public static readonly Color MediumAquamarine = AddColor(nameof(MediumAquamarine), 0x66CDAAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#0000CDFF</c>.</summary>
    public static readonly Color MediumBlue = AddColor(nameof(MediumBlue), 0x0000CDFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#BA55D3FF</c>.</summary>
    public static readonly Color MediumOrchid = AddColor(nameof(MediumOrchid), 0xBA55D3FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#9370D8FF</c>.</summary>
    public static readonly Color MediumPurple = AddColor(nameof(MediumPurple), 0x9370D8FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#3CB371FF</c>.</summary>
    public static readonly Color MediumSeaGreen = AddColor(nameof(MediumSeaGreen), 0x3CB371FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#7B68EEFF</c>.</summary>
    public static readonly Color MediumSlateBlue = AddColor(nameof(MediumSlateBlue), 0x7B68EEFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00FA9AFF</c>.</summary>
    public static readonly Color MediumSpringGreen = AddColor(nameof(MediumSpringGreen), 0x00FA9AFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#48D1CCFF</c>.</summary>
    public static readonly Color MediumTurquoise = AddColor(nameof(MediumTurquoise), 0x48D1CCFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#C71585FF</c>.</summary>
    public static readonly Color MediumVioletRed = AddColor(nameof(MediumVioletRed), 0xC71585FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#191970FF</c>.</summary>
    public static readonly Color MidnightBlue = AddColor(nameof(MidnightBlue), 0x191970FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F5FFFAFF</c>.</summary>
    public static readonly Color MintCream = AddColor(nameof(MintCream), 0xF5FFFAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFE4E1FF</c>.</summary>
    public static readonly Color MistyRose = AddColor(nameof(MistyRose), 0xFFE4E1FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFE4B5FF</c>.</summary>
    public static readonly Color Moccasin = AddColor(nameof(Moccasin), 0xFFE4B5FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFDEADFF</c>.</summary>
    public static readonly Color NavajoWhite = AddColor(nameof(NavajoWhite), 0xFFDEADFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#000080FF</c>.</summary>
    public static readonly Color Navy = AddColor(nameof(Navy), 0x000080FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FDF5E6FF</c>.</summary>
    public static readonly Color OldLace = AddColor(nameof(OldLace), 0xFDF5E6FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#808000FF</c>.</summary>
    public static readonly Color Olive = AddColor(nameof(Olive), 0x808000FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#6B8E23FF</c>.</summary>
    public static readonly Color OliveDrab = AddColor(nameof(OliveDrab), 0x6B8E23FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFA500FF</c>.</summary>
    public static readonly Color Orange = AddColor(nameof(Orange), 0xFFA500FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF4500FF</c>.</summary>
    public static readonly Color OrangeRed = AddColor(nameof(OrangeRed), 0xFF4500FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#DA70D6FF</c>.</summary>
    public static readonly Color Orchid = AddColor(nameof(Orchid), 0xDA70D6FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#EEE8AAFF</c>.</summary>
    public static readonly Color PaleGoldenrod = AddColor(nameof(PaleGoldenrod), 0xEEE8AAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#98FB98FF</c>.</summary>
    public static readonly Color PaleGreen = AddColor(nameof(PaleGreen), 0x98FB98FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#AFEEEEFF</c>.</summary>
    public static readonly Color PaleTurquoise = AddColor(nameof(PaleTurquoise), 0xAFEEEEFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#D87093FF</c>.</summary>
    public static readonly Color PaleVioletRed = AddColor(nameof(PaleVioletRed), 0xD87093FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFEFD5FF</c>.</summary>
    public static readonly Color PapayaWhip = AddColor(nameof(PapayaWhip), 0xFFEFD5FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFDAB9FF</c>.</summary>
    public static readonly Color PeachPuff = AddColor(nameof(PeachPuff), 0xFFDAB9FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#CD853FFF</c>.</summary>
    public static readonly Color Peru = AddColor(nameof(Peru), 0xCD853FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFC0CBFF</c>.</summary>
    public static readonly Color Pink = AddColor(nameof(Pink), 0xFFC0CBFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#DDA0DDFF</c>.</summary>
    public static readonly Color Plum = AddColor(nameof(Plum), 0xDDA0DDFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#B0E0E6FF</c>.</summary>
    public static readonly Color PowderBlue = AddColor(nameof(PowderBlue), 0xB0E0E6FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#800080FF</c>.</summary>
    public static readonly Color Purple = AddColor(nameof(Purple), 0x800080FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF0000FF</c>.</summary>
    public static readonly Color Red = AddColor(nameof(Red), 0xFF0000FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#BC8F8FFF</c>.</summary>
    public static readonly Color RosyBrown = AddColor(nameof(RosyBrown), 0xBC8F8FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#4169E1FF</c>.</summary>
    public static readonly Color RoyalBlue = AddColor(nameof(RoyalBlue), 0x4169E1FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#8B4513FF</c>.</summary>
    public static readonly Color SaddleBrown = AddColor(nameof(SaddleBrown), 0x8B4513FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FA8072FF</c>.</summary>
    public static readonly Color Salmon = AddColor(nameof(Salmon), 0xFA8072FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F4A460FF</c>.</summary>
    public static readonly Color SandyBrown = AddColor(nameof(SandyBrown), 0xF4A460FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#2E8B57FF</c>.</summary>
    public static readonly Color SeaGreen = AddColor(nameof(SeaGreen), 0x2E8B57FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFF5EEFF</c>.</summary>
    public static readonly Color SeaShell = AddColor(nameof(SeaShell), 0xFFF5EEFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#A0522DFF</c>.</summary>
    public static readonly Color Sienna = AddColor(nameof(Sienna), 0xA0522DFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#C0C0C0FF</c>.</summary>
    public static readonly Color Silver = AddColor(nameof(Silver), 0xC0C0C0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#87CEEBFF</c>.</summary>
    public static readonly Color SkyBlue = AddColor(nameof(SkyBlue), 0x87CEEBFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#6A5ACDFF</c>.</summary>
    public static readonly Color SlateBlue = AddColor(nameof(SlateBlue), 0x6A5ACDFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#708090FF</c>.</summary>
    public static readonly Color SlateGray = AddColor(nameof(SlateGray), 0x708090FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#708090FF</c>.</summary>
    public static readonly Color SlateGrey = AddSynonym(nameof(SlateGrey), SlateGray);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFAFAFF</c>.</summary>
    public static readonly Color Snow = AddColor(nameof(Snow), 0xFFFAFAFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00FF7FFF</c>.</summary>
    public static readonly Color SpringGreen = AddColor(nameof(SpringGreen), 0x00FF7FFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#4682B4FF</c>.</summary>
    public static readonly Color SteelBlue = AddColor(nameof(SteelBlue), 0x4682B4FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#D2B48CFF</c>.</summary>
    public static readonly Color Tan = AddColor(nameof(Tan), 0xD2B48CFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#008080FF</c>.</summary>
    public static readonly Color Teal = AddColor(nameof(Teal), 0x008080FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#D8BFD8FF</c>.</summary>
    public static readonly Color Thistle = AddColor(nameof(Thistle), 0xD8BFD8FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FF6347FF</c>.</summary>
    public static readonly Color Tomato = AddColor(nameof(Tomato), 0xFF6347FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#00000000</c>.</summary>
    public static readonly Color Transparent = AddColor(nameof(Transparent), 0x00000000);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#40E0D0FF</c>.</summary>
    public static readonly Color Turquoise = AddColor(nameof(Turquoise), 0x40E0D0FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#EE82EEFF</c>.</summary>
    public static readonly Color Violet = AddColor(nameof(Violet), 0xEE82EEFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F5DEB3FF</c>.</summary>
    public static readonly Color Wheat = AddColor(nameof(Wheat), 0xF5DEB3FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFFFFFF</c>.</summary>
    public static readonly Color White = AddColor(nameof(White), 0xFFFFFFFF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#F5F5F5FF</c>.</summary>
    public static readonly Color WhiteSmoke = AddColor(nameof(WhiteSmoke), 0xF5F5F5FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#FFFF00FF</c>.</summary>
    public static readonly Color Yellow = AddColor(nameof(Yellow), 0xFFFF00FF);
    /// <summary>Gets the system-defined color that has an RGBA value of <c>#9ACD32FF</c>.</summary>
    public static readonly Color YellowGreen = AddColor(nameof(YellowGreen), 0x9ACD32FF);

    /// <summary>
    /// Gets CGA color by its color's number.
    /// </summary>
    /// <param name="color">Number 0-15. 4 bits of IRGB format. `I` is intensity, `R`, `G` and `B` is Red, Green and Blue.</param>
    public static Color Cga(int color)
    {
        Color result = new Color(
            (byte)(170 * ((color & 4) / 4) + 85 * ((color & 8) / 8)),
            (byte)(170 * ((color & 2) / 2) + 85 * ((color & 8) / 8)),
            (byte)(170 * ((color & 1) / 1) + 85 * ((color & 8) / 8)),
            255
        );
        if (color == 6)
            result.Green = (byte) (result.Green / 2);
        return result;
    }

    private static Color AddColor(string name, Color value)
    {
        NameToColor.Add(name, value);
        ColorToName.Add(value, name);
        return value;
    }

    private static Color AddSynonym(string name, Color value)
    {
        NameToColor.Add(name, value);
        return value;
    }
}
