using UnityEngine;

namespace GeneralSolutions
{
    public static class GeneralMethods
    {
        public static T TryGetComponentValidating<T>(MonoBehaviour searchingOn) where T : MonoBehaviour
        {
            return TryGetComponentValidating<T>(searchingOn.gameObject);
        }

        public static T TryGetComponentValidating<T>(GameObject searchingOn) where T : MonoBehaviour
        {
            if (searchingOn.TryGetComponent<T>(out T component) == true)
            {
                return component;
            }
            else
            {
                Debug.LogError($"Component ''{(typeof(T))}'' doesn't exist on {searchingOn.name}...");
            }

            return null;
        }
    }
}
