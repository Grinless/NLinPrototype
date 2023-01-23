using System.Collections.Generic;

public class Alignments
{
    internal List<AlignmentType> _alignments = new List<AlignmentType>(); 

    /// <summary>
    /// Get the list of current Alignments
    /// </summary>
    public List<AlignmentType> GetAlignmentsList
    {
        get { return _alignments; }
    }

    //#region Alignment Value Modifiers. 

    /////// <summary>
    /////// Set values additively to an existing alignment. 
    /////// </summary>
    /////// <param name="title"> The title of the AlignmentType. </param>
    /////// <param name="id"> The ID of the AlignmentType. </param>
    /////// <param name="additiveValue"> The value to add to the alignment type. </param>
    ////public void SetAdditiveValue(string title, int id, float additiveValue)
    ////{
    ////    AlignmentType type = GetAlignmentType(title, id);
    ////    if (type != null)
    ////    {
    ////        type.Value = type.Value + additiveValue;
    ////    }
    ////}

    ///// <summary>
    ///// Set a new value to the alignment type.
    ///// NOTE: Will override the pre-existing value. 
    ///// </summary>
    ///// <param name="title"> The title of the AlignmentType. </param>
    ///// <param name="id"> The ID of the AlignmentType.</param>
    ///// <param name="value"> The value to overrite the pre-existing value with. </param>
    //public void SetNewValue(string title, int id, float value)
    //{
    //    PlayerAlignmentData type = GetAlignmentType(title, id);

    //    if (type != null)
    //    {
    //        type.Value = value;
    //    }
    //}
    //#endregion

    //#region Alignment Type Insertion.

    ///// <summary>
    ///// Add an alignment type. 
    ///// The value of the added type will default to 0, 
    ///// and the range will be set to [-1,1] inclusive.  
    ///// </summary>
    ///// <param name="title"> The title of the alignment type. </param>
    ///// <param name="id"> The ID associated with the alignment type. </param>
    //public void AddAlignmentType(string title, int id)
    //{
    //    AddAlignmentType(title, id, 0);
    //}

    ///// <summary>
    ///// Add an alignment type. 
    ///// The range will be set to [-1,1] inclusive by default.
    ///// </summary>
    ///// <param name="title"> String used to index the alignment type. </param>
    ///// <param name="id"> Integer ID used to index the alignment type. </param>
    ///// <param name="value"> The inital value set to the alignment type. </param>
    //public void AddAlignmentType(string title, int id, float value)
    //{
    //    AddAlignmentType(title, id, value, -1, 1);
    //}

    ///// <summary>
    ///// Add an alignment type. 
    ///// </summary>
    ///// <param name="title"> String used to index the alignment type. </param>
    ///// <param name="id"> Integer ID used to index the alignment type.</param>
    ///// <param name="value"> The inital value set to the alignment type. </param>
    ///// <param name="min"> The minimum range the value can reach. </param>
    ///// <param name="max"> The maximum range the value can reach. </param>
    //public void AddAlignmentType(string title, int id, float value, float min, float max)
    //{
    //    _alignments.Add(new AlignmentType(title, id, value));
    //}
    //#endregion

    //#region Alignment Type Searches. 
    ///// <summary>
    ///// Request a reference to an alignment type. 
    ///// </summary>
    ///// <param name="title"> The title ID of the requested alignment type. </param>
    ///// <returns> A reference to the alignment type or NULL. </returns>
    //public AlignmentType GetAlignmentType(string title)
    //{
    //    foreach (AlignmentType type in _alignments)
    //    {
    //        if (type.Data.CompareTitle(title))
    //        {
    //            return type;
    //        }
    //    }

    //    UnityEngine.Debug.Log(
    //        string.Format("ERROR: Alignment type: (title: {0}) could not be found.",
    //        title));
    //    return null;
    //}

    ///// <summary>
    ///// Request a reference to an alignment type. 
    ///// </summary>
    ///// <param name="id">The integer ID of the requested alignment type.</param>
    ///// <returns>A reference to the alignment type or NULL.</returns>
    //public AlignmentType GetAlignmentType(int id)
    //{
    //    foreach (AlignmentType type in _alignments)
    //    {
    //        if (type.Data.CompareIdentifier(id))
    //            return type;
    //    }

    //    UnityEngine.Debug.Log(
    //        string.Format("ERROR: Alignment type: (id: {1}) could not be found.", id));
    //    return null;
    //}

    ///// <summary>
    ///// Request a reference to an alignment type. 
    ///// </summary>
    ///// <param name="title">The title ID of the requested alignment type.</param>
    ///// <param name="id">The integer ID of the requested alignment type.</param>
    ///// <returns> A reference to the alignment type or NULL. </returns>
    //public AlignmentType GetAlignmentType(string title, int id)
    //{
    //    foreach (AlignmentType type in _alignments)
    //    {
    //        if (type.Data.CompareTitle(title) || type.Data.CompareIdentifier(id))
    //            return type;
    //    }

    //    UnityEngine.Debug.Log(
    //        string.Format("ERROR: Alignment type: (title: {0}, id: {1}) could not be found.",
    //        title, id));
    //    return null;
    //}
    //#endregion
}
