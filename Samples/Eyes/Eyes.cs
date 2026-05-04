using System.Diagnostics;

await ForCanvas(640, 480);

int w = 150, h = 100;
int x = 320 - w / 2, y = 240 - h / 2;

int mx = 320, my = 240;

Stopwatch sw = Stopwatch.StartNew();
sw.Restart();

int ex = (int)(w * 0.23f);
int ey = (int)(h * 0.5f);
int lx = x + ex;
int ly = y + ey;
int rx = x + w - ex;
int ry = y + ey;
int br = (int)(Math.Min(ex, ey) * 0.5f);

while (IsNotClosed)
{
    float t = (float)sw.Elapsed.TotalSeconds;

    float blink = 1.0f - MathF.Pow(MathF.Sin(t * 0.5f), 200.0f) * 0.8f - 0.000001f;

    Clear((76, 76, 76, 255));

    Ellipse(Fill(0, 0, 0, 32), (lx + 3, ly + 16), ex, ey);
    Ellipse(Fill(0, 0, 0, 32), (rx + 3, ry + 16), ex, ey);

    Ellipse(Fill(220, 220, 220), (lx, ly), ex, ey);
    Ellipse(Fill(220, 220, 220), (rx, ry), ex, ey);

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

    Ellipse(Fill(32, 32, 32), ((int)(lx + dx), (int)(ly + dy + ey * 0.25f * (1.0f - blink))), (int)br, (int)(br * blink));

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

    Ellipse(Fill(32, 32, 32), ((int)(rx + dx), (int)(ry + dy + ey * 0.25f * (1.0f - blink))), (int)br, (int)(br * blink));

    await ForFrame;
}
