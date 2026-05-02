await ForCanvas(640, 480);

int radius = 240;
int delta = radius / 16;

for (int i = 0; i < 16; i++)
{
    Circle(Fill(Cga(i)), (320, 240), radius - delta * i);
}
