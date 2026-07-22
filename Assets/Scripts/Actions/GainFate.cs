using System.Collections;

public class GainFate : IAction
{
    public int Gain {get; private set;}

    public GainFate(int gain)
    {
        Gain = gain;
    }

    public IEnumerator Execute()
    {
        GameManager.Instance.GainFate(Gain);

        return null;
    }
}
