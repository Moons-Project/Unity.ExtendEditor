using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotater : MonoBehaviour {
  // Update is called once per frame
  void Update() {
    transform.Rotate(new Vector3(15, 15, 15) * Time.deltaTime * 3);
  }
}