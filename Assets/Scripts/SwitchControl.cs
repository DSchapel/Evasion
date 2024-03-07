using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    private Switchable switchable;

    // Start is called before the first frame update
    void Start() {
        switchable = GetComponent<Switchable>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            switch (switchable.side) {
                case Switchable.Side.Foreground:
                    switchable.side = Switchable.Side.Background;
                    break;
                case Switchable.Side.Background:
                    switchable.side = Switchable.Side.Foreground;
                    break;
            }
        }
    }
}
