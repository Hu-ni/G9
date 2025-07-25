using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace G9.MiniGame.TheStack
{
    public class RubbleSpawner
    {

        public void CreateRubble(GameObject block, Vector3 pos, Vector3 scale, Transform parent = null)
        {
            GameObject go = UnityEngine.Object.Instantiate(block.gameObject);
            go.transform.parent = parent;

            go.transform.localPosition = pos;
            go.transform.localScale = scale;
            go.transform.localRotation = Quaternion.identity;

            go.AddComponent<Rigidbody>();
            go.name = "Rubble";
        }
    }
}
