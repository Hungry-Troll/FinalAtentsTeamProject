using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManagerEX
{
    // 오디오 믹서 (전체)
    public AudioMixer titleAudioMixer;

    // 오디오 믹서그룹(부분) BGM
    public AudioMixerGroup _bgmAudioMixerGroup;
    // 오디오 믹서그룹(부분) 효과음
    public AudioMixerGroup _sfxAudioMixerGroup;
    // BGM 오디오 소스
    public AudioSource _bgmAudioSource;

    // 효과음 오디오 소스
    public List<AudioSource> _sfxAudioSource;
    public AudioSource audio;

    // 효과음 오디오 소스 들고있을 게임오브젝트
    public List<GameObject> _sfxAudioSourceGameObj;

    // 효과음 오디오소스 클립 숫자 확인용
    int _cnt;

    // UI 옵션창을 예외로 사운드매니저에서 미리 불러오기 때문에 UI 매니저에 넘겨주기 위한 변수 선언
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
        // 오디오 믹서(전체) 가지고 옴
        titleAudioMixer = GameManager.Resource.GetAudioMixer("TitleAudioMixer");

        // 믹서에서 필요한 믹서그룹만 가지고 옴 (BGM)
        AudioMixerGroup[] tempMixerGroupBGM = titleAudioMixer.FindMatchingGroups("BGM");
        _bgmAudioMixerGroup = tempMixerGroupBGM[0];
        // 믹서에서 필요한 믹서그룹만 가지고 옴 (SFX)
        AudioMixerGroup[] tempMixerGroupSFX = titleAudioMixer.FindMatchingGroups("SFX");
        _sfxAudioMixerGroup = tempMixerGroupSFX[0];

        // BGM 오디오 소스 들고있을 빈 오브젝트 생성
        GameObject emptyGameObject = new GameObject("BGMAudioSource");
        // 게임매니저 자식으로 붙여놓음
        emptyGameObject.transform.SetParent(GameManager.Instance.transform);
        // 오디오 소스 컴포넌트를 붙여줌 (BGM용)
        _bgmAudioSource = emptyGameObject.AddComponent<AudioSource>();

        // 효과음 오디오 소스 들고있을 리스트 생성
        _sfxAudioSource = new List<AudioSource>();
        _sfxAudioSourceGameObj = new List<GameObject>();

        // 효과음 오디오 소스 들고있을 빈 오브젝트 생성
        // 오디오 소스를 10개 들고있음
        // 효과음 오디오 소스 클립 순서를 위한 cnt 변수
        _cnt = 0;
        for (int i = 0; i < 11; i++)
        {
            GameObject emptyGo = new GameObject("FPXAudioSource");
            emptyGo.transform.SetParent(GameManager.Instance.transform);
            _sfxAudioSource.Add(emptyGo.AddComponent<AudioSource>());
        }

        // 옵션창 슬라이드 연결을 위한 리소스 로드
        _option = GameManager.Resource.GetUi("TitleCanvas");
        // 재귀함수로 찾음
        Transform bgmSliderTr = FindSlider("BGMSlider", _option.transform);
        // 대입
        BGMSlider = bgmSliderTr.GetComponent<Slider>();
        // 재귀함수로 찾음
        Transform fpxSliderTr = FindSlider("SFXSlider", _option.transform);
        // 대입
        SFXSlider = fpxSliderTr.GetComponent<Slider>();


        PlayLogInt = PlayerPrefs.GetInt(PlayLog);
        // 처음 시작시 사운드 세팅
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
        // 다시 시작시 사운드 세팅 (추후 저장구현 후 확인 필요)
        else
        {
            BGMSliderValue = PlayerPrefs.GetFloat(BGMPref);
            BGMSlider.value = BGMSliderValue;
            SFXSliderValue = PlayerPrefs.GetFloat(SFXPref);
            SFXSlider.value = SFXSliderValue;
        }
        //BGM 재생

        BGMPlay("Low Poly Animated Dinosaurs-320k");

    }

    // BGM 재생 함수
    public void BGMPlay(string name)
    {
        // 널 체크
        if(_bgmAudioSource != null)
        {
            // 오디오 리소스 가지고오고
            AudioClip temAudio = GameManager.Resource.GetAudioSource(name);
            _bgmAudioSource.clip = temAudio;
            // BGM 믹스 그룹으로 설정 이렇게 해야 사운드바가 움직임
            _bgmAudioSource.outputAudioMixerGroup = _bgmAudioMixerGroup;
            // 재생
            _bgmAudioSource.Play();
            _bgmAudioSource.loop = true;
        }
    }

    // SFX 재생 함수
    public void SFXPlay(string name)
    {        
        // 오디오 클립 10개가 꽉차면 0부터 클립 변경
        if (_cnt > 10)
        {
            _cnt = 0;
        }
        // 오디오 리소스 가지고 오고
        AudioClip temAudio = GameManager.Resource.GetAudioSource(name);
        _sfxAudioSource[_cnt].clip = temAudio;
        // BGM 믹스 그룹으로 설정 이렇게 해야 사운드바가 움직임
        _sfxAudioSource[_cnt].outputAudioMixerGroup = _sfxAudioMixerGroup;
        // 재생
        _sfxAudioSource[_cnt].Play();
        // 오디오 클립 저장 숫자
        _cnt++;
    }


    // 재귀 함수
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

    // 마스터 볼륨
    public void MasterControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    // 배경음 볼륨
    public void BGMControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }
    // 효과음 볼륨
    public void SFXControll(float sliderValue)
    {
        titleAudioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    // 사운드 세팅 저장
    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }
    // 급작스럽게 꺼질 경우 사운드 세팅 저장
    void OnApplicationOnFocuse(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSetting();
        }
    }



}
