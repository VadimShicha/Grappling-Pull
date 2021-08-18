using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarManager : MonoBehaviour
{
    //used to make sure if you go on an old checkpoint the checkpoint position doesn't change
    public static int checkpointNumber;
    public static Vector3 checkpointPos;
    public static bool respawned;
}
