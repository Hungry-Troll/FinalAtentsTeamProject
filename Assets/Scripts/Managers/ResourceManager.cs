using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ResourceManager 
{
    public List<GameObject> _player;
    public List<GameObject> _monster;
    public List<GameObject> _pet;
    public List<GameObject> _npc;
    public List<GameObject> _fieldItem;
    public List<GameObject> _ui;
    public List<GameObject> _camera;
    public List<Sprite> _itemImage;
    public List<AudioClip> _audioClip;
    public List<AudioMixer> _audioMixer;

    // Start is called before the first frame update
    public void Init()
    {
        _player = new List<GameObject>();
        _monster = new List<GameObject>();
        _pet = new List<GameObject>();
        _npc = new List<GameObject>();
        _fieldItem = new List<GameObject>();
        _ui = new List<GameObject>();
        _camera = new List<GameObject>();
        _itemImage = new List<Sprite>();
        _audioClip = new List<AudioClip>();
        _audioMixer = new List<AudioMixer>();

        GameObject[] player = Resources.LoadAll<GameObject>("Prefabs/Character_Prefab/");
        GameObject[] monster = Resources.LoadAll<GameObject>("Prefabs/Monster_Prefab/");
        GameObject[] pet = Resources.LoadAll<GameObject>("Prefabs/Pet_Prefab/");
        GameObject[] npc = Resources.LoadAll<GameObject>("Prefabs/Npc_Prefab/");
        GameObject[] fieldItem = Resources.LoadAll<GameObject>("Prefabs/Item_Prefab/");
        GameObject[] ui = Resources.LoadAll<GameObject>("Prefabs/Ui_Prefab/");
        GameObject[] camera = Resources.LoadAll<GameObject>("Prefabs/Camera_Prefab/");
        Sprite[] itemImage = Resources.LoadAll<Sprite>("Resource/Image/ItemImage");
        AudioClip[] audioClip = Resources.LoadAll<AudioClip>("Resource/Sound");
        AudioMixer[] audioMixer = Resources.LoadAll<AudioMixer>("Resource/Sound");

        ListAdd(_player, player);
        ListAdd(_monster, monster);
        ListAdd(_pet, pet);
        ListAdd(_npc, npc);
        ListAdd(_fieldItem, fieldItem);
        ListAdd(_ui, ui);
        ListAdd(_camera, camera);
        ListAddImage(_itemImage, itemImage);
        ListAddAudio(_audioClip, audioClip);
        ListAddAudioMixer(_audioMixer, audioMixer);
    }

    public void ListAdd(List<GameObject> go, GameObject[] loadList)
    {
        foreach(GameObject one in loadList)
        {
            go.Add(one);
        }
    }
    public void ListAddImage(List<Sprite> images, Sprite[] loadListImage)
    {
        foreach (Sprite one in loadListImage)
        {
            images.Add(one);
        }
    }
    public void ListAddAudio(List<AudioClip> audioSource, AudioClip[] loadListAudio)
    {
        foreach (AudioClip one in loadListAudio)
        {
            audioSource.Add(one);
        }
    }
    public void ListAddAudioMixer(List<AudioMixer> audioMixer, AudioMixer[] loadAudioMixer)
    {
        foreach (AudioMixer one in loadAudioMixer)
        {
            audioMixer.Add(one);
        }
    }
    public GameObject GetCharacter(string playerName)
    {
        foreach(GameObject one in _player)
        {
            if (one.name.Equals(playerName))
            {
                return one;
            }
        }
        return null;
    }
    public GameObject GetMonster(string monsterName)
    {
        foreach(GameObject one in _monster)
        {
            if (one.name.Equals(monsterName))
            {
                return one;
            }
        }
        return null;
    }
    public GameObject GetPet(string petName)
    {
        foreach(GameObject one in _pet)
        {
            if (one.name.Equals(petName))
            {
                return one;
            }
        }
        return null;
    }
    public GameObject GetNpc(string npcName)
    {
        foreach(GameObject one in _npc)
        {
            if (one.name.Equals(npcName))
            {
                return one;
            }
        }
        return null;
    }
    public GameObject GetfieldItem(string fieldItemName)
    {
        foreach (GameObject one in _fieldItem)
        {
            if (one.name.Equals(fieldItemName))
            {
                return one;
            }
        }
        return null;
    }
    public GameObject GetUi(string uiName)
    {
        foreach(GameObject one in _ui)
        {
            if (one.name.Equals(uiName))
            {
                return one;
            }
        }
        return null;
    }
    public GameObject GetCamera(string cameraName)
    {
        foreach (GameObject one in _camera)
        {
            if (one.name.Equals(cameraName))
            {
                return one;
            }
        }
        return null;
    }
    public Sprite GetImage(string imageName)
    {
        foreach (Sprite one in _itemImage)
        {
            if (one.name.Equals(imageName))
            {
                return one;
            }
        }
        return null;
    }

    public AudioClip GetAudioSource(string audioClip)
    {
        foreach (AudioClip one in _audioClip)
        {
            if (one.name.Equals(audioClip))
            {
                return one;
            }
        }
        return null;
    }

    public AudioMixer GetAudioMixer(string audioMixer)
    {
        foreach (AudioMixer one in _audioMixer)
        {
            if (one.name.Equals(audioMixer))
            {
                return one;
            }
        }
        return null;
    }

}
