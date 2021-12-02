using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCtlScript : MonoBehaviour
{
   

 
    //����ǰ���ͺ��˵�Ŀ���
    public Transform targetForwardTransform;
    public Transform targetBackTransform;


    public float speed=7.3f;
    private ParticleSystem particleSys;
    private ParticleSystem.Particle[] particles;

    //��ʼ�ƶ�����
    public bool startMoveSwitch;
    //���� TRUE����ǰ false�����
    public bool moveForwardOrBack;

    //���ӷ�ɢ����
    public bool scatterSwitch;
    public bool ParticleReset;

    //�����񶯿���
    public bool vibrateSwitch;

    public ParticleSystem.NoiseModule noise;

    //���ӷ�ɢ��ʼ�ٶ�
    //public float particleSpeed=0f;
    public ParticleSystem.MainModule particleSpeed;
    private void Awake()
    {
        particleSys = this.GetComponent<ParticleSystem>();
        if (this.particleSys)
        {
            this.particles = new ParticleSystem.Particle[this.particleSys.main.maxParticles];
            //�Զ�������ϵͳ��ģ��ռ�
            ParticleSystem.MainModule main = this.particleSys.main;
            main.simulationSpace = ParticleSystemSimulationSpace.Custom;
            main.customSimulationSpace = this.transform;
        }
        //ParticleSystem ps = GetComponent<ParticleSystem>();
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region �����ƶ�����

        if (!this.targetForwardTransform || !this.targetBackTransform)
        {
            return;
        }

        if (this.particleSys && startMoveSwitch == true)
        {
            int count = this.particleSys.GetParticles(this.particles);
            for (int i = 0; i < count; i++)
            {
                if (moveForwardOrBack)
                {
                    //��Ŀ����ֵ���� ��ǰ
                    this.particles[i].position = Vector3.MoveTowards(this.particles[i].position, this.targetForwardTransform.position, this.speed * Time.deltaTime);
                }
                else
                {
                    //��Ŀ����ֵ���� ���
                    this.particles[i].position = Vector3.MoveTowards(this.particles[i].position, this.targetBackTransform.position, this.speed * Time.deltaTime);
                }

            }
            this.particleSys.SetParticles(this.particles, count);
        }
        #endregion

        #region ���ӷ�ɢ�ٶ�
        if (scatterSwitch)
        {
            if (!ParticleReset)
            {
                particleSys.Clear();
                ParticleReset = true;
            }
            
            particleSpeed = particleSys.main;
            particleSpeed.startSpeed = 5f;
            particleSys.Play();
            
        }
        else
        {
            ParticleReset = false;
            particleSpeed = particleSys.main;
            particleSpeed.startSpeed = 0f;
            particleSys.Play();
        } 
        #endregion


        #region ��������ع��ܴ���

        if (vibrateSwitch)
        {
            startMoveSwitch = false;
            //this.particleSys.noise.strength.constant = 0.2f;



            noise = particleSys.noise;
            noise.enabled = true;
            //���Ӷ���ǿ��������
            noise.strength = particleStrength + handDistance * 10f;
            noise.quality = ParticleSystemNoiseQuality.High;
            noise.frequency = particlefrequency + handDistance * 100f;


        }
        else
        {
            handDistance = 0f;
            noise = particleSys.noise;
            noise.enabled = true;
            noise.strength = particleStrength;
            noise.quality = ParticleSystemNoiseQuality.High;
            noise.frequency = particlefrequency;
        } 
        #endregion


    }
    public float handDistance=0f;
    public float particleStrength=0.2f;
    public float particlefrequency=0.5f;

}
