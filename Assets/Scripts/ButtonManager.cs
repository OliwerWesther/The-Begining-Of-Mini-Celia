using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Base UI")]
    public List<GameObject> Base = new List<GameObject>();

    [Header("Cooking UI")]
    public List<GameObject> CookingEnable = new List<GameObject>();
    public List<GameObject> CookingDisable = new List<GameObject>();

    [Header("People UI")]
    public List<GameObject> PeopleEnable = new List<GameObject>();
    public List<GameObject> PeopleDisable = new List<GameObject>();

    [Header("People UI")]
    public List<GameObject> ChestEnable = new List<GameObject>();
    public List<GameObject> ChestDisable = new List<GameObject>();


    //Door
    public void DoorOCE()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + -1);
    }

    //Cooking
    public void CookingPotOCE()
    {
        SetState(CookingEnable, true);
        SetState(CookingDisable, false);
    }
    public void CookingPotOCD()
    {
        SetState(CookingEnable, false);
        SetState(Base, true);
    }

    //People
    public void PeopleOCE()
    {
        SetState(PeopleEnable, true);
        SetState(PeopleDisable, false);
    }
    public void PeopleOCD()
    {
        SetState(PeopleEnable, false);
        SetState(Base, true);
    }

    public void ChestOCE()
    {
        SetState(ChestEnable, true);
        SetState(ChestDisable, false);
    }
    public void ChestOCD()
    {
        SetState(ChestEnable, false);
        SetState(Base, true);
    }



    private void SetState(List<GameObject> list, bool state)
    {
        list?.ForEach(obj => obj?.SetActive(state));
    } // This helper method is required for SetState(...) calls to work
}
