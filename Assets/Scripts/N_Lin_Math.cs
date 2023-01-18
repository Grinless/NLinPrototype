using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class N_Lin_Math
{

    public static int GetStartRand(List<Chapter> chapters)
    {
        if (CanStartRand(chapters))
        {
            int unityRand = UnityEngine.Random.Range(0, chapters.Count);
            return unityRand;
        }
        else
        {
            return 0;
        }
    }

    private static bool CanStartRand(List<Chapter> chapters)
    {
        if (chapters.Count > 0)
        {
            return true;
        }
        return false;
    }
}
