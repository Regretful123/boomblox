using System;

public struct BallSoundData
{
    public int minHertz;
    public int maxHertz;

    public BallSoundData( int minHertz, int maxHertz )
    {
        this.minHertz = minHertz;
        this.maxHertz = maxHertz;
    }
}