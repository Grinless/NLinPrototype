using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class NLinSys_HelperFunctions
{
    public static void PrintChapter(Chapter chapter)
    {
        Debug.Log(
            string.Format(
                "Level: <name: {0}, ID: {1}, type: {2}>", 
                chapter.Title,
                chapter.ID, 
                chapter.Type
                )
            );
    }
}
