using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class SurrogateToSave : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Transform[] transform = (Transform[])obj;
        info.AddValue("transform", transform);
        
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Transform []transform = (Transform [])obj;

        transform = (Transform[])info.GetValue("transform", typeof(Transform[]));
        
        obj = transform;
        return obj;
    }
}
