using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupPicker : MonoBehaviour
{
    public Button[] buttons;
    public Vector2 leftButtonPos;
    public Vector2 rightButtonPos;
    private int powerupsLeft = 0;
    private bool isShowingPowerups = false;

    public void OnLevelUp()
    {
        powerupsLeft += 1;

        if (isShowingPowerups == false)
        {
            SetupButtons();
        }
    }

    private void SetupButtons()
    {
        isShowingPowerups = true;

        List<int> indexes = new List<int>();
        for (int i = 0; i < 5; i++)
            indexes.Add(i);
        int[] chosenIndexes = new int[2];

        for (int i = 0; i < 2; i++)
        {
            int chosenIndexIndex = Random.Range(0, indexes.Count - 1);
            chosenIndexes[i] = indexes[chosenIndexIndex];
            indexes.RemoveAt(chosenIndexIndex);
        }

        SetupButton(chosenIndexes[0], 0);
        SetupButton(chosenIndexes[1], 1);
    }

    private void SetupButton(int buttonIndex, int locationID)
    { 
        Button button = buttons[buttonIndex];
        button.gameObject.SetActive(true);
        switch (locationID)
        {
            case 0:
                button.gameObject.transform.localPosition = leftButtonPos;
                break;
            case 1:
                button.gameObject.transform.localPosition = rightButtonPos;
                break;
        }
    }

    public void ResetButtons()
    {
        powerupsLeft -= 1;

        isShowingPowerups = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if (powerupsLeft > 0)
        {
            SetupButtons();
        }
    }

    public void UpgradeMS()
    {
        Upgrade(Powerup.MovementSpeed);
    }

    public void UpgradeAS()
    {
        Upgrade(Powerup.AttackSpeed);
    }

    public void UpgradeDSC()
    {
        Upgrade(Powerup.DoubleShotChance);
    }

    public void UpgradeSC()
    {
        Upgrade(Powerup.SkillCooldown);
    }

    public void UpgradeRS()
    {
        Upgrade(Powerup.RegenSpeed);
    }

    public void Upgrade(Powerup powerup)
    {
        GameObject player = GameObject.FindGameObjectWithTag("RangePlayer");
        player.GetComponent<RPlayer>().Upgrade(powerup);
    }
}
