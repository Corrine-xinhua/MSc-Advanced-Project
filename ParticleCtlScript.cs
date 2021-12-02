using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCtlScript : MonoBehaviour
{
   

 
    //定义前进和后退的目标点
    public Transform targetForwardTransform;
    public Transform targetBackTransform;


    public float speed=7.3f;
    private ParticleSystem particleSys;
    private ParticleSystem.Particle[] particles;

    //开始移动开关
    public bool startMoveSwitch;
    //朝向 TRUE是向前 false是向后
    public bool moveForwardOrBack;

    //粒子分散开关
    public bool scatterSwitch;
    public bool ParticleReset;

    //粒子振动开关
    public bool vibrateSwitch;

    public ParticleSystem.NoiseModule noise;

    //粒子分散初始速度
    //public float particleSpeed=0f;
    public ParticleSystem.MainModule particleSpeed;
    private void Awake()
    {
        particleSys = this.GetComponent<ParticleSystem>();
        if (this.particleSys)
        {
            this.particles = new ParticleSystem.Particle[this.particleSys.main.maxParticles];
            //自定义粒子系统的模拟空间
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
        #region 粒子移动代码

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
                    //朝目标点插值缓动 向前
                    this.particles[i].position = Vector3.MoveTowards(this.particles[i].position, this.targetForwardTransform.position, this.speed * Time.deltaTime);
                }
                else
                {
                    //朝目标点插值缓动 向后
                    this.particles[i].position = Vector3.MoveTowards(this.particles[i].position, this.targetBackTransform.position, this.speed * Time.deltaTime);
                }

            }
            this.particleSys.SetParticles(this.particles, count);
        }
        #endregion

        #region 粒子分散速度
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


        #region 粒子振动相关功能代码

        if (vibrateSwitch)
        {
            startMoveSwitch = false;
            //this.particleSys.noise.strength.constant = 0.2f;



            noise = particleSys.noise;
            noise.enabled = true;
            //粒子抖动强弱的因素
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
