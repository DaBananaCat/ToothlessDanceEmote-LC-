using BepInEx;
using CustomEmotesAPI_Template_Mod;
using EmotesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; // This is needed Directory.Exists and simmular methods
using System.Threading.Tasks;
using UnityEngine;

namespace Toothless_Dance_Emote
{
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class ExampleEmoteMod : BaseUnityPlugin
    {
        public const string PluginGUID = "sirbananacat.toothless_dance_emote";
        public const string PluginName = "Toothless Dance Emote";
        public const string PluginVersion = "1.0.4";
        public static PluginInfo PInfo { get; private set; }
        public static ExampleEmoteMod instance;


        public void Awake()
        {
            instance = this;
            PInfo = Info;
            // If there is an assetbundles folder, load the assets from there, else load from the mod folder.
            if (Directory.Exists(Path.Combine(Path.GetDirectoryName(ExampleEmoteMod.PInfo.Location), "assetbundles")))
            {
                CustomEmotesAPI_Template_Mod.Assets.LoadAssetBundlesFromFolder("assetbundles", true);
            } else {
                CustomEmotesAPI_Template_Mod.Assets.LoadAssetBundlesFromFolder("");
            }



            AnimationClipParams emoteParams = new AnimationClipParams();
            emoteParams.animationClip = new List<AnimationClip> { CustomEmotesAPI_Template_Mod.Assets.Load<AnimationClip>("assets/toothless dance.anim") }.ToArray();
            emoteParams.secondaryAnimation = null;// list of secondary animation clips, must be the same size as animationClip, will be picked randomly in the same slot (so if we pick animationClip[3] for an animation, we will also pick secondaryAnimation[3])
            emoteParams.looping = true;//used to specify if audio is looping, you only need to set this if you are importing a single audio file
            emoteParams._primaryAudioClips = new List<AudioClip> { CustomEmotesAPI_Template_Mod.Assets.Load<AudioClip>($"assets/driftveil city.mp3") }.ToArray();//primary list of audio clips
            emoteParams._secondaryAudioClips = null;//secondary list of audio clips, if these are specified, the primary clip will never loop and the secondary clip that plays will always loop
            emoteParams._primaryDMCAFreeAudioClips = null;//same as _primaryAudioClips but will be played if DMCA settings allow it (if normal audio clips exist, and dmca clips do not, the dmca clips will simply be silence)
            emoteParams._secondaryDMCAFreeAudioClips = null;//same as _secondaryAudioClips but will be played if DMCA settings allow it
            emoteParams.visible = true;// If false, will hide the emote from all normal areas, however it can still be invoked through PlayAnimation, use this for emotes that are only needed in code
            emoteParams.syncAnim = true;// If true, will sync animation between all people emoting
            emoteParams.syncAudio = true;// If true, will sync audio between all people emoting
            emoteParams.startPref = -1;// Spot in animationClip array where a BoneMapper will play when there is no other instance of said emote playing -1 is random, -2 is sequential, anything else is what you make it to be
            emoteParams.joinPref = -1;// Spot in animationClip array where a BoneMapper will play when there is at least one other instance of said emote playing, -1 is random, -2 is sequential, anything else is what you make it to be
            emoteParams.joinSpots = null;// Array of join spots which will appear when the animation is playing
            emoteParams.customName = "Toothless Dance";// Custom name for emote, if not specified, the first emote from animationClip will be used as the name
            emoteParams.lockType = AnimationClipParams.LockType.headBobbing;// determines the lock type of your emote, none, headBobbing, lockHead, or rootMotion
            emoteParams.willGetClaimedByDMCA = false;// Lets you mark if your normal set of audio will get claimed by DMCA
            emoteParams.audioLevel = 0.7F;// determines the volume of the emote in terms of alerting enemies, 0 is nothing, 1 is max

            CustomEmotesAPI.AddCustomAnimation(emoteParams);
        }
    }
}