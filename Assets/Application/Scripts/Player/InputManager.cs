using UnityEngine;

public static class InputManager
{
    public static float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    public static bool IsMoving(string axisName)
    {
        return Input.GetAxis(axisName) != 0;
    }

    public static bool IsLeftMouseButtonDown()
    {

        if (Input.GetMouseButton(0))
        {
            return true;
        }

        return false;
    }
}
