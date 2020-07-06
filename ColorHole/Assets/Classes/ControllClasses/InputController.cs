using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector2 fingerMovementDir;

    void Update()
    {
       if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                fingerMovementDir = Vector2.ClampMagnitude(Input.GetTouch(0).deltaPosition, 1);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                fingerMovementDir = Vector2.zero;
            }

            EventManager.getInstance().playerEvents.onPlayerInput.Invoke(new Vector3(-fingerMovementDir.y, 0, fingerMovementDir.x));
        }
    }
}
