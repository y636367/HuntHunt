using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;//����� �� �̸�
    public AudioClip clip; //��
}
public class soundManager : MonoBehaviour
{
    #region �̱���
    static public soundManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion Singleton

    public AudioSource[] audioSourcesEffect;                                    // ����� ����� �ҽ�
    public AudioSource[] audioSourceBgm;

    public string[] playSoundName;                                              // ������� ����� �̸�

    public Sound[] EffectSound;                                                 // ����� ����� �̸� ���
    public Sound[] BgmSound;

    public AudioMixer audioMixer;                                               // ����� �ͼ�
    
    public Slider BGMSlider;                                                    // ����� �� ������ ���� Slider
    public Slider SFXSlider;                                          

    float bgm_value = 0.55f;                                                    // ����� Slider �⺻ ��
    float sfx_value = 0.55f;

    public float prv_bgm_value = -1.0f;                                            // �� �ű�� �� ������ Slider �� ������ ����
    public float prv_sfx_value = -1.0f;

    public bool TheFirst = true;
    public bool now_Set_possible = false;
    void Start()
    {
        playSoundName = new string[audioSourcesEffect.Length];                  // ������� ����� Ȯ���� ���� string �迭 �Ҵ�
    }
    void LateUpdate()
    {
        if (now_Set_possible)                                                   // ����� �� ���� ���� ���� Ȯ��
        {
            SetBgm();
            SetSfx();
        }
    }
    #region SoundEffect
    /// <summary>
    /// string ���� �޾Ƽ� �̸� ������Ʈ�� ������ ����� Ŭ���� �̸��� �����ϴٸ� ���
    /// </summary>
    /// <param name="name"></param>
    public void PlaySoundEffect(string name)                                            
    {
        for (int i = 0; i < EffectSound.Length; i++)
        {
            if (name == EffectSound[i].name)
            {
                for (int j = 0; j < audioSourcesEffect.Length; j++)
                {
                    if (!audioSourcesEffect[j].isPlaying)
                    {
                        playSoundName[j] = EffectSound[i].name;
                        audioSourcesEffect[j].clip = EffectSound[i].clip;
                        audioSourcesEffect[j].Play();
                        return;
                    }
                }
                return;
            }
        }
    }
    /// <summary>
    /// ���� ����ǰ� �ִ� ��� ȿ���� ����
    /// </summary>
    public void StopAllSoundEffect()
    {
        for (int i = 0; i < audioSourcesEffect.Length; i++)
        {
            audioSourcesEffect[i].Stop();
        }
    }
    /// <summary>
    /// string ���� �޾� Ư���� �ϳ��� ȿ���� ����
    /// </summary>
    /// <param name="name"></param>
    public void StopSoundEffect(string name)
    {
        for (int i = 0; i < audioSourcesEffect.Length; i++)
        {
            if (playSoundName[i] == name)
            {
                audioSourcesEffect[i].Stop();
                return;
            }
        }
    }
    #endregion SoundEffect
    #region Bgm
    /// <summary>
    /// ȿ������ ������� ����
    /// </summary>
    /// <param name="name"></param>
    public void PlaySoundBGM(string name)
    {
        for (int i = 0; i < BgmSound.Length; i++)
        {
            if (name == BgmSound[i].name)
            {
                for (int j = 0; j < audioSourceBgm.Length; j++)
                {
                    if (!audioSourceBgm[j].isPlaying)
                    {
                        playSoundName[j] = BgmSound[i].name;
                        audioSourceBgm[j].clip = BgmSound[i].clip;
                        audioSourceBgm[j].Play();
                        audioSourceBgm[j].loop = true;                                      // �ݺ���� üũ
                        return;
                    }
                }
                return;
            }
        }
    }
    public void StopAllSoundBGM()
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            audioSourceBgm[i].Stop();
        }
    }
    public void StopSoundBGM(string name)
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            if (playSoundName[i] == name)
            {
                audioSourceBgm[i].Stop();
                return;
            }
        }
    }
    #endregion Bgm

    /// <summary>
    /// Slider�� �α� ���� ���� �����Ͽ� �ش� ����� �ͼ� ����
    /// </summary>
    public void SetBgm()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
    }
    public void SetSfx()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);
    }
    /// <summary>
    /// �ش� ������ ����� �ͼ� �����ϴ� Slider�� ã�� Slider�� ����� �ͼ��� ����, ������ ������ �� �����ϰ� �ݿ�
    /// </summary>
    public void FInd_slider()
    {
        SetFunction_UI_InGame();
        SetValue_UI();
    }
    #region First_Set_Sound_Slder
    /// <summary>
    /// ���� Slider �Ҵ��
    /// </summary>
    public void SetFunction_UI()
    {
        BGMSlider.onValueChanged.AddListener(Function_Slider_BGM);                          //BGM �����̴��� Function_Slider_BGM �̺�Ʈ �߰�
        SFXSlider.onValueChanged.AddListener(Function_Slider_SFX);                          //SFX �����̴��� Function_Slider_SFX �̺�Ʈ �߰�

        ResetFunction_UI();
    }
    private void SetFunction_UI_InGame()
    {
        BGMSlider.onValueChanged.AddListener(Function_Slider_BGM);
        SFXSlider.onValueChanged.AddListener(Function_Slider_SFX);
    }
    /// <summary>
    /// �Է¹���(�� ���� ���ο��� �����̴��� ����� ��)�� Slider�� �ݿ��ϱ� ���� �Լ�
    /// </summary>
    /// <param name="_value"></param>
    private void Function_Slider_BGM(float _value)
    {
        bgm_value = _value;
    }
    private void Function_Slider_SFX(float _value)
    {
        sfx_value = _value;
    }
    /// <summary>
    /// ����� �ͼ��� ������ Slider �⺻ ������ ����, ����� �ͼ� ���� �⺻ ������ ����
    /// </summary>
    private void ResetFunction_UI()
    {
        BGMSlider.maxValue = 1.0f;
        SFXSlider.maxValue = 1.0f;

        BGMSlider.minValue = 0.0001f;
        SFXSlider.minValue = 0.0001f;

        BGMSlider.value = 0.55f;
        SFXSlider.value = 0.55f;

        BGMSlider.wholeNumbers = false;                                                     // ���� �� ���� Ǯ��
        SFXSlider.wholeNumbers = false;

        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);

        now_Set_possible = true;
        TheFirst = false;
    }
    #endregion
    private void SetValue_UI()
    {
        BGMSlider.maxValue = 1.0f;
        SFXSlider.maxValue = 1.0f;

        BGMSlider.minValue = 0.0001f;
        SFXSlider.minValue = 0.0001f;
        //�� �缳�� �ʿ���

        if (prv_bgm_value == -1.0f)
            BGMSlider.value = 0.55f;
        else
            BGMSlider.value = prv_bgm_value;
        if (prv_sfx_value == -1.0f)
            SFXSlider.value = 0.55f;
        else
            SFXSlider.value = prv_sfx_value;

        BGMSlider.wholeNumbers = false;
        SFXSlider.wholeNumbers = false;

        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);

        now_Set_possible = true;
    }
    /// <summary>
    /// ���ο� ���� ���۵ɶ����� �ش� ���� Slider ���� �޾� �ݿ�
    /// </summary>
    /// <param name="t_BGM"></param>
    /// <param name="t_SFX"></param>
    public void GetSliders(Slider t_BGM, Slider t_SFX)
    {
        BGMSlider = t_BGM;
        SFXSlider = t_SFX;

        if (TheFirst)
            SetFunction_UI();
        else
            FInd_slider();
    }
    /// <summary>
    /// ������� BGM�� FadeOut �Ͽ� ��ȯ ��� �߰�
    /// </summary>
    public void Sounds_BGM_Fade_Out()
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            StartCoroutine(SBF(audioSourceBgm[i]));
        }
    }
    public IEnumerator SBF(AudioSource AS)
    {
        while (AS.volume >0f)
        {
            AS.volume-=Time.deltaTime*0.9f;
            yield return null;
        }
    }
    /// <summary>
    /// ���ο� ���� ���۵ɶ����� Fade_Out �ߴ� ����� �ҽ��� �ٽ� �⺻������ ����
    /// </summary>
    /// <param name="AS"></param>
    /// <returns></returns>
    public void Reset_BGM_Fade()
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            StopCoroutine(SBF(audioSourceBgm[i]));
            audioSourceBgm[i].volume = 1f;
        }
    }
    //public void return_BGM_Volume()
    //{
    //    for (int i = 0; i < audioSourceBgm.Length; i++)
    //    {
    //        audioSourceBgm[i].volume = BGMSlider.value;
    //    }
    //}
    //public void return_SFX_Volume()
    //{
    //    for (int i = 0; i < audioSourcesEffect.Length; i++)
    //    {
    //        audioSourcesEffect[i].volume = SFXSlider.value;
    //    }
    //}
    //public void Pasue()
    //{
    //    for (int i = 0; i < audioSourceBgm.Length; i++)
    //    {
    //        audioSourceBgm[i].Pause();
    //    }
    //    for (int i = 0; i < audioSourcesEffect.Length; i++)
    //    {
    //        audioSourcesEffect[i].Pause();
    //    }
    //}

    /// <summary>
    ///  ���� ������� ��� ȿ���� �Ͻ�����
    /// </summary>
    public void Pause_Sfx()
    {
        for (int i = 0; i < audioSourcesEffect.Length; i++)
        {
            audioSourcesEffect[i].Pause();
        }
    }
    /// <summary>
    /// ��� ȿ���� �Ͻ����� ����
    /// </summary>
    public void UnPause_Sfx()
    {
        for (int i = 0; i < audioSourcesEffect.Length; i++)
        {
            audioSourcesEffect[i].UnPause();
        }
    }
    //public void Play_Re()
    //{
    //    for (int i = 0; i < audioSourceBgm.Length; i++)
    //    {
    //        audioSourceBgm[i].UnPause();
    //    }
    //    for (int i = 0; i < audioSourcesEffect.Length; i++)
    //    {
    //        audioSourcesEffect[i].UnPause();
    //    }
    //}

    /// <summary>
    /// ���� ������� ��� ����� �Ͻ�����
    /// </summary>
    public void Pause_Bgm()
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            audioSourceBgm[i].Pause();
        }
    }
    /// <summary>
    /// ��� ����� �Ͻ����� ����
    /// </summary>
    public void UnPause_Bgm()
    {
        for (int i = 0; i < audioSourceBgm.Length; i++)
        {
            audioSourceBgm[i].UnPause();
        }
    }
    /// <summary>
    /// Slider Value �� ����
    /// </summary>
    public void Save_prview_SliderVale()
    {
        prv_bgm_value = BGMSlider.value;
        prv_sfx_value = SFXSlider.value;
    }
}
