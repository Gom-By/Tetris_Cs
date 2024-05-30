namespace Window;
using Engine;

public partial class Form1 : Form
{
    private readonly string[] tileImagesPath = new string[] {
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileEmpty.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileCyan.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileBlue.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileOrange.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileYellow.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileGreen.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TilePurple.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\TileRed.png"
    };

    private readonly string[] blockImagesPath = new string[] {
         @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-Empty.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-I.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-J.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-L.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-O.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-S.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-T.png",
        @"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Block-Z.png"
    };
    
    private PictureBox[,] gameCanvas;

    private GamesState gamesState = new GamesState();

    public Form1()
    {
        Size = new Size(600, 800);
        BackgroundImage = Image.FromFile(@"C:\Users\nahar\Desktop\code\C_variants\Tetris_Cs\Asset\Background.png");
        
        // Add a panel to the form
        Panel drawingPanel = new Panel
        {
            Dock = DockStyle.None,
            BackColor = Color.Black,

            Size = new Size(500, 700),
            Location = new Point((this.ClientSize.Width - 500) / 2, (this.ClientSize.Height - 700) / 2)
        };

        gameCanvas = SetupGameCanvas(gamesState.GameGrid);

        foreach (PictureBox item in gameCanvas)
        {
            drawingPanel.Controls.Add(item);
        }
        
        KeyDown += Form1_KeyDown;
        Load += GameCanvas_Loader;

        Controls.Add(drawingPanel);

        InitializeComponent();
    }

    private PictureBox[,] SetupGameCanvas(GameGrid grid)
    {
        int cellSize = 30;
        PictureBox[,] canvas = new PictureBox[grid.Rows, grid.Columns];

        for (int row = 0; row < grid.Rows; row++)
        {
            for (int col = 0; col < grid.Columns; col++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(cellSize, cellSize);
                pictureBox.Location = new Point((this.ClientSize.Width-500) / 2 + col * cellSize, (this.ClientSize.Height - 700) / 2 +row * cellSize);

                pictureBox.BackColor = Color.White;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                canvas[row, col] = pictureBox;
            }
        }

        return canvas;
    }

    private void DrawGrid(GameGrid grid) 
    {
        for (int i = 0; i < gamesState.GameGrid.Rows; i++)
        {
            for (int j = 0; j < gamesState.GameGrid.Columns; j++)
            {
                int id = grid[i, j];
                gameCanvas[i, j].Image = Image.FromFile(tileImagesPath[id]);
            }
        }
    }

    private void DrawBlock(Block block) 
    {
        foreach (Position p in block.TilePositions())
        {
            gameCanvas[p.Row, p.Column].Image = Image.FromFile(tileImagesPath[block.Id]);
        }
    }

    private void Draw(GamesState gamesState)
    {
        DrawGrid(gamesState.GameGrid);
        DrawBlock(gamesState.CurrentBlock);
    }

    private async void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if(gamesState.GameOver) return;

        switch (e.KeyCode)
        {
            case Keys.Left:
                gamesState.MoveBlockLeft();
                break;
            case Keys.Right:
                gamesState.MoveBlockRight();
                break;
            case Keys.Down:
                gamesState.MoveBlockDown();
                break;
            case Keys.Up:
                gamesState.RotateBlockCW();
                break;
            case Keys.E:
                gamesState.RotateBlockCCW();
                break;
            default:
                return;
            // Add more cases as needed
        }
        Draw(gamesState);
    }

    private async Task GameLoop() 
    {
        Draw(gamesState);
         
        while (!gamesState.GameOver)
        {
            await Task.Delay(500);
            gamesState.MoveBlockDown();
            Draw(gamesState);
        }
    } 

    private async void GameCanvas_Loader(object sender, EventArgs e)
    {
        await GameLoop();
    }

}
