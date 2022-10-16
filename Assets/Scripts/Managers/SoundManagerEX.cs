using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManagerEX
{
    // ����� �ͼ� (��ü)
    public AudioMixer titleAudioMixer;

    // ����� �ͼ��׷�(�κ�) BGM
    public AudioMixerGroup _bgmAudioMixerGroup;
    // ����� �ͼ��׷�(�κ�) ȿ����
    public AudioMixerGroup _sfxAudioMixerGroup;
    // BGM ����� �ҽ�
    public AudioSource _bgmAudioSource;

    // ȿ���� ����� �ҽ�
    public List<AudioSource> _sfxAudioSource;
    public AudioSource audio;

    // ȿ���� ����� �ҽ� ������� ���ӿ�����Ʈ
    public List<GameObject> _sfxAudioSourceGameObj;

    // ȿ���� ������ҽ� Ŭ�� ���� Ȯ�ο�
    int _cnt;

    // UI �ɼ�â�� ���ܷ� ����Ŵ������� �̸� �ҷ����� ������ UI �Ŵ����� �Ѱ��ֱ� ���� ���� ����
    public GameObject _option; 



    private static readonly string PlayLog = "PlayLog";
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";
    private int PlayLogInt;
    public Slider BGMSlider, SFXSlider;
    private float BGMSliderValue, SFXSliderValue;


    // Start is called before the first frame update
    public void Init()
    {
        // ����� �ͼ�(��ü) ������ ��
        titleAudioMixer = GameManager.Resource.GetAudioMixer("TitleAudioMixer");

        // �ͼ����� �ʿ��� �ͼ��׷츸 ������ �� (BGM)
        AudioMixerGroup[] tempMixerGroupBGM = titleAudioMixer.FindMatchingGroups("BGM");
        _bgmAudioMixerGroup = tempMixerGroupBGM[0];
        // �ͼ����� �ʿ��� �ͼ��׷츸 ������ �� (SFX)
        AudioMixerGroup[] tempMixerGroupSFX = titleAudioMixer.FindMatchingGroups("SFX");
        _sfxAudioMixerGroup = tempMixerGroupSFX[0];

        // BGM ����� �ҽ� ������� �� ������Ʈ ����
        GameObject emptyGameObject = new GameObject("BGMAudioSource");
        // ���ӸŴ��� �ڽ����� �ٿ�����
        emptyGameObject.transform.SetParent(GameManager.Instance.transform);
        // ����� �ҽ� ������Ʈ�� �ٿ��� (BGM��)
        _bgmAudioSource = emptyGameObject.AddComponent<AudioSource>();

        // ȿ���� ����� �ҽ� ������� ����Ʈ ����
        _sfxAudioSource = new List<AudioSource>();
        _sfxAudioSourceGameObj = new List<GameObject>();

        // ȿ���� ����� �ҽ� ������� �� ������Ʈ ����
        // ����� �ҽ��� 10�� �������
        // ȿ���� ����� �ҽ� Ŭ�� ������ ���� cnt ����
        _cnt = 0;
        for (int i = 0; i < 11; i++)
        {
            GameObject emptyGo = new GameObject("FPXAudioSource");
            emptyGo.transform.SetParent(GameManager.Instance.transform);
            _sfxAudioSource.Add(emptyGo.AddComponent<AudioSource>());
        }

        // �ɼ�â �����̵� ������ ���� ���ҽ� �ε�
        _option = GameManager.Resource.GetUi("TitleCanvas");
        // ����Լ��� ã��
        Transform bgmSliderTr = FindSlider("BGMSlider", _option.transform);
        // ����
        BGMSlider = bgmSliderTr.GetComponent<Slider>();
        // ����Լ��� ã��
        Transform fpxSliderTr = FindSlider("SFXSlider", _option.transform);
        // ����
        SFXSlider = fpxSliderTr.GetComponent<Slider>();


        PlayLogInt = PlayerPrefs.GetInt(PlayLog);
        // ó�� ���۽� ���� ����
        if (PlayLogInt == 0)
        {
            BGMSliderValue = 1f;
            SFXSliderValue = 1f;
            BGMSlider.value = BGMSliderValue;
            SFXSlider.value = SFXSliderValue;
            PlayerPrefs.SetFloat(BGMPref, BGMSliderValue);
            PlayerPrefs.SetFloat(SFXPref, SFXSliderValue);
            PlayerPrefs.SetInt(PlayLog, -1);
        }
        // �ٽ� ���۽� ���� ���� (���� ���屸�� �� Ȯ�� �ʿ�)
        else
        {
            BGMSliderValue = PlayerPrefs.GetFloat(BGMPref);
            BGMSlider.value = BGMSliderValue;
            SFXSliderValue = PlayerPrefs.GetFloat(SFXPref);
            SFXSlider.value = SFXSliderValue;
        }
        //BGM ���

        BGMPlay("Low Poly Animated Dinosaurs-320k");

    }

    // BGM ��� �Լ�
    public void BGMPlay(string name)
    {
        // �� üũ
        if(_bgmAudioSource != null)
        {
            // ����� ���ҽ� ���������
            AudioClip temAudio = GameManager.Resource.GetAudioSource(name);
            _bgmAudioSource.clip = temAudio;
            // BGM �ͽ� �׷����� ���� �̷��� �ؾ� ����ٰ� ������
            _bgmAudioSource.outputAudioMixerGroup = _bgmAudioMixerGroup;
            // ���
            _bgmAudioSource.Play();
            _bgmAudioSource.loop = true;
        }
    }

    // SFX ��� �Լ�
    public void SFXPlay(string name)
    {        
        // ����� Ŭ�� 10���� ������ 0���� Ŭ�� ����
        if (_cnt > 10)
        {
            _cnt = 0;
        }
        // ����� ���ҽ� ������ ����
        AudioClip temAudio = GameManager.Resource.GetAudioSource(name);
        _sfxAudioSource[_cnt].clip = temAudio;
        // BGM �ͽ� �׷����� ���� �̷��� �ؾ� ����ٰ� ������
        _sfxAudioSource[_cnt].outputAudioMixerGroup = _sfxAudioMixerGroup;
        // ���
        _sfxAudioSource[_cnt].Play();
        // ����� Ŭ�� ���� ����
        _cnt++;
    }


    // ��� �Լ�
    public Transform FindSlider(string name, Transform _tr)
    {
        if(_tr.name == name)

        {
            return _tr;
        }
        for(int i =0; i < _tr.childCount; i++)
        {
            Transform findTr = FindSlider(name, _tr.GetChild(i));
            if (findTr != null)
                return findTr;
        }
        return null;
    }

    // ������ ����
    public void MasterControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    // ����� ����
    public void BGMControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }
    // ȿ���� ����
    public void SFXControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    // ���� ���� ����
    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }
    // ���۽����� ���� ��� ���� ���� ����
    void OnApplicationOnFocuse(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSetting();
        }
    }



}
