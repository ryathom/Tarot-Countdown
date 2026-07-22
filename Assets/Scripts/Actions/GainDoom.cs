using System.Collections;

public class GainDoom : IAction
{
    public int Doom {get; private set;}

    public GainDoom(int num)
    {
        Doom = num;
    }

    public IEnumerator Execute()
    {
        GameManager.Instance.Doom += Doom;

        return null;
    }
}
