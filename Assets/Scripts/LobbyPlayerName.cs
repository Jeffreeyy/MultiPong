using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerName : NetworkBehaviour
{
    [SerializeField]private Text _enteredNameText;
    [SerializeField]private Text _profileNameText;

    private string _playerName;

    public void SetName()
    {
        _playerName = _enteredNameText.text;
        _profileNameText.text = _playerName;
    }

    public string GetName()
    {
        return _playerName;
    }

}
