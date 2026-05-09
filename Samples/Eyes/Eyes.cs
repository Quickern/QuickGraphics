using System.Diagnostics;

await ForCanvas(640, 480);

Number w = 150, h = 100;
Number x = 320 - w / 2, y = 240 - h / 2;

Stopwatch sw = Stopwatch.StartNew();
sw.Restart();

Number ex = w * 0.23f;
Number ey = h * 0.5f;
Number lx = x + ex;
Number ly = y + ey;
Number rx = x + w - ex;
Number ry = y + ey;
Number br = MathF.Min(ex, ey) * 0.5f;

while (IsNotClosed)
{
    Number t = sw.Elapsed.TotalSeconds;

    Number blink = 1.0f - MathF.Pow(MathF.Sin(t * 0.5f), 200.0f) * 0.8f;

    Clear((76, 76, 76, 255));

    Ellipse(Fill(0, 0, 0, 32), (lx + 3, ly + 16), ex, ey);
    Ellipse(Fill(0, 0, 0, 32), (rx + 3, ry + 16), ex, ey);

    Ellipse(Fill(220, 220, 220), (lx, ly), ex, ey);
    Ellipse(Fill(220, 220, 220), (rx, ry), ex, ey);

    (Number mx, Number my) = Mouse.Position;

    Circle(Red, (mx, my), 10);

    float dx = (mx - rx) / (ex * 10.0f);
    float dy = (my - ry) / (ey * 10.0f);
    float d = MathF.Sqrt(dx * dx + dy * dy);
    if (d > 1.0f)
    {
        dx /= d;
        dy /= d;
    }
    dx *= ex * 0.4f;
    dy *= ey * 0.5f;

    Ellipse(Fill(32, 32, 32), (lx + dx, ly + dy + ey * 0.25f * (1.0f - blink)), br, br * blink);

    dx = (mx - rx) / (ex * 10.0f);
    dy = (my - ry) / (ey * 10.0f);
    d = MathF.Sqrt(dx * dx + dy * dy);
    if (d > 1.0f)
    {
        dx /= d;
        dy /= d;
    }
    dx *= ex * 0.4f;
    dy *= ey * 0.5f;

    Ellipse(Fill(32, 32, 32), (rx + dx, ry + dy + ey * 0.25f * (1.0f - blink)), br, br * blink);

    await ForFrame;
}
