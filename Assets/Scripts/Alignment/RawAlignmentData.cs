using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RawAlignmentIdentifiers
{
    public string title;
    public int identifier;
    public float value;
}

public class RawPlayerAlignmentData
{
    RawAlignmentIdentifiers data; 
    Range playerRange;
}

public class RawRoomData
{
    RawAlignmentIdentifiers data;
    Range innerMatchRange;
    Range outerMatchRange;
}

public class RawRoomAlignmentData
{
    RawAlignmentIdentifiers data;
    Range innerMatchRange;
    Range outerMatchRange;
}
