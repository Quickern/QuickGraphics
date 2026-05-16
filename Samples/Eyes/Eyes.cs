await ForCanvas(640, 480);

Number w = 150, h = 100;
Number x = 320 - w / 2, y = 240 - h / 2;

Number ex = w * 0.23;
Number ey = h * 0.5;
Number lx = x + ex;
Number ly = y + ey;
Number rx = x + w - ex;
Number ry = y + ey;
Number br = Min(ex, ey) * 0.5;

while (IsNotClosed)
{
    Number t = TimeSeconds;

    Number blink = 1.0f - Pow(Sin(t * 0.5), 200.0) * 0.8;

    Clear((76, 76, 76, 255));

    Ellipse(Fill(0, 0, 0, 32), (lx + 3, ly + 16), ex, ey);
    Ellipse(Fill(0, 0, 0, 32), (rx + 3, ry + 16), ex, ey);

    Ellipse(Fill(220, 220, 220), (lx, ly), ex, ey);
    Ellipse(Fill(220, 220, 220), (rx, ry), ex, ey);

    (Number mx, Number my) = Mouse.Position;

    if (Mouse.IsPressed(MouseButton.Left))
        Circle(Fill(Blue), (mx, my), 10);
    else if (Mouse.IsPressed(MouseButton.Right))
        Circle(Fill(Green), (mx, my), 10);
    else
        Circle(Red, (mx, my), 10);

    float dx = (mx - rx) / (ex * 10.0);
    float dy = (my - ry) / (ey * 10.0);
    float d = Sqrt(dx * dx + dy * dy);
    if (d > 1.0)
    {
        dx /= d;
        dy /= d;
    }
    dx *= ex * 0.4f;
    dy *= ey * 0.5f;

    Ellipse(Fill(32, 32, 32), (lx + dx, ly + dy + ey * 0.25 * (1.0 - blink)), br, br * blink);

    dx = (mx - rx) / (ex * 10.0);
    dy = (my - ry) / (ey * 10.0);
    d = Sqrt(dx * dx + dy * dy);
    if (d > 1.0)
    {
        dx /= d;
        dy /= d;
    }
    dx *= ex * 0.4;
    dy *= ey * 0.5;

    Ellipse(Fill(32, 32, 32), (rx + dx, ry + dy + ey * 0.25 * (1.0 - blink)), br, br * blink);

    await ForFrame;
}
