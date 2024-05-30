namespace Engine;
using Engine;

public abstract class Block
{
    protected abstract Position[][] Tiles { get; }
    protected abstract Position StartOffset { get; }
    public abstract int Id { get; }
    private int rotationState;
    private Position offset;

    public Block()
    {
        offset = new Position(StartOffset.Row, StartOffset.Column);
    }

    public IEnumerable<Position> TilePositions()
    {
        foreach (Position p in Tiles[rotationState])
        {
            yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
        }
    }

    public void RotateCW()
    {
        rotationState = (rotationState + 1) % Tiles.Length;  // Fix the typo here
    }

    public void RotateCCW()
    {
        if (rotationState == 0)
        {
            rotationState = Tiles.Length - 1;
        }
        else
        {
            rotationState--;
        }
    }

    public void Move(int rows, int columns)
    {
        offset.Row += rows;
        offset.Column += columns;
    }

    public void Reset()
    {
        rotationState = 0;
        offset.Row = StartOffset.Row;
        offset.Column = StartOffset.Column;
    }
}
public class IBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(1, 0), new(1, 1), new(1, 2), new(1, 3) },
        new Position[] { new(0, 2), new(1, 2), new(2, 2), new(3, 2) },  
        new Position[] { new(2, 0), new(2, 1), new(2, 2), new(2, 3) },  
        new Position[] { new(0, 1), new(1, 1), new(2, 1), new(3, 1) },  
    };

    public override int Id => 1;
    protected override Position StartOffset => new Position(-1, 3);
    protected override Position[][] Tiles => tiles;
}

public class OBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1) },
    };

    public override int Id => 4;
    protected override Position StartOffset => new Position(0, 4);
    protected override Position[][] Tiles => tiles;
}

public class JBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(0, 0), new(1, 0), new(1, 1), new(1, 2) },
        new Position[] { new(0, 1), new(0, 2), new(1, 1), new(2, 1) },
        new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 2) },
        new Position[] { new(0, 1), new(1, 1), new(2, 0), new(2, 1) },
    };

    public override int Id => 2;
    protected override Position StartOffset => new Position(0, 3);
    protected override Position[][] Tiles => tiles;
}

public class LBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(0, 2), new(1, 0), new(1, 1), new(1, 2) },
        new Position[] { new(0, 1), new(1, 1), new(2, 1), new(2, 2) },
        new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 0) },
        new Position[] { new(0, 0), new(0, 1), new(1, 1), new(2, 1) },
    };

    public override int Id => 3;
    protected override Position StartOffset => new Position(0, 3);
    protected override Position[][] Tiles => tiles;
}

public class SBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(0, 1), new(0, 2), new(1, 0), new(1, 1) },
        new Position[] { new(0, 1), new(1, 1), new(1, 2), new(2, 2) },
        new Position[] { new(1, 1), new(1, 2), new(2, 0), new(2, 1) },
        new Position[] { new(0, 0), new(1, 0), new(1, 1), new(2, 1) },
    };

    public override int Id => 5;
    protected override Position StartOffset => new Position(0, 3);
    protected override Position[][] Tiles => tiles;
}

public class TBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(0, 1), new(1, 0), new(1, 1), new(1, 2) },
        new Position[] { new(0, 1), new(1, 1), new(1, 2), new(2, 1) },
        new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 1) },
        new Position[] { new(0, 1), new(1, 0), new(1, 1), new(2, 1) },
    };

    public override int Id => 6;
    protected override Position StartOffset => new Position(0, 3);
    protected override Position[][] Tiles => tiles;
}

public class ZBlock : Block
{
    private readonly Position[][] tiles = new Position[][]
    {
        new Position[] { new(0, 0), new(0, 1), new(1, 1), new(1, 2) },
        new Position[] { new(0, 2), new(1, 1), new(1, 2), new(2, 1) },
        new Position[] { new(1, 0), new(1, 1), new(2, 1), new(2, 2) },
        new Position[] { new(0, 1), new(1, 0), new(1, 1), new(2, 0) },
    };

    public override int Id => 7;
    protected override Position StartOffset => new Position(0, 3);
    protected override Position[][] Tiles => tiles;
}
