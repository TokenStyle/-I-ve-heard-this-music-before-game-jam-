using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTextureLib
{
    public Texture2D GenerateNoiseTexture(float seed, float noiseFrequency, Texture2D noiseTexture)
    {
        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiseFrequency, (y + seed) * noiseFrequency);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply(); // All the changes to the texture are applied, now it's texture with noise

        return noiseTexture; // return the result noise texture just for case
    }

    // CODE NOT USING THIS AT THIS MOMENT
    public Texture2D GenerateCircularNoiseTexture(float seed, float noiseFrequency, Texture2D noiseTexture, int radiusSize)
    {
        DrawCircle(ref noiseTexture, Color.black, noiseTexture.width/2, noiseTexture.height/2, radiusSize);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiseFrequency, (y + seed) * noiseFrequency);

                if (noiseTexture.GetPixel(x, y).r != 1)
                {
                    noiseTexture.SetPixel(x, y, new Color(v, v, v));
                }
            }
        }

        noiseTexture.Apply(); // All the changes to the texture are applied, now it's texture with noise

        return noiseTexture; // return the result noise texture just for case
    }

    // CODE NOT USING THIS AT THIS MOMENT
    // Draw circle on texture2D
    private Texture2D DrawCircle(ref Texture2D tex, Color color, int x, int y, int radius = 3)
    {
        float rSquared = radius * radius;

        for (int u = x - radius; u < x + radius + 1; u++)
            for (int v = y - radius; v < y + radius + 1; v++)
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                    tex.SetPixel(u, v, color);

        return tex;
    }

    public NoiseTextureLib() { }
}
