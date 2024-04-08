using UnityEngine;

public class Wall : MonoBehaviour {
    public enum PushDirection {
        North,
        South,
        East,
        West
    }

    public PushDirection pushDirection;
}
