using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swappable : MonoBehaviour
{
    int isCol;
    public Vector2 col;
    public LayerMask layer;
    Collider2D[] results;
    public Transform circle;
    public bool popup;
    private bool playerHit;

    [SerializeField] private KeyCode swapKey;
    [SerializeField] public ASwapData data;
    public UpgradesDrop upgradesDrop;
    void Start()
    {
        results = new Collider2D[20];
        popup = false;
    }

    void Update()
    {
        col.x = circle.position.x; col.y = circle.position.y;
        isCol = Physics2D.OverlapCircleNonAlloc(col, 0.35f, results, layer);
        if (isCol > 0)
        {
            for(int i = 0; i < isCol; i++)
            {
                GameObject hit = results[i].gameObject;
                if (hit.tag == ("Player")) 
                {
                    playerHit = true;
                    popup = true;
                    if (Input.GetKeyDown(swapKey))
                    {
                        AbilityDatabase abilities = hit.GetComponent<AbilityDatabase>();
                        abilities.AbilitySwitch(data.abilityNum);
                        upgradesDrop.delete();
                    }
                }
            }
            if (!playerHit)
            {
                popup = false;
            }
        }
        playerHit = false;
    }
}
