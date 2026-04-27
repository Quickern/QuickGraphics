Canvas(640, 480);

Vector velocity = (1, 2);
Point position = (320, 240);

while (!IsClosed)
{
    ClearCanvas();

    Rectangle(Red, position, (20, 20));

    if (position.X <= 0 || position.X + 20 >= CanvasWidth)
        velocity.X = -velocity.X;
    if (position.Y <= 0 || position.Y + 20 >= CanvasHeight)
        velocity.Y = -velocity.Y;

    position.X += velocity.X;
    position.Y += velocity.Y;

    await ForFrame;
}
