using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharaterSelectManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _weaponType;
    [SerializeField] private Transform _characterGridView;
    [SerializeField] private GameObject _characterModels;
    [SerializeField] private CharacterItem _characterItemPrefab;

    private List<Character> _characters;
    private int _selectedCharacterIndex;

    private void Awake()
    {
        _characters = new();
        _selectedCharacterIndex = -1;
        LoadCharaterInfo();
        if (_characters.Count > 0)
            SelectCharacter(0);
        _startButton.onClick.AddListener(EnterGame);
    }
    /*
    private void Update()
    {

    }
    */
    private void LoadCharaterInfo()
    {
        int charlListIndex = 0;
        foreach (Transform child in _characterModels.transform)
        {
            Character cha = child.GetComponent<Character>();
            if (cha != null)
            {
                cha.SetAsStaticModel();
                _characters.Add(cha);
                CharacterItem newAvator = Instantiate(_characterItemPrefab, _characterGridView);
                newAvator.gameObject.SetActive(true);
                newAvator.MainButton.onClick.AddListener(() => SelectCharacter(charlListIndex++));
                newAvator.CharacterAvator.sprite = cha.Define.Avator;
            }
        }
    }

    private void SelectCharacter(int charIndex)
    {
        if(charIndex < 0 || charIndex >= _characters.Count || _selectedCharacterIndex == charIndex)
            return;
        CacheManager.Instance.SetPlayerChar(_characters[charIndex].Define.CharacterId);
        SetPreviewModel(charIndex);
    }

    private void SetPreviewModel(int charIndex)
    {
        if (_selectedCharacterIndex == charIndex)
            return;
        if(_selectedCharacterIndex >= 0 && _selectedCharacterIndex < _characters.Count)
            _characters[_selectedCharacterIndex].gameObject.SetActive(false);
        if (charIndex < 0 || charIndex >= _characters.Count)
        {
            _selectedCharacterIndex = -1;
            return;
        }
        _characters[charIndex].gameObject.SetActive(true);
        _characters[charIndex].Pose();
        _selectedCharacterIndex = charIndex;
        CharacterDefine define = _characters[charIndex].Define;
        _characterName.text = define.CharacterName;
        _weaponType.text = define.WeaponType;
    }

    private void EnterGame()
    {
        LoadManager.Instance.SwitchScene("SampleScene");
    }
}
