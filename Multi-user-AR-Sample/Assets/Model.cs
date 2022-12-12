using UnityEngine;
namespace Assets
{
    public class Model
    {
        public string name;
        public GameObject gameObject;

        public Model(string name, GameObject gameObject)
        {
            this.name = name;
            this.gameObject = gameObject;
        }
    }
}