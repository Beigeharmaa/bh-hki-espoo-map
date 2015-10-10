//
// Learning C# by Developing Games with Unity 3D Beginner's Guide [eBook].pdf
// =================================================
// Notes

using UnityEngine;
using System.Collections;

public class LearningScript : MonoBehaviour
{
 
  private GameObject target;

  private void Start()
  {
    target = GameObject.FindWithTag("Player");
    if (target != null)
      Debug.Log (target);
  }

  private void Update()
  {

  }
 
  private void AddTwo(int a, int b)
  {
    Debug.Log(a + b);
  }
  
}
