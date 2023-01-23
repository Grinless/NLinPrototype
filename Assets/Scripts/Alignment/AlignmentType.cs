
public class AlignmentType
{
    AlignmentData _data;

    public AlignmentData Data
    {
        get { return _data; }
    }

    public AlignmentType(string title, int identifier, float value)
    {
        _data = new AlignmentData(title, identifier);
    }

}

public enum AlignmentMatchSetting
{
    INCLUDED, 
    EXCLUDED
}

public class RoomAlignmentType
{
    public RoomAlignmentType(AlignmentData data, Range match, Range acceptedDeviance, AlignmentMatchSetting matchSetting)
    {

    }
}

public class PlayerAlignmentType
{

    PlayerAlignmentData _data;

    /// <summary>
    /// Assign value to the alignment type. 
    /// Values assigned will be bound to the min/max expected values. 
    /// </summary>
    public float Value
    {
        get { return _data.Value; }
        set
        {
            _data.Value = value;
        }
    }

    public PlayerAlignmentType(AlignmentData data, Range range)
    {
        _data = new PlayerAlignmentData(data, 0, range);
    }
}