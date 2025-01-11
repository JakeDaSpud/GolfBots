using UnityEngine;

namespace GD
{
    //See the following video tutorials before using this class in your code
    //Singletons & Managers in Unity - https://www.youtube.com/watch?v=92NQVeFiDeY
    //Everything You Need to Know About Singletons in Unity - https://www.youtube.com/watch?v=mpM0C6quQjs
    //How To Access Variables From Another Script In Unity - https://www.youtube.com/watch?v=-kd68uKt4jk
    //See C# Generics - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/

    /// <summary>
    /// Parent class for all singletons (SelectionManager, InventoryManager, GameManager)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <see cref="TimeTickSystem"/>
    /// <see cref="https://refactoring.guru/design-patterns/singleton"/>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null) {
                    instance = FindFirstObjectByType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();              //create an object to be added to the scene
                        obj.name = typeof(T).ToString();                //give it a name based on class name
                        obj.hideFlags = HideFlags.HideAndDontSave;      //uncomment to hide in Hierarchy window and dont save across scenes
                        instance = obj.AddComponent<T>();               //add the singleton to the scene object
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            // Ensure only one instance exists
            if (instance == null)
            {
                instance = this as T;
                // Optionally, keep this object across scenes
                DontDestroyOnLoad(gameObject);
            }

            else if (instance != this) {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}