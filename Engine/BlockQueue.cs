namespace Engine;
using Engine;
using System;

public class BlockQueue
{
    private readonly Block[] Block = new Block[]
    {
        new IBlock(),
        new JBlock(),
        new LBlock(),
        new OBlock(),
        new SBlock(),
        new TBlock(),
        new ZBlock(),
    };

    private readonly Random random = new Random();

    public Block NextBlock { get; private set; }

    public BlockQueue() 
    {
        NextBlock = RandomBlock();
    }

    public Block RandomBlock() 
    {
        return Block[random.Next(Block.Length)];
    }

    public Block GetAndUpdate() 
    {
        Block block = NextBlock;

        do {
            NextBlock = RandomBlock();
        } while(block.Id == NextBlock.Id); 

        return block;
    }
}

