using System.Collections;

public class GainDoom : IAction
{
    public int Gain {get; private set;}

    public GainDoom(int gain)
    {
        Gain = gain;
    }

    public IEnumerator Execute()
    {
        GameManager.Instance.GainDoom(Gain);

        return null;
    }
}
