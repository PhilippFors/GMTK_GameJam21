using UnityEngine;

[CreateAssetMenu(fileName = "Values", menuName = "CameraShake", order = 0)]
public class CameraShakeValues : ScriptableObject
{
    public float stress = 1;
    public float frequency = 25;
    public float traumaExponent = 1;
    public float recoverySpeed = 1;
    public bool angularShake;
    public bool translationShake;
    public Vector3 maximumAngularShake = Vector3.one * 15;
    public Vector3 maximumTranslationShake = Vector3.one;
    public ActivationTypes activation;
    public bool useGibbonVelocity;
    public float velocityMult = 1;

}

public enum ActivationTypes
{
    onCrash,
    onGround,
    onTrickBoost
}