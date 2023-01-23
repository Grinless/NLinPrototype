using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class storing player alignment values.
/// </summary>
public class PlayerAlignment 
{
    private List<PlayerAlignmentType> _alignments;

    /// <summary>
    /// CTOR. 
    /// </summary>
    /// <param name="types"> The types to be set and handled by the instance. </param>
    public PlayerAlignment(List<PlayerAlignmentType> types)
    {
        _alignments = types; 
    }
}
