using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void InitaliseInteraction(); 
}

//Prop
//--
//--
//Responsibilities
//-- Handles interaction.
//-- Contains an alignment effect.  

public class Prop : IInteractable
{
    [SerializeField] private PropData _data = new PropData();

    void IInteractable.InitaliseInteraction()
    {
        throw new System.NotImplementedException();
    }
}

//PropData
//--
//--
//Responsibilities
//-- Contains stats affected by this prop. 
//-- Contains a string of dialogue to be shown. 

public class PropData
{
    private List<AlignmentType> _alignmentTypes = new List<AlignmentType>();
}
