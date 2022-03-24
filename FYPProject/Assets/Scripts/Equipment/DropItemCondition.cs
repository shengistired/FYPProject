using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropItemCondition
{
    public abstract bool Check(DragAndDrop draggable);
    public abstract bool CheckReturn(ReturnDragDrop draggable);
}
