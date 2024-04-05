using System.Collections.Generic;
using UnityEngine;

public class NumberDisplay : MonoBehaviour {
    public enum Align {
        Left,
        Right
    }

    [Header("Text Properties")]
    public string text;
    public float offset;
    public Align align;
    
    [Header("Assets")]
    public GameObject prefab;
    public Sprite[] sprites;

    private struct Digit {
        public GameObject gameObject;
        public SpriteRenderer render;

        public Digit(Transform parent, GameObject prefab) {
            gameObject = Instantiate(prefab, parent);
            render = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    private const int SEPERATOR_INDEX = 10;
    private List<Digit> digits;

    void Start() {
        digits = new();
        UpdateDisplay();
    }

    void Update() {
        UpdateDisplay();
    }

    void UpdateDisplay() {
        while (digits.Count < text.Length) {
            digits.Add(new(transform, prefab));
        }

        while (digits.Count > text.Length) {
            var last = digits.Count - 1;

            var digit = digits[last];
            Destroy(digit.gameObject);

            digits.RemoveAt(digits.Count - 1);
        }

        var index = 0;
        foreach (var character in text) {
            var digit = digits[index];

            var offsetVector = Vector3.right * index * offset;

            if (align == Align.Left) {
                digit.gameObject.transform.position = transform.position + offsetVector;
            } else {
                digit.gameObject.transform.position = transform.position - offsetVector;
            }

            digit.render.sprite = sprites[getSpriteIndex(character)];

            index += 1;
        }
    }

    int getSpriteIndex(char character)  {
        if (character >= '0' && character <= '9') {
            return character - '0';
        } else {
            return SEPERATOR_INDEX;
        }
    }
}
