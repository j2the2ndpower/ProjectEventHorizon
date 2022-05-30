using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public AudioClip creditMusic;
    public AudioClip creditSFX;

    public Text leftText;
    public Text rightText;

    public float creditDelay = 3.0f;

    float timer;

    int index = 0;

    string[] credits = new string[]
{
        "Developers:  \n\tJon Bishop, \n\tVincent Bishop & \n\tKurt Schwind",
        "It's our first Game Jam.  Hope you enjoyed.",
        "Music provided by Tangential Cold Studios",
        "SFX from https://kenney.nl/assets/sci-fi-sounds",
        "Background Images from NASA",
        "Stylized Astronaut by Pulsar Bytes on Unity Assetstore",
        "Particle Attractor by Moonflower Carnivore on Unity Assetstore",
        "Black Hole Free by qq.d.y on Unity Assetstore",
        "Apartment Door by DevDen on Unity Assetstore",
        "Asteroid Pack by Pixel Make on Unity Assetstore",
        "Deep Space Skybox Pack by Sean Duffy on Unity Assetstore",
        "Desert Kits 64 Sample by Sagital3D on Unity Assetstore",
        "Dynamic Space Background Lite by DinV Studio on Unity Assetstore",
        "Free PBR Velociraptors by Ferocious Industries on Unity Assetstore",
        "Furniture FREE Pack by Dexsoft Games on Unity Assetstore",
        "Meshtint Free Burrow Cute Series by Meshtint Studio on Unity Assetstore",
        "Meshtint Free Chicken Mega Toon Series by Meshtint Studio on Unity Assetstore",
        "Old \"USSR\" Lamp by ESsplashkid on Unity Assetstore",
        "Realistic toaster by SnowQ on Unity Assetstore",
        "Sedan car - 01 by Final Form Studio on Unity Assetstore",
        "Space SFX - 102218 by GWriterStudio on Unity Assetstore",
        "RPGTalk by Seize Studios on Unity Assetstore",
        "Fonts: https://www.1001freefonts.com/"
};


    // Start is called before the first frame update
    void Start()
    {
        leftText.text = "";
        rightText.text = "";
        SoundSystem._instance.Quiet();
        SoundSystem._instance.PlayMusic(creditMusic);
  

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > creditDelay)
        {
            timer = timer - creditDelay;
            rollCredits();
        }

    }

    void rollCredits()
    {

        int i = index % credits.Length;

        SoundSystem._instance.PlaySFX(creditSFX);


        if ((index % 2) == 0)
        {

            leftText.text = credits[i];
        }
        else
        {
            rightText.text = credits[i];
        }
        index += 1;



    }
}
