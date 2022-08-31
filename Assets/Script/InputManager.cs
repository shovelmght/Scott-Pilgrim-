using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    virtual public bool WantsMoveRight()
    {
        if (Input.GetKey(KeyCode.D) )
        {
            return true;
        }
        return false;
    }

    //  Move Left / A
    virtual public bool WantsMoveLeft()
    {
        if (Input.GetKey(KeyCode.A) )
        {
            return true;
        }
        return false;
    }
    virtual public bool ShowObjectif()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        return false;
    }

    // Jump / space
    virtual public bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            return true;
        }
        return false;
    }

    //  Shoot projectil / F /Left clic mouse
    virtual public bool ShootProjectil()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F) )
        {
            return true;
        }
        return false;
    }
}
