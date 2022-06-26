using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoChange : MonoBehaviour
{
    public TextMeshProUGUI AmmoText;

    public void ChangeAmmo(int ammo)
    {
        AmmoText.SetText("AMMO: " + ammo);
    }
}
