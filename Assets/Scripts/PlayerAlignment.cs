using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class storing player alignment values.
/// </summary>
public class PlayerAlignment : Alignments
{
    /// <summary>
    /// CTOR. 
    /// </summary>
    /// <param name="types"> The types to be set and handled by the instance. </param>
    public PlayerAlignment(List<AlignmentType> types)
    {
        _alignments = types; 
    }
}

public class Alignments
{
    internal List<AlignmentType> _alignments = new List<AlignmentType>(); 

    public List<AlignmentType> GetAlignmentsList
    {
        get { return _alignments; }
    }

    #region Alignment Value Modifiers. 

    /// <summary>
    /// Set values additively to an existing alignment. 
    /// </summary>
    /// <param name="title"> The title of the AlignmentType. </param>
    /// <param name="id"> The ID of the AlignmentType. </param>
    /// <param name="additiveValue"> The value to add to the alignment type. </param>
    public void SetAdditiveValue(string title, int id, float additiveValue)
    {
        AlignmentType type = GetAlignmentType(title, id);
        if (type != null)
        {
            type.Value = type.Value + additiveValue;
        }
    }

    /// <summary>
    /// Set a new value to the alignment type.
    /// NOTE: Will override the pre-existing value. 
    /// </summary>
    /// <param name="title"> The title of the AlignmentType. </param>
    /// <param name="id"> The ID of the AlignmentType.</param>
    /// <param name="value"> The value to overrite the pre-existing value with. </param>
    public void SetNewValue(string title, int id, float value)
    {
        AlignmentType type = GetAlignmentType(title, id);

        if (type != null)
        {
            type.Value = value;
        }
    }
    #endregion

    #region Alignment Type Insertion.

    /// <summary>
    /// Add an alignment type. 
    /// The value of the added type will default to 0, 
    /// and the range will be set to [-1,1] inclusive.  
    /// </summary>
    /// <param name="title"> The title of the alignment type. </param>
    /// <param name="id"> The ID associated with the alignment type. </param>
    public void AddAlignmentType(string title, int id)
    {
        AddAlignmentType(title, id, 0);
    }

    /// <summary>
    /// Add an alignment type. 
    /// The range will be set to [-1,1] inclusive by default.
    /// </summary>
    /// <param name="title"> String used to index the alignment type. </param>
    /// <param name="id"> Integer ID used to index the alignment type. </param>
    /// <param name="value"> The inital value set to the alignment type. </param>
    public void AddAlignmentType(string title, int id, float value)
    {
        AddAlignmentType(title, id, value, -1, 1);
    }

    /// <summary>
    /// Add an alignment type. 
    /// </summary>
    /// <param name="title"> String used to index the alignment type. </param>
    /// <param name="id"> Integer ID used to index the alignment type.</param>
    /// <param name="value"> The inital value set to the alignment type. </param>
    /// <param name="min"> The minimum range the value can reach. </param>
    /// <param name="max"> The maximum range the value can reach. </param>
    public void AddAlignmentType(string title, int id, float value, float min, float max)
    {
        _alignments.Add(new AlignmentType(title, id, value, min, max));
    }
    #endregion

    #region Alignment Type Searches. 
    /// <summary>
    /// Request a reference to an alignment type. 
    /// </summary>
    /// <param name="title"> The title ID of the requested alignment type. </param>
    /// <returns> A reference to the alignment type or NULL. </returns>
    public AlignmentType GetAlignmentType(string title)
    {
        foreach (AlignmentType type in _alignments)
        {
            if (type.Title == title)
            {
                return type;
            }
        }

        UnityEngine.Debug.Log(
            string.Format("ERROR: Alignment type: (title: {0}) could not be found.",
            title));
        return null;
    }

    /// <summary>
    /// Request a reference to an alignment type. 
    /// </summary>
    /// <param name="id">The integer ID of the requested alignment type.</param>
    /// <returns>A reference to the alignment type or NULL.</returns>
    public AlignmentType GetAlignmentType(int id)
    {
        foreach (AlignmentType type in _alignments)
        {
            if (type.ID == id)
            {
                return type;
            }
        }

        UnityEngine.Debug.Log(
            string.Format("ERROR: Alignment type: (id: {1}) could not be found.", id));
        return null;
    }

    /// <summary>
    /// Request a reference to an alignment type. 
    /// </summary>
    /// <param name="title">The title ID of the requested alignment type.</param>
    /// <param name="id">The integer ID of the requested alignment type.</param>
    /// <returns> A reference to the alignment type or NULL. </returns>
    public AlignmentType GetAlignmentType(string title, int id)
    {
        foreach (AlignmentType type in _alignments)
        {
            if (type.Title == title || type.ID == id)
            {
                return type;
            }
        }

        UnityEngine.Debug.Log(
            string.Format("ERROR: Alignment type: (title: {0}, id: {1}) could not be found.",
            title, id));
        return null;
    }
    #endregion
}

public class AlignmentType
{
    private string _title; //String Identifier
    private int _ID; //The integer identifier. 
    private float _value; //The value assigned to the alignment value. 
    private float _minValue; //The minimum value assignable. 
    private float _maxValue; //The maximum value assignable. 

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

    /// <summary>
    /// Assign value to the alignment type. 
    /// Values assigned will be bound to the min/max expected values. 
    /// </summary>
    public float Value
    {
        get { return _value; }
        set {
            _value = value; //Assign the value. 

            if(_value < _minValue) //Check the value is not below the threshold. 
                _value = _minValue;

            else if(_value > _maxValue) //Check the value is not above the threshold. 
                _value = _maxValue;
        }
    }

    /// <summary>
    /// CTOR: 
    /// </summary>
    /// <param name="title"> The string identifier. </param>
    /// <param name="id"> The integer identifier. </param>
    /// <param name="initalValue"> The inital value to set. </param>
    /// <param name="minValue"> The minimum value to constrain the AlignmentType by.</param>
    /// <param name="maxValue"> The maximum value to constrain the AlignmentType by.</param>
    public AlignmentType(string title, int id, float initalValue, float minValue, float maxValue)
    {
        this._title = title;
        this._ID = id;
        this._value = initalValue;
        this._minValue = minValue;
        this._maxValue = maxValue;
    }
}
