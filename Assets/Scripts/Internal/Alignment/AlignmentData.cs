using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AlignmentData
{
    internal string _title; //String Identifier
    internal int _ID; //The integer identifier. 

    /// <summary>
    /// Get the Title (String ID) of the AlignmentType. 
    /// </summary>
    public string Title
    {
        get { return _title; }
    }

    /// <summary>
    /// Get the ID (Integer ID) of the AlignmentType. 
    /// </summary>
    public int ID
    {
        get { return _ID; }
    }

    public AlignmentData(string title, int identifier)
    {
        _title = title;
        _ID = identifier;
    }

    public bool CompareTitle(string title) => (Title == title);
    public bool CompareIdentifier(int identifier) => (ID == identifier);
}

public class PlayerAlignmentData
{
    AlignmentData _data;
    Range _alignmentRange;
    private float _value; //The value assigned to the alignment value.

    /// <summary>
    /// Assign value to the alignment type. 
    /// Values assigned will be bound to the min/max expected values. 
    /// </summary>
    public float Value
    {
        get { return _value; }
        set
        {
            _value = value; //Assign the value. 

            if (_value < _alignmentRange.min) //Check the value is not below the threshold. 
                _value = _alignmentRange.min;

            else if (_value > _alignmentRange.max) //Check the value is not above the threshold. 
                _value = _alignmentRange.max;
        }
    }

    public PlayerAlignmentData(AlignmentData alignmentData, float value, Range alignmentRange)
    {
        this.Value = value;
        this._data = alignmentData;
        this._alignmentRange = alignmentRange;
    }

    public bool CompareTitle(string title) => _data.CompareTitle(title);
    public bool CompareIdentifier(int identifier) => _data.CompareIdentifier(identifier);
}

public class RoomAlignmentData
{
    AlignmentData _alignmentData;
    Range _matchRange;
    Range _thresholdRange;
    AlignmentMatchSetting _matchSetting;

    public RoomAlignmentData(AlignmentData data, Range matchRange, Range thresholdRange, AlignmentMatchSetting matchSetting)
    {
        _alignmentData = data;
        _matchRange = matchRange;
        _thresholdRange = thresholdRange;
        _matchSetting = matchSetting;
    }
}


