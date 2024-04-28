using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquadUI : MonoBehaviour
{
    [SerializeField] TeamPrepUnitUI[] teamPrepUnitUI;
    [SerializeField] PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();   
    }

    public void UpdateUI()
    {
        for(int i = 0; i < teamPrepUnitUI.Length; i++)
        {
            if(i < playerData.squad.Count)
            {
                teamPrepUnitUI[i].unitSO = playerData.squad[i];
            }
            else
            {
                teamPrepUnitUI[i].unitSO = null;
            }
            teamPrepUnitUI[i].UpdateUI();
        }
    }

    public void BeginMission()
    {
        if(playerData.squad.Count > 0)
        {
            SceneManager.LoadScene("ShipGeneration");
        }
    }

    private void OnEnable()
    {
        TeamPrepEvents.instance.onUpdateUI += onUpdateUI;
    }

    private void OnDisable()
    {
        TeamPrepEvents.instance.onUpdateUI -= onUpdateUI;
    }

    private void onUpdateUI(object sender, System.EventArgs e)
    {
        UpdateUI();
    }
}
