using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BossImageLoader : MonoBehaviour
{
    //MainScene���� ��� �̹����� �ε��ϴ� ��ũ��Ʈ.
    [SerializeField] private Image bossImage;

    [System.Serializable]
    public class Character
    {
        public string name;
        public string sprite_path;
    }

    [System.Serializable]
    public class CharacterEntry
    {
        public string key;
        public Character value;
    }

    [System.Serializable]
    public class CharacterData
    {
        public List<CharacterEntry> characters;
    }

    private void Start()
    {
        string selectedBoss = PlayerPrefs.GetString("SelectedBoss", "male_boss");//male_boss�� �⺻��.
        string path = Path.Combine(Application.streamingAssetsPath, "Character_Data.json");
        string json = File.ReadAllText(path);// ���Ͽ��� �ؽ�Ʈ�� �о� json�� ���� 
        CharacterData data = JsonUtility.FromJson<CharacterData>(json);

        Character selectedCharacter = null;//List���� key�� �˻�
        foreach (var entry in data.characters)
        {
            if (entry.key == selectedBoss)
            {
                selectedCharacter = entry.value;//���õ� ������ key�� �Ͽ� CharacterŸ���� value�� ����.
                break;
            }
        }
        Debug.Log("���õ� ��� : " + selectedCharacter.name);

        if (selectedCharacter == null)
        {
            Debug.LogError("���õ� ��� ������ ã�� �� ����" + selectedBoss);
            return;
        }

        Sprite bossSprite = Resources.Load<Sprite>(selectedCharacter.sprite_path);//Resources���� Sprite�ε�
        if (bossSprite == null)
        {
            Debug.LogError("����� ��������Ʈ �̹����� ã�� �� ����" + selectedCharacter.sprite_path);
            return;
        }
        bossImage.sprite = bossSprite;
    }
}
