using System.Collections.Generic;
using General.Utilities;
using NaughtyAttributes;
using UnityEngine;

public class CameraShake : SingletonBehaviour<CameraShake>
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    private Vector3 originalPos;

    
    public List<CameraShakeValues> valuesList;
    
    /// <summary>
    /// Uses angular shake when true
    /// </summary>
    private bool angularShake;
    
    /// <summary>
    /// Uses translation shake when true
    /// </summary>
    private bool translationShake;
    
    /// <summary>
    /// Last Velocity of Gibbon
    /// </summary>
    private Vector3 v;

    /// <summary>
    /// Maximum distance in each direction the transform
    /// with translate during shaking.
    /// </summary>
    Vector3 maximumTranslationShake = Vector3.one;

    /// <summary>
    /// Maximum angle, in degrees, the transform will rotate
    /// during shaking.
    /// </summary>
    Vector3 maximumAngularShake = Vector3.one * 15;

    /// <summary>
    /// Frequency of the Perlin noise function. Higher values
    /// will result in faster shaking.
    /// </summary>
    float frequency = 25;

    /// <summary>
    /// <see cref="trauma"/> is taken to this power before
    /// shaking is applied. Higher values will result in a smoother
    /// falloff as trauma reduces.
    /// </summary>
    float traumaExponent = 1;

    /// <summary>
    /// Amount of trauma per second that is recovered.
    /// </summary>
    float recoverySpeed = 1;

    /// <summary>
    /// Value between 0 and 1 defining the current amount
    /// of stress this transform is enduring.
    /// </summary>
    private float trauma;

    /// <summary>
    ///  Random Value between 0 and 1 to make it more random.
    /// </summary>
    private float seed;


    public void ActivateShake(CameraShakeValues values)
    {
        
        trauma = Mathf.Clamp01(trauma + values.stress);
       
        this.angularShake = values.angularShake;
        this.translationShake = values.translationShake;
        this.traumaExponent = values.traumaExponent;
        this.recoverySpeed = values.recoverySpeed;
        this.maximumAngularShake = values.maximumAngularShake;
        this.maximumTranslationShake = values.maximumTranslationShake;
        Shake();
    }

    public void Shake()
    {
        originalPos = camTransform.position;

        if (trauma > 0)
        {
            // Taking trauma to an exponent allows the ability to smoothen
            // out the transition from shaking to being static.
            float shake = Mathf.Pow(trauma, traumaExponent);

            // This x value of each Perlin noise sample is fixed,
            // allowing a vertical strip of noise to be sampled by each dimension
            // of the translational and rotational shake.
            // PerlinNoise returns a value in the 0...1 range; this is transformed to
            // be in the -1...1 range to ensure the shake travels in all directions.
            if (translationShake)
            {
                camTransform.localPosition = originalPos + new Vector3(
                    maximumTranslationShake.x * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
                    maximumTranslationShake.y * (Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1),
                    maximumTranslationShake.z * (Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1)
                ) * shake;
            }


            if (angularShake)
            {
                camTransform.localRotation = Quaternion.Euler(new Vector3(
                    maximumAngularShake.x * (Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2 - 1),
                    maximumAngularShake.y * (Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2 - 1),
                    maximumAngularShake.z * (Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2 - 1)
                ) * shake);
            }

            trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
        }
        else
        {
            trauma = 0;
          
        }
    }
}