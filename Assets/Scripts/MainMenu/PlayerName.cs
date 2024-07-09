using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    //public TMP_Text playerName = InputField.Name.text;
    public TextMeshProUGUI playerName;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
