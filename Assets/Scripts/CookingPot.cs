using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public GameObject cookingUi;

    public void OnClickEnter()
    {
        cookingUi.SetActive(true);
    }

    public void OnClickExit()
    {
        cookingUi.SetActive(false);
    }
}
