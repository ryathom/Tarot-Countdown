using System.Collections;

public class GainFate : IAction
{
    public int Fate {get; private set;}

    public GainFate(int num)
    {
        Fate = num;
    }

    public IEnumerator Execute()
    {
        GameManager.Instance.Fate += Fate;

        return null;
    }
}
