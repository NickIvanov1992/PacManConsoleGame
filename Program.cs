using Microsoft.Win32;

Console.CursorVisible = false;
char[,] map = ReadMap("map.txt");
ConsoleKeyInfo keyInfo = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);

Task.Run(() =>
{
    while (true)
    {
        keyInfo = Console.ReadKey();
    }
});

int pacmanX = 1;
int pacmanY = 1;

int score = 0;
while (true)
{
   
    Console.Clear();
    HandleInput(keyInfo, ref pacmanX, ref pacmanY, map, ref score);

    Console.ForegroundColor = ConsoleColor.DarkRed;
    DrawMap(map);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.SetCursorPosition(pacmanX, pacmanY);
    Console.Write("@");
    

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.SetCursorPosition(55, 0);
    Console.Write($"Score:{score}");

    Thread.Sleep(500);


}

static void HandleInput(ConsoleKeyInfo keyInfo, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
{
    int[] direction = GetDirection(keyInfo);
    int nextpacmanPosX = pacmanX + direction[0];
    int nextpacmanPosY = pacmanY + direction[1];
    char nextPos = map[nextpacmanPosX, nextpacmanPosY];

    if (nextPos == ' '  || nextPos == '*')
    {
        pacmanX = nextpacmanPosX;
        pacmanY = nextpacmanPosY;
        if (nextPos == '*')
        {
            score += 1;
            map[nextpacmanPosX, nextpacmanPosY] = ' ';
        }
    }
     //if (keyInfo.Key == ConsoleKey.UpArrow)
    //{
    //    pacmanY -= 1;
    //}
    //else if(keyInfo.Key == ConsoleKey.DownArrow)
    //{
    //    pacmanY += 1;
    //}
    //else if(keyInfo.Key == ConsoleKey.LeftArrow)
    //{
    //    pacmanX -= 1;
    //}
    //else if (keyInfo.Key == ConsoleKey.RightArrow)
    //{
    //    pacmanX += 1;
    //}

}
static int[] GetDirection(ConsoleKeyInfo keyInfo)
{
    int[] direction = { 0, 0 };

    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        direction[1] = -1;
    }
    else if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        direction[1] = 1;
    }
    else if (keyInfo.Key == ConsoleKey.LeftArrow)
    {
        direction[0] = -1;
    }
    else if (keyInfo.Key == ConsoleKey.RightArrow)
    {
        direction[0] = 1;
    }
    return direction;
}

 static char[,] ReadMap (string path)
{
    string[] file = File.ReadAllLines("map.txt");
    char[,] map = new char[GetMaxLengthMap(file), file.Length]; 
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            map[x, y] = file[y][x]; 
        }
       
    }
    return map;
}
 static int GetMaxLengthMap (string[] lines)
{
    int maxLength = lines[0].Length;
    foreach (var line in lines)
        if (line.Length > maxLength)
            maxLength = line.Length;
    return maxLength;
}   // поиск максимальной длины строчки
static void DrawMap(char[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            Console.Write(map[x, y]);
        }
        Console.Write("\n");

    }
}
     