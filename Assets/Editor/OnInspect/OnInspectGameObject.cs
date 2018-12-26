using UnityEngine;
using UnityEditor;

/// <summary>
/// This class is used to call the OnInspect method on components of gameobjects
/// </summary>
[CustomPreview(typeof(GameObject))]
public class OnInspectGameObject : ObjectPreview
{
    /// <summary>
    /// This method is called by the unity editor when gameobjects are being previewed
    /// </summary>
    /// <param name="targets">Contains the gameobject which is previewed</param>
    public override void Initialize(Object[] targets)
    {
        base.Initialize(targets);

        if (!Application.isPlaying)
        {
            var components = (targets[0] as GameObject).GetComponents<MonoBehaviour>();

            for (int i = 0; i < components.Length; i++)
            {
                var mInfo = components[i].GetType().GetMethod("OnInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (mInfo != null)
                {
                    mInfo.Invoke(components[i], null);
                }

                var type = components[i].GetType().BaseType;

                while (type != typeof(MonoBehaviour))
                {
                    var pMInfo = type.GetMethod("OnInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                    if (pMInfo != null)
                    {
                        pMInfo.Invoke(components[i], null);
                    }

                    type = type.BaseType;
                }
            }
        }
    }
}