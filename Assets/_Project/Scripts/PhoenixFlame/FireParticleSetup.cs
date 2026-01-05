using UnityEngine;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Helper script to configure a great-looking fire particle system.
    /// Attach to a GameObject with a ParticleSystem component.
    /// Use the context menu to apply fire settings.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class FireParticleSetup : MonoBehaviour
    {
        [Header("Fire Settings")]
        [SerializeField] private float fireHeight = 3f;
        [SerializeField] private float fireWidth = 1.5f;
        [SerializeField] private int particleCount = 200;

        private ParticleSystem ps;

        [ContextMenu("Apply Fire Settings")]
        public void ApplyFireSettings()
        {
            ps = GetComponent<ParticleSystem>();
            if (ps == null) return;

            // Main module
            var main = ps.main;
            main.duration = 1f;
            main.loop = true;
            main.startLifetime = new ParticleSystem.MinMaxCurve(0.5f, 1.5f);
            main.startSpeed = new ParticleSystem.MinMaxCurve(1f, 3f);
            main.startSize = new ParticleSystem.MinMaxCurve(0.3f, 0.8f);
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.maxParticles = particleCount;
            main.gravityModifier = -0.5f; // Fire rises

            // Emission
            var emission = ps.emission;
            emission.enabled = true;
            emission.rateOverTime = 100;

            // Shape - cone pointing up
            var shape = ps.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 15f;
            shape.radius = fireWidth * 0.3f;
            shape.radiusThickness = 1f;

            // Color over lifetime (fade out at top)
            var colorOverLifetime = ps.colorOverLifetime;
            colorOverLifetime.enabled = true;

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.white, 0.0f),
                    new GradientColorKey(Color.white, 0.3f),
                    new GradientColorKey(Color.white, 1.0f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(0.0f, 0.0f),
                    new GradientAlphaKey(1.0f, 0.1f),
                    new GradientAlphaKey(1.0f, 0.5f),
                    new GradientAlphaKey(0.0f, 1.0f)
                }
            );
            colorOverLifetime.color = gradient;

            // Size over lifetime (shrink as it rises)
            var sizeOverLifetime = ps.sizeOverLifetime;
            sizeOverLifetime.enabled = true;
            AnimationCurve sizeCurve = new AnimationCurve();
            sizeCurve.AddKey(0f, 0.5f);
            sizeCurve.AddKey(0.3f, 1f);
            sizeCurve.AddKey(1f, 0f);
            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);

            // Velocity over lifetime (flicker effect)
            var velocityOverLifetime = ps.velocityOverLifetime;
            velocityOverLifetime.enabled = true;
            velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(-0.5f, 0.5f);
            velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(fireHeight * 0.5f, fireHeight);
            velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(-0.5f, 0.5f);

            // Noise (organic movement)
            var noise = ps.noise;
            noise.enabled = true;
            noise.strength = 0.5f;
            noise.frequency = 2f;
            noise.scrollSpeed = 1f;

            // Renderer settings
            var renderer = ps.GetComponent<ParticleSystemRenderer>();
            if (renderer != null)
            {
                renderer.sortingOrder = 10;
                // Use additive blending for glow effect
                // Material should use "Particles/Standard Unlit" with Additive
            }

            Debug.Log("Fire particle settings applied! Make sure to:");
            Debug.Log("1. Assign a soft particle texture (circle gradient)");
            Debug.Log("2. Use material with Particles/Standard Unlit shader");
            Debug.Log("3. Set material Rendering Mode to Additive");
        }

        [ContextMenu("Set Orange Fire Color")]
        public void SetOrangeColor()
        {
            SetFireColor(new Color(1f, 0.4f, 0.1f)); // #FF6619
        }

        [ContextMenu("Set Green Fire Color")]
        public void SetGreenColor()
        {
            SetFireColor(new Color(0.18f, 0.75f, 0.5f)); // #2DBE7F
        }

        [ContextMenu("Set Blue Fire Color")]
        public void SetBlueColor()
        {
            SetFireColor(new Color(0.18f, 0.64f, 1f)); // #2FA4FF
        }

        public void SetFireColor(Color color)
        {
            ps = GetComponent<ParticleSystem>();
            if (ps == null) return;

            var main = ps.main;

            // Create gradient from white core to colored edges
            Gradient startGradient = new Gradient();
            Color coreColor = Color.Lerp(color, Color.white, 0.7f);

            startGradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(coreColor, 0.0f),
                    new GradientColorKey(color, 1.0f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1.0f, 0.0f),
                    new GradientAlphaKey(1.0f, 1.0f)
                }
            );

            main.startColor = new ParticleSystem.MinMaxGradient(startGradient);
        }
    }
}
