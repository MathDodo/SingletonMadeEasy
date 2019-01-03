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

        var goTarget = (targets[0] as GameObject);
        var components = goTarget.GetComponents<MonoBehaviour>();

        for (int i = 0; i < components.Length; i++)
        {
            var mInfo = components[i].GetType().GetMethod("OnInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            if (mInfo != null && mInfo.IsPrivate)
            {
                mInfo.Invoke(components[i], null);
            }

            var type = components[i].GetType().BaseType;

            while (type != typeof(MonoBehaviour))
            {
                mInfo = type.GetMethod("OnInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (mInfo != null && mInfo.IsPrivate)
                {
                    mInfo.Invoke(components[i], null);
                }

                type = type.BaseType;
            }
        }

        var root = goTarget.transform.root;

        var rComponents = root.GetComponents<MonoBehaviour>();

        for (int i = 0; i < rComponents.Length; i++)
        {
            var mInfo = rComponents[i].GetType().GetMethod("OnHierarchyInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            if (mInfo != null && mInfo.IsPrivate)
            {
                mInfo.Invoke(rComponents[i], null);
            }

            var type = rComponents[i].GetType().BaseType;

            while (type != typeof(MonoBehaviour))
            {
                mInfo = type.GetMethod("OnHierarchyInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (mInfo != null && mInfo.IsPrivate)
                {
                    mInfo.Invoke(rComponents[i], null);
                }

                type = type.BaseType;
            }
        }

        foreach (Transform transform in root)
        {
            var tComponents = transform.GetComponents<MonoBehaviour>();

            for (int i = 0; i < tComponents.Length; i++)
            {
                var mInfo = tComponents[i].GetType().GetMethod("OnHierarchyInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (mInfo != null && mInfo.IsPrivate)
                {
                    mInfo.Invoke(tComponents[i], null);
                }

                var type = tComponents[i].GetType().BaseType;

                while (type != typeof(MonoBehaviour))
                {
                    mInfo = type.GetMethod("OnHierarchyInspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                    if (mInfo != null && mInfo.IsPrivate)
                    {
                        mInfo.Invoke(tComponents[i], null);
                    }

                    type = type.BaseType;
                }
            }
        }
    }
}